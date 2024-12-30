using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Business.Synthesizer.Wishes.Obsolete;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using static JJ.Framework.Reflection.ExpressionHelper;
using static JJ.Business.Synthesizer.Calculation.AudioFileOutputs.AudioFileOutputCalculatorFactory;
using static JJ.Business.Synthesizer.Enums.SpeakerSetupEnum;
using static JJ.Business.Synthesizer.Wishes.Helpers.DebuggerDisplayFormatter;
using static JJ.Business.Synthesizer.Wishes.LogWishes;
using static JJ.Business.Synthesizer.Wishes.NameWishes;
using static JJ.Business.Synthesizer.Wishes.JJ_Framework_IO_Wishes.FileWishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.ServiceFactory;
using static JJ.Business.Synthesizer.Wishes.Helpers.CloneWishes;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ParameterHidesMember
// ReSharper disable UseObjectOrCollectionInitializer
// ReSharper disable AccessToModifiedClosure
// ReSharper disable once PossibleLossOfFraction
// ReSharper disable once InvokeAsExtensionMethod

#pragma warning disable IDE0028

namespace JJ.Business.Synthesizer.Wishes
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class Buff
    {
        string DebuggerDisplay => GetDebuggerDisplay(this);
        
        /// <inheritdoc cref="docs._buffbytes"/>
        public byte[] Bytes { get; set; }
        public string FilePath { get; set; }
        public AudioFileOutput UnderlyingAudioFileOutput { get; internal set; }

        public AudioFileFormatEnum GetAudioFormat => UnderlyingAudioFileOutput?.GetAudioFileFormatEnum() ?? default;
        public string Name => ResolveName(UnderlyingAudioFileOutput, FilePath);
    }

    // MakeBuff in SynthWishes

    public partial class SynthWishes
    {
        // On Tape
        
        /// <inheritdoc cref="docs._makebuff" />
        internal void MakeBuff(Tape tape, [CallerMemberName] string callerMemberName = null)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));

            if (tape.UnderlyingAudioFileOutput == null)
            {
                tape.UnderlyingAudioFileOutput = ConfigureAudioFileOutput(tape);
            }
            
            InternalMakeBuff(tape, callerMemberName);
            
            tape.Sample = Sample(tape);
        }

        internal AudioFileOutput ConfigureAudioFileOutput(
            Tape tape, [CallerMemberName] string callerMemberName = null)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            // Configure AudioFileOutput (avoid backend, because buggy)
            
            var audioFileOutputRepository = CreateRepository<IAudioFileOutputRepository>(Context);
            AudioFileOutput audioFileOutput = audioFileOutputRepository.Create();
            audioFileOutput.Name = tape.Descriptor();
            audioFileOutput.FilePath = tape.GetFilePath(callerMemberName: callerMemberName);
            audioFileOutput.Amplifier = tape.Config.Bits.MaxValue();
            audioFileOutput.TimeMultiplier = 1;
            audioFileOutput.Duration = tape.Duration;
            audioFileOutput.SetBits(tape.Config.Bits, Context);
            audioFileOutput.SetAudioFormat(tape.Config.AudioFormat, Context);
            audioFileOutput.SamplingRate = tape.Config.SamplingRate;
            
            audioFileOutput.SpeakerSetup = GetSubstituteSpeakerSetup(tape.Outlets.Count);
            CreateOrRemoveChannels(audioFileOutput, tape.Outlets.Count);

            switch (tape.Outlets.Count)
            {
                case 1:
                    audioFileOutput.AudioFileOutputChannels[0].Outlet = tape.Outlets[0];
                    // Fool AudioFileOutput(Calculator) to use ChannelIndex 1 for its mono output.
                    if (tape.Config.IsRight)
                    {
                        audioFileOutput.AudioFileOutputChannels[0].Index = 1;
                    }

                    break;

                case 2:
                    audioFileOutput.AudioFileOutputChannels[0].Outlet = tape.Outlets[0];
                    audioFileOutput.AudioFileOutputChannels[1].Outlet = tape.Outlets[1];
                    break;

                default:
                    throw new Exception($"Value not supported: {GetText(() => tape.Outlets.Count)} = {GetValue(() => tape.Outlets.Count)}");;
            }
            
            LogAction(audioFileOutput, "Create");
            
            return audioFileOutput;
        }
        
        /// <inheritdoc cref="docs._makebuff" />
        internal static void InternalMakeBuff(
            Tape tape, [CallerMemberName] string callerMemberName = null)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            if (tape.UnderlyingAudioFileOutput == null) throw new ArgumentNullException(nameof(tape.UnderlyingAudioFileOutput));
            
            var audioFileOutput = tape.UnderlyingAudioFileOutput;

            // Process parameter
            string resolvedName = ResolveName(tape.GetName(), audioFileOutput, callerMemberName);
            string resolvedFilePath = tape.GetFilePath(audioFileOutput.FilePath, callerMemberName);
            
            bool inMemory = !(tape.Actions.DiskCache.On || tape.Actions.Save.On || tape.Actions.SaveChannels.On);

            audioFileOutput.Name = resolvedName;

            // Inject stream where back-end originally created it internally.
            byte[] bytes = null;
            var calculator = CreateAudioFileOutputCalculator(audioFileOutput);
            var calculatorAccessor = new AudioFileOutputCalculatorAccessor(calculator);
            if (inMemory)
            {
                // Inject an in-memory stream
                bytes = new byte[audioFileOutput.FileLengthNeeded(tape.Config.CourtesyFrames)];
                
                calculatorAccessor._stream = new MemoryStream(bytes);
            }
            else 
            {
                // Inject a file stream
                // (CreateSafeFileStream numbers files to prevent file name contention
                //  It does so in a thread-safe, interprocess-safe way.)
                FileStream fileStream;
                (resolvedFilePath, fileStream) = CreateSafeFileStream(resolvedFilePath, maxExtensionLength: ConfigWishes.Static.GetFileExtensionMaxLength);
                calculatorAccessor._stream = fileStream;
                audioFileOutput.FilePath = resolvedFilePath;
            }

            // Calculate
            var stopWatch = Stopwatch.StartNew();
            calculator.Execute();
            stopWatch.Stop();
            double calculationDuration = stopWatch.Elapsed.TotalSeconds;

            // Result
            tape.FilePathResolved = resolvedFilePath;
            tape.Bytes = bytes;
            tape.UnderlyingAudioFileOutput = audioFileOutput;

            if (!inMemory)
            {
                // Mark Save Actions as Done to avoid duplicate saves.
                if (tape.Actions.SaveChannels.On)
                {
                    tape.Actions.SaveChannels.Done = true;
                }
                else if (tape.Actions.Save.On)
                {
                    tape.Actions.Save.Done = true;
                }
            }
            
            // Report
            string report = SynthLog(tape, calculationDuration);
            LogLine(report);
        }

        // MakeBuff Legacy
        
        /// <inheritdoc cref="docs._makebuff" />
        internal Buff MakeBuffLegacy(
            FlowNode signal, FlowNode duration, bool inMemory, bool mustPad,
            string name, string filePath, [CallerMemberName] string callerMemberName = null)
            => MakeBuffLegacy(
                new[] { signal }, duration, inMemory, mustPad, name, filePath, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        internal Buff MakeBuffLegacy(
            IList<FlowNode> channelSignals, FlowNode duration, bool inMemory, bool mustPad,
            string name, string filePath, [CallerMemberName] string callerMemberName = null)
        {
            if (channelSignals == null) throw new ArgumentNullException(nameof(channelSignals));
            
            channelSignals.ForEach(Assert);

            if (mustPad)
            {
                this.ApplyPaddingOld(channelSignals);
            }

            // Run Parallel Processing
            if (GetParallelProcessing)
            {
                _tapeRunner.RunAllTapes();
            }
            
            Tape dummyTape = CloneTape(this);
            dummyTape.SetSignals(channelSignals);
            dummyTape.Duration = (duration ?? GetAudioLength).Value;
            dummyTape.Actions.Save.On = !inMemory;
            dummyTape.FallbackName = ResolveName(name, callerMemberName);
            dummyTape.FilePathSuggested = filePath;

            LogAction(dummyTape, "Create", "Buff Legacy");
            
            MakeBuff(dummyTape);
            
            return dummyTape.Buff;
        }
        
        internal static Buff MakeBuffLegacy(
            AudioFileOutput audioFileOutput,
            bool inMemory, int courtesyFrames, 
            string name, string filePath, [CallerMemberName] string callerMemberName = null)
        {
            Tape dummyTape = CloneTape(audioFileOutput);
            
            dummyTape.Actions.DiskCache.On = !inMemory;
            dummyTape.Config.CourtesyFrames = courtesyFrames;
            dummyTape.FallbackName = ResolveName(name, dummyTape.FallbackName, filePath, callerMemberName);
            dummyTape.FilePathSuggested = ResolveFilePath(filePath, dummyTape.FilePathSuggested, ResolveName(name, callerMemberName));
            
            InternalMakeBuff(dummyTape, callerMemberName);
            
            return dummyTape.Buff;
        }
        
        // Helpers
        
        /// <inheritdoc cref="docs._avoidSpeakerSetupsBackEnd" />
        internal SpeakerSetup GetSubstituteSpeakerSetup(int channels)
        {
            switch (channels)
            {
                case 1: return GetMonoSpeakerSetupSubstitute();
                case 2: return GetStereoSpeakerSetupSubstitute();
                default: throw new Exception($"Unsupported value {new{channels}}");
            }
        }
        
        /// <inheritdoc cref="docs._avoidSpeakerSetupsBackEnd" />
        private static readonly object _stereoSpeakerSetupSubstituteLock = new object();
        
        /// <inheritdoc cref="docs._avoidSpeakerSetupsBackEnd" />
        private static SpeakerSetup _stereoSpeakerSetupSubstitute;
        
        /// <inheritdoc cref="docs._avoidSpeakerSetupsBackEnd" />
        private SpeakerSetup GetStereoSpeakerSetupSubstitute()
        {
            if (_stereoSpeakerSetupSubstitute != null)
            {
                return _stereoSpeakerSetupSubstitute;
            }

            lock (_stereoSpeakerSetupSubstituteLock)
            {
                _stereoSpeakerSetupSubstitute = CreateStereoSpeakerSetupSubstitute();
                return _stereoSpeakerSetupSubstitute;
            }
        }
        
        private SpeakerSetup CreateStereoSpeakerSetupSubstitute()
        {
                var channelRepository = CreateRepository<IChannelRepository>(Context);
                
                var stereo = new SpeakerSetup
                {
                    ID = (int)Stereo,
                    Name = nameof(Stereo),
                };
                
                var left = new SpeakerSetupChannel
                {
                    ID = 2,
                    Index = 0,
                    Channel = channelRepository.Get((int)ChannelEnum.Left),
                };
                
                var right = new SpeakerSetupChannel
                {
                    ID = 3,
                    Index = 1,
                    Channel = channelRepository.Get((int)ChannelEnum.Right),
                };
                
                left.SpeakerSetup = stereo;
                right.SpeakerSetup = stereo;
                stereo.SpeakerSetupChannels = new List<SpeakerSetupChannel> { left, right };
                
                return stereo;
            }
        
        /// <inheritdoc cref="docs._avoidSpeakerSetupsBackEnd" />
        private static readonly object _monoSpeakerSetupSubstituteLock = new object();
        
        /// <inheritdoc cref="docs._avoidSpeakerSetupsBackEnd" />
        private static SpeakerSetup _monoSpeakerSetupSubstitute;

        /// <inheritdoc cref="docs._avoidSpeakerSetupsBackEnd" />
        private SpeakerSetup GetMonoSpeakerSetupSubstitute()
        {
            if (_monoSpeakerSetupSubstitute != null)
            { 
                return _monoSpeakerSetupSubstitute;
            }
            
            lock (_monoSpeakerSetupSubstituteLock)
            {
                _monoSpeakerSetupSubstitute = CreateMonoSpeakerSetupSubstitute();
                return _monoSpeakerSetupSubstitute;
            }
        }
        
        private SpeakerSetup CreateMonoSpeakerSetupSubstitute()
        {
                var channelRepository = CreateRepository<IChannelRepository>(Context);
                
                var mono = new SpeakerSetup
                {
                    ID = (int)Mono,
                    Name = nameof(Mono),
                };
                
                var center = new SpeakerSetupChannel
                {
                    ID = 1,
                    Index = 0,
                    Channel = channelRepository.Get((int)ChannelEnum.Single),
                };
                
                center.SpeakerSetup = mono;
                mono.SpeakerSetupChannels = new List<SpeakerSetupChannel> { center };
                
                return mono;
            }
        
        /// <inheritdoc cref="docs._avoidSpeakerSetupsBackEnd" />
        internal void CreateOrRemoveChannels(AudioFileOutput audioFileOutput, int channels)
        {
            // (using a lower abstraction layer, to circumvent error-prone syncing code in back-end).
            var audioFileOutputChannelRepository = CreateRepository<IAudioFileOutputChannelRepository>(Context);

            // Create additional channels
            for (int i = audioFileOutput.AudioFileOutputChannels.Count; i < channels; i++)
            {
                // Create
                AudioFileOutputChannel audioFileOutputChannel = audioFileOutputChannelRepository.Create();

                // Set properties
                audioFileOutputChannel.Index = i;

                // Link relationship
                audioFileOutputChannel.AudioFileOutput = audioFileOutput;
                audioFileOutput.AudioFileOutputChannels.Add(audioFileOutputChannel);
            }

            // Remove surplus channels
            for (int i = audioFileOutput.AudioFileOutputChannels.Count - 1; i >= channels; i--)
            {
                AudioFileOutputChannel audioFileOutputChannel = audioFileOutput.AudioFileOutputChannels[i];

                // Clear properties
                audioFileOutputChannel.Outlet = null;

                // Remove parent-child relationship
                audioFileOutputChannel.AudioFileOutput = null;
                audioFileOutput.AudioFileOutputChannels.RemoveAt(i);

                // Delete
                audioFileOutputChannelRepository.Delete(audioFileOutputChannel);
            }
        }
    }
}