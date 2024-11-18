using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using static JJ.Business.Synthesizer.Calculation.AudioFileOutputs.AudioFileOutputCalculatorFactory;
using static JJ.Business.Synthesizer.Wishes.Helpers.FrameworkStringWishes;
using static JJ.Business.Synthesizer.Wishes.NameHelper;
using JJ.Business.Synthesizer.Extensions;
using static JJ.Business.Synthesizer.Enums.SpeakerSetupEnum;
using static JJ.Business.Synthesizer.Wishes.Helpers.FrameworkIOWishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.ServiceFactory;

// ReSharper disable ParameterHidesMember
// ReSharper disable UseObjectOrCollectionInitializer
// ReSharper disable AccessToModifiedClosure
// ReSharper disable once PossibleLossOfFraction
// ReSharper disable once InvokeAsExtensionMethod

#pragma warning disable IDE0028

namespace JJ.Business.Synthesizer.Wishes
{
    // StreamAudio in SynthWishes

    public partial class SynthWishes
    {
        // StreamAudio on Instance
        
        /// <inheritdoc cref="docs._saveorplay" />
        internal Result<StreamAudioData> StreamAudio(
            Func<FlowNode> channelInputFunc, 
            bool inMemory, bool mustPad, IList<string> additionalMessages, string name, [CallerMemberName] string callerMemberName = null)
        {
            name = FetchName(name, callerMemberName);

            var originalChannel = Channel;
            try
            {
                switch (GetSpeakers)
                {
                    case Mono:
                        WithCenter(); var monoOutlet = channelInputFunc();
                        return StreamAudio(new[] { monoOutlet }, inMemory, mustPad, additionalMessages, name);

                    case Stereo:
                        WithLeft(); var leftOutlet = channelInputFunc();
                        WithRight(); var rightOutlet = channelInputFunc();
                        return StreamAudio(new[] { leftOutlet, rightOutlet }, inMemory, mustPad, additionalMessages, name);
                    
                    default:
                        throw new ValueNotSupportedException(GetSpeakers);
                }
            }
            finally
            {
                Channel = originalChannel;
            }
        }
        
        /// <inheritdoc cref="docs._saveorplay" />
        internal Result<StreamAudioData> StreamAudio(
            FlowNode channelInput,
            bool inMemory, bool mustPad, IList<string> additionalMessages, string name, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                new[] { channelInput }, 
                inMemory, mustPad, additionalMessages, name, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        internal Result<StreamAudioData> StreamAudio(
            IList<FlowNode> channelInputs,
            bool inMemory, bool mustPad, IList<string> additionalMessages, string name, [CallerMemberName] string callerMemberName = null)
        {
            // Process Parameters
            if (channelInputs == null) throw new ArgumentNullException(nameof(channelInputs));
            if (channelInputs.Count == 0) throw new ArgumentException("channels.Count == 0", nameof(channelInputs));
            if (channelInputs.Contains(null)) throw new ArgumentException("channels.Contains(null)", nameof(channelInputs));
            additionalMessages = additionalMessages ?? Array.Empty<string>();
            
            // Fetch Name
            name = FetchName(name, callerMemberName);

            // Apply Padding
            if (mustPad)
            {
                for (int i = 0; i < channelInputs.Count; i++)
                {
                    channelInputs[i] = ApplyPadding(channelInputs[i]);
                }
            }

            // Run Parallel Processing
            if (GetParallels)
            {
                RunParallelsRecursive(channelInputs); 
            }

            // Configure AudioFileOutput (avoid backend)
            AudioFileOutput audioFileOutput = ConfigureAudioFileOutput(channelInputs, name);
            
            // Write Audio
            var result = StreamAudio(
                audioFileOutput,
                inMemory, additionalMessages.Union(ToolingHelper.GetToolingWarnings()).ToArray(), name);
            
            return result;
        }

        internal AudioFileOutput ConfigureAudioFileOutput(IList<FlowNode> channelInputs, string name)
        {
            // Configure AudioFileOutput (avoid backend)

            int channelCount = channelInputs.Count;
            var speakerSetupEnum = channelCount.ToSpeakerSetup();
            
            var audioFileOutputRepository = CreateRepository<IAudioFileOutputRepository>(Context);
            AudioFileOutput audioFileOutput = audioFileOutputRepository.Create();
            audioFileOutput.Amplifier = GetBits.GetNominalMax();
            audioFileOutput.TimeMultiplier = 1;
            audioFileOutput.Duration = GetAudioLength.Calculate();
            audioFileOutput.FilePath = FormatAudioFileName(name, GetAudioFormat);
            audioFileOutput.SetBits(GetBits, Context);
            audioFileOutput.SetAudioFormat(GetAudioFormat, Context);
            audioFileOutput.Name = name;
            audioFileOutput.SamplingRate = ResolveSamplingRate();

            SetSpeakerSetup(audioFileOutput, speakerSetupEnum);
            CreateOrRemoveChannels(audioFileOutput, channelCount);

            switch (speakerSetupEnum)
            {
                case Mono:
                    audioFileOutput.AudioFileOutputChannels[0].Outlet = channelInputs[0];
                    break;

                case Stereo:
                    audioFileOutput.AudioFileOutputChannels[0].Outlet = channelInputs[0];
                    audioFileOutput.AudioFileOutputChannels[1].Outlet = channelInputs[1];
                    break;

                default:
                    throw new InvalidValueException(speakerSetupEnum);
            }
            
            return audioFileOutput;
        }

        // StreamAudio in Statics
        
        /// <inheritdoc cref="docs._saveorplay" />
        internal static Result<StreamAudioData> StreamAudio(
            AudioFileOutput entity, 
            bool inMemory, IList<string> additionalMessages, string name, [CallerMemberName] string callerMemberName = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            additionalMessages = additionalMessages ?? Array.Empty<string>();

            //name = StaticFetchName(name, entity.Name, callerMemberName);
            name = StaticFetchName(name, callerMemberName);
            entity.Name = name;
            entity.FilePath = FormatAudioFileName(name, entity.GetAudioFileFormatEnum());            

            // Assert
            
            #if DEBUG
            entity.Assert();
            #endif
            
            foreach (var audioFileOutputChannel in entity.AudioFileOutputChannels)
            {
                audioFileOutputChannel.Outlet?.Assert();
            }
            
            // Warnings
            var warnings = new List<string>();
            warnings.AddRange(additionalMessages);
            foreach (var audioFileOutputChannel in entity.AudioFileOutputChannels)
            {
                warnings.AddRange(audioFileOutputChannel.Outlet?.GetWarnings() ?? Array.Empty<string>());
            }
            warnings.AddRange(entity.GetWarnings());

            // Inject stream where back-end originally created it internally.
            byte[] bytes = null;
            var calculator = CreateAudioFileOutputCalculator(entity);
            var calculatorAccessor = new AudioFileOutputCalculatorAccessor(calculator);
            if (inMemory)
            {
                // Inject an in-memory stream/
                bytes = new byte[entity.GetFileLengthNeeded()];
                calculatorAccessor._stream = new MemoryStream(bytes);
            }
            else 
            {
                // Inject a file stream
                // (CreateSafeFileStream numbers files to prevent file name contention
                //  It does so in a thread-safe, interprocess-safe way.)
                (string filePath, FileStream fileStream) = CreateSafeFileStream(entity.FilePath);
                calculatorAccessor._stream = fileStream;
                entity.FilePath = filePath;
            }

            // Calculate
            var stopWatch = Stopwatch.StartNew();
            calculator.Execute();
            stopWatch.Stop();
            double calculationDuration = stopWatch.Elapsed.TotalSeconds;

            // Result
            var result = new Result<StreamAudioData>
            {
                Successful = true,
                ValidationMessages = warnings.ToCanonical(),
                Data = new StreamAudioData(entity, bytes)
            };

            // Report
            var reportLines = GetReport(result, calculationDuration);
            reportLines.ForEach(Console.WriteLine);
            
            return result;
        }
        
        /// <inheritdoc cref="docs._saveorplay" />
        internal static Result<StreamAudioData> StreamAudio(
            StreamAudioData data, 
            bool inMemory, IList<string> additionalMessages, string name, [CallerMemberName] string callerMemberName = null)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            
            return StreamAudio(
                data.AudioFileOutput,
                inMemory, additionalMessages, name, callerMemberName);
        }

        /// <inheritdoc cref="docs._saveorplay" />
        internal static Result<StreamAudioData> StreamAudio(
            Result<StreamAudioData> result, 
            bool inMemory, IList<string> additionalMessages, string name, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                result.Data, 
                inMemory, additionalMessages, name, callerMemberName);

        // Helpers
                
        private FlowNode ApplyPadding(FlowNode outlet)
        {
            if (GetLeadingSilence == 0 &&
                GetTrailingSilence == 0)
            {
                return outlet;
            }

            Console.WriteLine($"{PrettyTime()} Padding a channel: {GetLeadingSilence} s before | {GetTrailingSilence} s after");

            AddAudioLength(GetLeadingSilence);
            AddAudioLength(GetTrailingSilence);
            
            if (GetLeadingSilence == 0)
            {
                return outlet;
            }
            else
            {
                return Delay(outlet, _[GetLeadingSilence]);
            }
        }

        private static List<string> GetReport(Result<StreamAudioData> result, double calculationDuration)
        {
            ResultWishes.Assert(result);

            // Get Info
            var stringifiedChannels = new List<string>();

            foreach (var audioFileOutputChannel in result.Data.AudioFileOutput.AudioFileOutputChannels)
            {
                string stringify = audioFileOutputChannel.Outlet?.Stringify() ?? "";
                stringifiedChannels.Add(stringify);
            }

            // Gather Lines
            var lines = new List<string>();

            lines.Add("");
            lines.Add(GetPrettyTitle(result.Data.AudioFileOutput.Name ?? result.Data.AudioFileOutput.FilePath));
            lines.Add("");

            string realTimeComplexityMessage = FormatMetrics(result.Data.AudioFileOutput.Duration, calculationDuration, result.Complexity());
            lines.Add(realTimeComplexityMessage);
            lines.Add("");

            lines.Add($"Calculation time: {PrettyDuration(calculationDuration)}");
            lines.Add($"Audio length: {PrettyDuration(result.Data.AudioFileOutput.Duration)}");
            lines.Add($"Sampling rate: { result.Data.AudioFileOutput.SamplingRate } Hz | {result.Data.AudioFileOutput.GetSampleDataTypeEnum()} | {result.Data.AudioFileOutput.GetSpeakerSetupEnum()}");

            lines.Add("");

            IList<string> warnings = result.ValidationMessages.Select(x => x.Text).ToArray();
            if (warnings.Any())
            {
                lines.Add("Warnings:");
                lines.AddRange(warnings.Select(warning => $"- {warning}"));
                lines.Add("");
            }

            for (var i = 0; i < result.Data.AudioFileOutput.AudioFileOutputChannels.Count; i++)
            {
                var channelString = stringifiedChannels[i];

                lines.Add($"Calculation Channel {i + 1}:");
                lines.Add("");
                lines.Add(channelString);
                lines.Add("");
            }

            if (result.Data.Bytes != null)
            {
                lines.Add($"{PrettyByteCount(result.Data.Bytes.Length)} written to memory.");
            }
            if (File.Exists(result.Data.AudioFileOutput.FilePath))
            {
                lines.Add($"Output file: {Path.GetFullPath(result.Data.AudioFileOutput.FilePath)}");
            }

            lines.Add("");

            return lines;
        }
        
        private void SetSpeakerSetup(AudioFileOutput audioFileOutput, SpeakerSetupEnum speakerSetup)
        {
            // Using a lower abstraction layer, to circumvent error-prone syncing code in back-end.

            var channelRepository = CreateRepository<IChannelRepository>(Context);

            switch (speakerSetup)
            {
                case Mono:
                {
                    var mono = new SpeakerSetup
                    {
                        ID = (int)Mono,
                        Name = $"{Mono}",
                    };

                    var single = new SpeakerSetupChannel
                    {
                        ID = 1,
                        Index = 0,
                        Channel = channelRepository.Get((int)ChannelEnum.Single),
                    };

                    audioFileOutput.SpeakerSetup = mono;
                    audioFileOutput.SpeakerSetup.SpeakerSetupChannels = new List<SpeakerSetupChannel> { single };
                    break;
                }

                case Stereo:
                {
                    var stereo = new SpeakerSetup
                    {
                        ID = (int)Stereo,
                        Name = $"{Stereo}",
                    };

                    var left = new SpeakerSetupChannel
                    {
                        ID = 2,
                        Index = 0,
                        SpeakerSetup = audioFileOutput.SpeakerSetup,
                        Channel = channelRepository.Get((int)ChannelEnum.Left),
                    };

                    var right = new SpeakerSetupChannel
                    {
                        ID = 3,
                        Index = 1,
                        SpeakerSetup = audioFileOutput.SpeakerSetup,
                        Channel = channelRepository.Get((int)ChannelEnum.Right),
                    };

                    audioFileOutput.SpeakerSetup = stereo;
                    audioFileOutput.SpeakerSetup.SpeakerSetupChannels = new List<SpeakerSetupChannel> { left, right };
                    break;
                }

                default:
                    throw new InvalidValueException(speakerSetup);
            }
        }

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
        
        /// <inheritdoc cref="docs._resolvesamplingrate"/>
        private int ResolveSamplingRate()
        {
            int samplingRateOverride = GetSamplingRate;
            if (samplingRateOverride != 0)
            {
                //ValidationMessages = new List<ValidationMessage> { $"Sampling rate override: {samplingRateOverride}".ToCanonical() },
                return samplingRateOverride;
            }
            
            var samplingRateForTool = ToolingHelper.TryGetSamplingRateForTooling();
            if (samplingRateForTool.HasValue)
            {
                return samplingRateForTool.Value;
            }
            
            return ConfigHelper.SamplingRate;
        }
    }

    // Info Type

    public class StreamAudioData
    {
        public AudioFileOutput AudioFileOutput { get; }
        
        /// <inheritdoc cref="docs._saveresultbytes"/>
        public byte[] Bytes { get; }

        /// <inheritdoc cref="docs._saveresultbytes"/>
        public StreamAudioData(AudioFileOutput audioFileOutput, byte[] bytes)
        {
            AudioFileOutput = audioFileOutput ?? throw new ArgumentNullException(nameof(audioFileOutput));
            Bytes = bytes;
        }
    }
}