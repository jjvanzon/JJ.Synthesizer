using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Common;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using static JJ.Framework.Reflection.ExpressionHelper;
using static JJ.Business.Synthesizer.Calculation.AudioFileOutputs.AudioFileOutputCalculatorFactory;
using static JJ.Business.Synthesizer.Enums.SpeakerSetupEnum;
using static JJ.Business.Synthesizer.Wishes.LogWishes;
using static JJ.Business.Synthesizer.Wishes.NameHelper;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Text_Wishes.StringExtensionWishes;
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
    public class Buff
    {
        /// <inheritdoc cref="docs._buffbytes"/>
        public byte[] Bytes { get; set; }
        public string FilePath { get; set; }
        public AudioFileOutput UnderlyingAudioFileOutput { get; set; }

        private IList<string> _messages = new List<string>();
        public IList<string> Messages
        {
            get => _messages;
            set => _messages = value ?? throw new NullException(() => Messages);
        }

        public AudioFileFormatEnum AudioFormat 
            => UnderlyingAudioFileOutput?.GetAudioFileFormatEnum() ?? default;
    }

    // MakeBuff in SynthWishes

    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._makebuff" />
        internal void MakeBuffNew(
            Tape tape, [CallerMemberName] string callerMemberName = null)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            var signals = tape.ConcatSignals();


            // Apply Padding
            var originalAudioLength = GetAudioLength;
            try
            {
                if (tape.IsPadding)
                {
                    ApplyPadding(signals);
                }
                
                // Configure AudioFileOutput (avoid backend)
                AudioFileOutput audioFileOutput = ConfigureAudioFileOutputNew(tape);
                
                // Write Audio
                MakeBuffNew(tape, audioFileOutput, callerMemberName);
            }
            finally
            {
                WithAudioLength(originalAudioLength);
            }
        }
        
        internal static AudioFileOutput ConfigureAudioFileOutputNew(
            Tape tape, [CallerMemberName] string callerMemberName = null)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            if (tape.Duration == null) throw new NullException(() => tape.Duration);
            
            // Configure AudioFileOutput (avoid backend)

            IList<FlowNode> channelSignals = tape.ConcatSignals();

            var context = CreateContext();
            var audioFileOutputRepository = CreateRepository<IAudioFileOutputRepository>(context);
            AudioFileOutput audioFileOutput = audioFileOutputRepository.Create();
            audioFileOutput.Name = ResolveName(tape.GetName, callerMemberName) ;
            audioFileOutput.FilePath = ResolveFilePath(tape.AudioFormat, tape.FilePath, tape.FallBackName, callerMemberName);
            audioFileOutput.Amplifier = tape.Bits.GetNominalMax();
            audioFileOutput.TimeMultiplier = 1;
            audioFileOutput.Duration = tape.Duration.Calculate();
            audioFileOutput.SetBits(tape.Bits, context);
            audioFileOutput.SetAudioFormat(tape.AudioFormat, context);
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
            
            return audioFileOutput;
        }
        
        /// <inheritdoc cref="docs._makebuff" />
        internal static void MakeBuffNew(
            Tape tape, AudioFileOutput audioFileOutput, [CallerMemberName] string callerMemberName = null)
        {
            if (audioFileOutput == null) throw new ArgumentNullException(nameof(audioFileOutput));

            // Process parameter
            string resolvedName = ResolveName(tape.GetName, tape.FilePath, audioFileOutput, callerMemberName);
            string resolvedFilePath = ResolveFilePath(audioFileOutput.GetAudioFileFormatEnum(), tape.FilePath, resolvedName, callerMemberName);
            bool inMemory = !tape.CacheToDisk && !tape.IsSave && !tape.IsSaveChannel;

            audioFileOutput.Name = resolvedName;

            // Validate
            
            #if DEBUG
            audioFileOutput.Assert();
            #endif
            
            foreach (var audioFileOutputChannel in audioFileOutput.AudioFileOutputChannels)
            {
                audioFileOutputChannel.Outlet?.Assert();
            }
            
            var warnings = new List<string>();
            foreach (var audioFileOutputChannel in audioFileOutput.AudioFileOutputChannels)
            {
                warnings.AddRange(audioFileOutputChannel.Outlet?.GetWarnings() ?? Array.Empty<string>());
            }
            warnings.AddRange(audioFileOutput.GetWarnings());

            // Inject stream where back-end originally created it internally.
            byte[] bytes = null;
            var calculator = CreateAudioFileOutputCalculator(audioFileOutput);
            var calculatorAccessor = new AudioFileOutputCalculatorAccessor(calculator);
            if (inMemory)
            {
                // Inject an in-memory stream
                bytes = new byte[audioFileOutput.GetFileLengthNeeded(tape.ExtraBufferFrames)];
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
            tape.Buff = new Buff
            {
                Bytes = bytes, 
                FilePath = resolvedFilePath, 
                UnderlyingAudioFileOutput = audioFileOutput, 
                Messages = warnings
            };

            // Report
            var reportLines = GetSynthLog(tape.Buff, calculationDuration);
            reportLines.ForEach(Console.WriteLine);
        }

        // Helpers
        
        internal void ApplyPadding(IList<FlowNode> channelSignals)
        {
            FlowNode leadingSilence = GetLeadingSilence;
            FlowNode trailingSilence = GetTrailingSilence;
            
            if (leadingSilence.Value == 0 && trailingSilence.Value == 0)
            {
                return;
            }
            
            Console.WriteLine($"{PrettyTime()} Padding: {leadingSilence} s before | {trailingSilence} s after");
            
            FlowNode originalAudioLength = GetAudioLength;
            
            // Extend AudioLength once for the two channels.
            AddAudioLength(leadingSilence);
            AddAudioLength(trailingSilence);

            FlowNode newAudioLength = GetAudioLength;
            
            Console.WriteLine(
                $"{PrettyTime()} Padding: AudioLength = {originalAudioLength} + " +
                $"{leadingSilence} + {trailingSilence} = {newAudioLength}");

            for (int i = 0; i < channelSignals.Count; i++)
            {
                channelSignals[i] = ApplyPaddingDelay(channelSignals[i]);
            }
        }

        internal FlowNode ApplyPaddingDelay(FlowNode outlet)
        {
            FlowNode leadingSilence = GetLeadingSilence;
            
            if (leadingSilence.Value == 0)
            {
                return outlet;
            }
            else
            {
                Console.WriteLine($"{PrettyTime()} Padding: Channel Delay + {leadingSilence} s");
                return Delay(outlet, leadingSilence);
            }
        }
        
        /// <inheritdoc cref="docs._avoidSpeakerSetupsBackEnd" />
        internal static SpeakerSetup GetSubstituteSpeakerSetup(int channels)
        {
            switch (channels)
            {
                case 1: return GetMonoSpeakerSetupSubstitute();
                case 2: return GetStereoSpeakerSetupSubstitute();
                default: throw new Exception($"Unsupported value {new{channels}}");
            }
        }
        
        private static readonly object _stereoSpeakerSetupSubstituteLock = new object();
        
        /// <inheritdoc cref="docs._avoidSpeakerSetupsBackEnd" />
        private static SpeakerSetup _stereoSpeakerSetupSubstitute;
        
        /// <inheritdoc cref="docs._avoidSpeakerSetupsBackEnd" />
        private static SpeakerSetup GetStereoSpeakerSetupSubstitute()
        {
            if (_stereoSpeakerSetupSubstitute != null)
            {
                return _stereoSpeakerSetupSubstitute;
            }

            lock (_stereoSpeakerSetupSubstituteLock)
            {
                var channelRepository = CreateRepository<IChannelRepository>();
                
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
        
        private static readonly object _monoSpeakerSetupSubstituteLock = new object();
        
        /// <inheritdoc cref="docs._avoidSpeakerSetupsBackEnd" />
        private static SpeakerSetup _monoSpeakerSetupSubstitute;

        /// <inheritdoc cref="docs._avoidSpeakerSetupsBackEnd" />
        private static SpeakerSetup GetMonoSpeakerSetupSubstitute()
        {
            if (_monoSpeakerSetupSubstitute != null)
            { 
                return _monoSpeakerSetupSubstitute;
            }
            
            lock (_monoSpeakerSetupSubstituteLock)
            {
                var channelRepository = CreateRepository<IChannelRepository>();
                
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
        internal static void CreateOrRemoveChannels(AudioFileOutput audioFileOutput, int channels)
        {
            // (using a lower abstraction layer, to circumvent error-prone syncing code in back-end).
            var audioFileOutputChannelRepository = CreateRepository<IAudioFileOutputChannelRepository>();

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