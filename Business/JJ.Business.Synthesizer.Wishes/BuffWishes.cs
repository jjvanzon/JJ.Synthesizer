using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using static JJ.Business.Synthesizer.Calculation.AudioFileOutputs.AudioFileOutputCalculatorFactory;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Text_Wishes;
using static JJ.Business.Synthesizer.Wishes.NameHelper;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Enums.SpeakerSetupEnum;
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
            var originalChannel = GetChannel;
            try
            {
                switch (GetSpeakers)
                {
                    case Mono:
                    {
                        WithCenter(); var monoOutlet = func();
                        return MakeBuff(new[] { monoOutlet }, duration, inMemory, mustPad, additionalMessages, name, filePath, callerMemberName);
                    }
                    case Stereo:
                    {
                        WithLeft(); var leftOutlet = func();
                        WithRight(); var rightOutlet = func();
                        return MakeBuff(new[] { leftOutlet, rightOutlet }, duration, inMemory, mustPad, additionalMessages, name, filePath, callerMemberName);
                    }
                    default: throw new ValueNotSupportedException(GetSpeakers);
                }
            }
            finally
            {
                WithChannel(originalChannel);
            }
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
            IList<FlowNode> channels, FlowNode duration,
            bool inMemory, bool mustPad, IList<string> additionalMessages, 
            string name, string filePath, [CallerMemberName] string callerMemberName = null)
        {
            // Process Parameters
            if (channels == null) throw new ArgumentNullException(nameof(channels));
            if (channels.Count == 0) throw new ArgumentException("channels.Count == 0", nameof(channels));
            if (channels.Contains(null)) throw new ArgumentException("channels.Contains(null)", nameof(channels));
            additionalMessages = additionalMessages ?? Array.Empty<string>();
            
            // Fetch Name
            name = FetchName(name, filePath, channels, callerMemberName);
           
            Buff buff;
            
            // Apply Padding
            var originalAudioLength = GetAudioLength;
            try
            {
                if (mustPad)
                {
                    ApplyPadding(channels);
                }
                
                // Run Parallel Processing
                if (GetParallelTaping)
                {
                    RunAllTapes(channels);
                }
                
                // Configure AudioFileOutput (avoid backend)
                AudioFileOutput audioFileOutput = ConfigureAudioFileOutput(channels, duration, name, filePath);
                
                // Gather Warnings
                IList<string> configWarnings = _configResolver.GetWarnings(audioFileOutput.GetFileExtension());
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
            IList<FlowNode> channels, FlowNode duration, string name, string filePath)
        {
            // Configure AudioFileOutput (avoid backend)

            int channelCount = channels.Count;
            var speakerSetupEnum = channelCount.ToSpeakerSetup();
            
            var audioFileOutputRepository = CreateRepository<IAudioFileOutputRepository>(Context);
            AudioFileOutput audioFileOutput = audioFileOutputRepository.Create();
            audioFileOutput.Name = FetchName(name, filePath);
            audioFileOutput.FilePath = FetchFilePath(filePath, name, GetAudioFormat.GetFileExtension());
            audioFileOutput.Amplifier = GetBits.GetNominalMax();
            audioFileOutput.TimeMultiplier = 1;
            audioFileOutput.Duration = (duration ?? GetAudioLength).Calculate();
            audioFileOutput.SetBits(GetBits, Context);
            audioFileOutput.SetAudioFormat(GetAudioFormat, Context);
            audioFileOutput.SamplingRate = GetSamplingRate;
            audioFileOutput.SpeakerSetup = GetSubstituteSpeakerSetup(speakerSetupEnum);
            CreateOrRemoveChannels(audioFileOutput, channelCount);

            switch (speakerSetupEnum)
            {
                case Mono:
                    audioFileOutput.AudioFileOutputChannels[0].Outlet = channels[0];
                    break;

                case Stereo:
                    audioFileOutput.AudioFileOutputChannels[0].Outlet = channels[0];
                    audioFileOutput.AudioFileOutputChannels[1].Outlet = channels[1];
                    break;

                default:
                    throw new InvalidValueException(speakerSetupEnum);
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

            string resolvedName = FetchName(name, filePath, audioFileOutput, callerMemberName);
            string resolvedFilePath = FetchFilePath(filePath, resolvedName, audioFileOutput.GetFileExtension(), callerMemberName);

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
            var reportLines = GetReport(buff, calculationDuration);
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
            
            name = FetchName(name, buff, callerMemberName);
            
            return MakeBuff(
                buff.UnderlyingAudioFileOutput,
                inMemory, extraBufferFrames, additionalMessages, 
                name, filePath, callerMemberName);
        }

        // Helpers
        
        private void ApplyPadding(IList<FlowNode> channels)
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

            for (int i = 0; i < channels.Count; i++)
            {
                channels[i] = ApplyPaddingToChannel(channels[i]);
            }
        }

        private FlowNode ApplyPaddingToChannel(FlowNode outlet)
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

        private static List<string> GetReport(Buff buff, double calculationDuration)
        {
            // Get Info
            var stringifiedChannels = new List<string>();

            foreach (var audioFileOutputChannel in buff.UnderlyingAudioFileOutput.AudioFileOutputChannels)
            {
                string stringify = audioFileOutputChannel.Outlet?.Stringify() ?? "";
                stringifiedChannels.Add(stringify);
            }

            // Gather Lines
            var lines = new List<string>();

            lines.Add("");
            lines.Add(GetPrettyTitle(FetchName(buff)));
            lines.Add("");

            string realTimeComplexityMessage = FormatMetrics(buff.UnderlyingAudioFileOutput.Duration, calculationDuration, buff.Complexity());
            lines.Add(realTimeComplexityMessage);
            lines.Add("");

            lines.Add($"Calculation time: {PrettyDuration(calculationDuration)}");
            lines.Add($"Audio length: {PrettyDuration(buff.UnderlyingAudioFileOutput.Duration)}");
            lines.Add($"Sampling rate: {buff.UnderlyingAudioFileOutput.SamplingRate} Hz " +
                      $"| {buff.UnderlyingAudioFileOutput.GetSampleDataTypeEnum()} " +
                      $"| {buff.UnderlyingAudioFileOutput.GetSpeakerSetupEnum()}");

            lines.Add("");

            IList<string> warnings = buff.Messages.ToArray();
            if (warnings.Any())
            {
                lines.Add("Warnings:");
                lines.AddRange(warnings.Select(warning => $"- {warning}"));
                lines.Add("");
            }

            for (var i = 0; i < buff.UnderlyingAudioFileOutput.AudioFileOutputChannels.Count; i++)
            {
                var channelString = stringifiedChannels[i];

                lines.Add($"Calculation Channel {i + 1}:");
                lines.Add("");
                lines.Add(channelString);
                lines.Add("");
            }

            if (buff.Bytes != null)
            {
                lines.Add($"{PrettyByteCount(buff.Bytes.Length)} written to memory.");
            }
            if (File.Exists(buff.FilePath)) // TODO: Remove the if. It may be redundant now.
            {
                lines.Add($"Output file: {Path.GetFullPath(buff.FilePath)}");
            }

            lines.Add("");

            return lines;
        }
        
        /// <inheritdoc cref="docs._avoidSpeakerSetupsBackEnd" />
        private SpeakerSetup GetSubstituteSpeakerSetup(SpeakerSetupEnum speakers)
        {
            switch (speakers)
            {
                case Mono: return GetMonoSpeakerSetupSubstitute();
                case Stereo: return GetStereoSpeakerSetupSubstitute();
                default: throw new InvalidValueException(speakers);
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
        private void CreateOrRemoveChannels(AudioFileOutput audioFileOutput, int channelCount)
        {
            // (using a lower abstraction layer, to circumvent error-prone syncing code in back-end).
            var audioFileOutputChannelRepository = CreateRepository<IAudioFileOutputChannelRepository>(Context);

            // Create additional channels
            for (int i = audioFileOutput.AudioFileOutputChannels.Count; i < channelCount; i++)
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
            for (int i = audioFileOutput.AudioFileOutputChannels.Count - 1; i >= channelCount; i--)
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