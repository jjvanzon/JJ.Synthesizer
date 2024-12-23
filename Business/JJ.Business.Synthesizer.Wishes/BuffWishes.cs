using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_IO_Wishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.ServiceFactory;
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

            AudioFileOutput audioFileOutput = ConfigureAudioFileOutput(tape);
            MakeBuff(tape, audioFileOutput, callerMemberName);
        }
        
        internal AudioFileOutput ConfigureAudioFileOutput(
            Tape tape, [CallerMemberName] string callerMemberName = null)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            // Configure AudioFileOutput (avoid backend, because buggy)

            IList<FlowNode> channelSignals = tape.ConcatSignals();

            var audioFileOutputRepository = CreateRepository<IAudioFileOutputRepository>(Context);
            AudioFileOutput audioFileOutput = audioFileOutputRepository.Create();
            audioFileOutput.Name = tape.Descriptor();
            audioFileOutput.FilePath = ResolveFilePath(tape.AudioFormat, tape.FilePathResolved, tape.FilePathSuggested, tape.Signal, tape.Signals, tape.FallBackName, callerMemberName);
            audioFileOutput.Amplifier = tape.Bits.MaxValue();
            audioFileOutput.TimeMultiplier = 1;
            audioFileOutput.Duration = tape.Duration;
            audioFileOutput.SetBits(tape.Bits, Context);
            audioFileOutput.SetAudioFormat(tape.AudioFormat, Context);
            audioFileOutput.SamplingRate = tape.SamplingRate;
            
            audioFileOutput.SpeakerSetup = GetSubstituteSpeakerSetup(channelSignals.Count);
            CreateOrRemoveChannels(audioFileOutput, channelSignals.Count);

            switch (channelSignals.Count)
            {
                case 1:
                    audioFileOutput.AudioFileOutputChannels[0].Outlet = channelSignals[0];
                    break;

                case 2:
                    audioFileOutput.AudioFileOutputChannels[0].Outlet = channelSignals[0];
                    audioFileOutput.AudioFileOutputChannels[1].Outlet = channelSignals[1];
                    break;

                default:
                    throw new Exception($"Value not supported: {GetText(() => channelSignals.Count)} = {GetValue(() => channelSignals.Count)}");;
            }
            
            LogAction(audioFileOutput, "Create");
            
            return audioFileOutput;
        }
        
        /// <inheritdoc cref="docs._makebuff" />
        internal static void MakeBuff(
            Tape tape, AudioFileOutput audioFileOutput, [CallerMemberName] string callerMemberName = null)
        {
            if (audioFileOutput == null) throw new ArgumentNullException(nameof(audioFileOutput));

            // Process parameter
            string resolvedName = ResolveName(tape.GetName(), audioFileOutput, callerMemberName);
            string resolvedFilePath = ResolveFilePath(audioFileOutput.GetAudioFileFormatEnum(), tape.GetFilePath(), audioFileOutput, callerMemberName);
            bool inMemory = !tape.DiskCache && !tape.IsSave && !tape.IsSaveChannel;

            audioFileOutput.Name = resolvedName;

            // Validate
            
            #if DEBUG
            audioFileOutput.Assert();
            #endif
            
            foreach (var audioFileOutputChannel in audioFileOutput.AudioFileOutputChannels)
            {
                audioFileOutputChannel.Outlet?.Assert();
            }

            // Inject stream where back-end originally created it internally.
            byte[] bytes = null;
            var calculator = CreateAudioFileOutputCalculator(audioFileOutput);
            var calculatorAccessor = new AudioFileOutputCalculatorAccessor(calculator);
            if (inMemory)
            {
                // Inject an in-memory stream
                bytes = new byte[audioFileOutput.FileLengthNeeded(tape.CourtesyFrames)];
                calculatorAccessor._stream = new MemoryStream(bytes);
            }
            else 
            {
                // Inject a file stream
                // (CreateSafeFileStream numbers files to prevent file name contention
                //  It does so in a thread-safe, interprocess-safe way.)
                FileStream fileStream;
                (resolvedFilePath, fileStream) = CreateSafeFileStream(resolvedFilePath);
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
                if (tape.IsSave) tape.IsSaved = true;
                if (tape.IsSaveChannel) tape.ChannelIsSaved = true;
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
            
            // Help ReSharper not error over unused legacy parameter.
            mustPad = mustPad;

            var dummyTape = new Tape
            {
                Signals = channelSignals,
                
                Duration = (duration ?? GetAudioLength).Value,
                LeadingSilence = GetLeadingSilence.Value,
                TrailingSilence = GetTrailingSilence.Value,
                
                FilePathSuggested = filePath,
                FallBackName = ResolveName(name, callerMemberName),
                
                SamplingRate = GetSamplingRate,
                Bits = GetBits,
                Channels = channelSignals.Count,
                AudioFormat = GetAudioFormat,
                Interpolation = GetInterpolation,

                IsSave = !inMemory,

                DiskCache = GetDiskCache,
                PlayAllTapes = GetPlayAllTapes,
                CourtesyFrames = GetCourtesyFrames
            };
            
            MakeBuff(dummyTape);
            
            return dummyTape.Buff;
        }
        
        internal static Buff MakeBuffLegacy(
            AudioFileOutput audioFileOutput,
            bool inMemory, int courtesyFrames, 
            string name, string filePath, [CallerMemberName] string callerMemberName = null)
        {
            var dummyTape = new Tape
            {
                CourtesyFrames = courtesyFrames,
                FilePathSuggested = filePath,
                FallBackName = name,
                DiskCache = !inMemory
            };
            
            MakeBuff(dummyTape, audioFileOutput, callerMemberName);
            
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
                
                _stereoSpeakerSetupSubstitute = stereo;
                
                return stereo;
            }
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
                
                _monoSpeakerSetupSubstitute = mono;
                
                return mono;
            }
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