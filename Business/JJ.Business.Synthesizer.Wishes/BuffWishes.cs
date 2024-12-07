using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Wishes.Helpers;
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
        // MakeBuff on Instance (Start-of-Chain)
        
        /// <inheritdoc cref="docs._makebuff" />
        internal Buff MakeBuff(
            Func<FlowNode> func, FlowNode duration,
            bool inMemory, bool mustPad, IList<string> additionalMessages, 
            string name, string filePath, [CallerMemberName] string callerMemberName = null)
        {
            var channelSignals = GetChannelSignals(func);
            return MakeBuff(channelSignals, duration, inMemory, mustPad, additionalMessages, name, filePath, callerMemberName);
        }
        
        /// <inheritdoc cref="docs._makebuff" />
        internal Buff MakeBuff(
            FlowNode channel, FlowNode duration,
            bool inMemory, bool mustPad, IList<string> additionalMessages, 
            string name, string filePath, [CallerMemberName] string callerMemberName = null)
            => MakeBuff(
                new[] { channel }, duration,
                inMemory, mustPad, additionalMessages, name, filePath, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        internal Buff MakeBuff(
            IList<FlowNode> channelSignals, FlowNode duration,
            bool inMemory, bool mustPad, IList<string> additionalMessages, 
            string name, string filePath, [CallerMemberName] string callerMemberName = null)
        {
            // Process Parameters
            if (channelSignals == null) throw new ArgumentNullException(nameof(channelSignals));
            if (channelSignals.Count == 0) throw new ArgumentException("channelSignals.Count == 0", nameof(channelSignals));
            if (channelSignals.Contains(null)) throw new ArgumentException("channelSignals.Contains(null)", nameof(channelSignals));
            additionalMessages = additionalMessages ?? Array.Empty<string>();
            
            // Resolve Name
            name = ResolveName(name, filePath, channelSignals, callerMemberName);
           
            Buff buff;
            
            // Apply Padding
            var originalAudioLength = GetAudioLength;
            try
            {
                if (mustPad)
                {
                    ApplyPadding(channelSignals);
                }
                
                // Run Parallel Processing
                if (GetParallelTaping)
                {
                    _tapeRunner.RunAllTapes();
                }
                
                // Configure AudioFileOutput (avoid backend)
                AudioFileOutput audioFileOutput = ConfigureAudioFileOutput(channelSignals, duration, name, filePath);
                
                // Gather Warnings
                IList<string> configWarnings = Config.GetWarnings(audioFileOutput.GetFileExtension());
                IList<string> warnings = additionalMessages.Union(configWarnings).ToArray();
                
                // Write Audio
                buff = MakeBuff(audioFileOutput, inMemory, GetExtraBufferFrames, warnings, name, filePath);
            }
            finally
            {
                WithAudioLength(originalAudioLength);
            }
            
            return buff;
        }
        
        /// <param name="duration">Nullable. Falls back to AudioLength or else to a 1-second time span.</param>
        private AudioFileOutput ConfigureAudioFileOutput(
            IList<FlowNode> channelSignals, FlowNode duration, string name, string filePath)
        {
            // Configure AudioFileOutput (avoid backend)

            var audioFileOutputRepository = CreateRepository<IAudioFileOutputRepository>(Context);
            AudioFileOutput audioFileOutput = audioFileOutputRepository.Create();
            audioFileOutput.Name = ResolveName(name, filePath);
            audioFileOutput.FilePath = ResolveFilePath(filePath, name, GetAudioFormat);
            audioFileOutput.Amplifier = GetBits.GetNominalMax();
            audioFileOutput.TimeMultiplier = 1;
            audioFileOutput.Duration = (duration ?? GetAudioLength).Calculate();
            audioFileOutput.SetBits(GetBits, Context);
            audioFileOutput.SetAudioFormat(GetAudioFormat, Context);
            audioFileOutput.SamplingRate = GetSamplingRate;
            
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

        // MakeBuff in Statics (End-of-Chain)
        
        /// <inheritdoc cref="docs._makebuff" />
        internal static Buff MakeBuff(
            AudioFileOutput audioFileOutput,
            bool inMemory, int extraBufferFrames, IList<string> additionalMessages, 
            string name, string filePath, [CallerMemberName] string callerMemberName = null)
        {
            if (audioFileOutput == null) throw new ArgumentNullException(nameof(audioFileOutput));
            additionalMessages = additionalMessages ?? Array.Empty<string>();

            string resolvedName = ResolveName(name, filePath, audioFileOutput, callerMemberName);
            string resolvedFilePath = ResolveFilePath(filePath, resolvedName, audioFileOutput.GetAudioFileFormatEnum(), callerMemberName);

            audioFileOutput.Name = resolvedName;

            // Assert
            
            #if DEBUG
            audioFileOutput.Assert();
            #endif
            
            foreach (var audioFileOutputChannel in audioFileOutput.AudioFileOutputChannels)
            {
                audioFileOutputChannel.Outlet?.Assert();
            }
            
            // Warnings
            var warnings = new List<string>();
            warnings.AddRange(additionalMessages);
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
                bytes = new byte[audioFileOutput.GetFileLengthNeeded(extraBufferFrames)];
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
            var buff = new Buff
            {
                Bytes = bytes, 
                FilePath = resolvedFilePath, 
                UnderlyingAudioFileOutput = audioFileOutput, 
                Messages = warnings
            };

            // Report
            var reportLines = GetSynthLog(buff, calculationDuration);
            reportLines.ForEach(Console.WriteLine);
            
            return buff;
        }
        
        /// <inheritdoc cref="docs._makebuff" />
        internal static Buff MakeBuff(
            Buff buff, 
            bool inMemory, int extraBufferFrames, IList<string> additionalMessages, 
            string name, string filePath, [CallerMemberName] string callerMemberName = null)
        {
            if (buff == null) throw new ArgumentNullException(nameof(buff));
            
            name = ResolveName(name, buff, callerMemberName);
            
            return MakeBuff(
                buff.UnderlyingAudioFileOutput,
                inMemory, extraBufferFrames, additionalMessages, 
                name, filePath, callerMemberName);
        }

        // Helpers
        
        private void ApplyPadding(IList<FlowNode> channelSignals)
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
        private SpeakerSetup GetSubstituteSpeakerSetup(int channels)
        {
            switch (channels)
            {
                case 1: return GetMonoSpeakerSetupSubstitute();
                case 2: return GetStereoSpeakerSetupSubstitute();
                default: throw new Exception($"Unsupported value {new{channels}}");
            }
        }
        
        private readonly object _stereoSpeakerSetupSubstituteLock = new object();
        
        /// <inheritdoc cref="docs._avoidSpeakerSetupsBackEnd" />
        private SpeakerSetup _stereoSpeakerSetupSubstitute;
        
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
        
        private readonly object _monoSpeakerSetupSubstituteLock = new object();
        
        /// <inheritdoc cref="docs._avoidSpeakerSetupsBackEnd" />
        private SpeakerSetup _monoSpeakerSetupSubstitute;

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
        private void CreateOrRemoveChannels(AudioFileOutput audioFileOutput, int channels)
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