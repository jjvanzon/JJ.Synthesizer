using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Calculation.AudioFileOutputs;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using static System.Console;
using static JJ.Business.Synthesizer.Enums.ChannelEnum;
using static JJ.Business.Synthesizer.Wishes.Helpers.FrameworkWishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.NameHelper;
// ReSharper disable UseObjectOrCollectionInitializer

namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        // Want my static usings, but clashes with System type names.
        public readonly SampleDataTypeEnum Int16 = SampleDataTypeEnum.Int16;
        public readonly SampleDataTypeEnum Byte = SampleDataTypeEnum.Byte;
        public readonly ChannelEnum Single = ChannelEnum.Single;

        private void InitializeAudioFileWishes(IContext context)
        {
            _audioFileOutputManager = ServiceFactory.CreateAudioFileOutputManager(context);
            _samples = ServiceFactory.CreateSampleManager(context);
        }

        private AudioFileOutputManager _audioFileOutputManager;
        private SampleManager _samples;

        // Sample
        
        /// <inheritdoc cref="docs._sample"/>
        public SampleOperatorWrapper Sample(
            byte[] bytes, 
            InterpolationTypeEnum interpolationTypeEnum = default,
            double amplifier = 1, double speedFactor = 1, int bytesToSkip = 0)
            => SampleBase(new MemoryStream(bytes), default, interpolationTypeEnum, amplifier, speedFactor, bytesToSkip);
        
        /// <inheritdoc cref="docs._sample"/>
        public SampleOperatorWrapper Sample(
            Stream stream,
            InterpolationTypeEnum interpolationTypeEnum = default,
            double amplifier = 1, double speedFactor = 1, int bytesToSkip = 0)
            => SampleBase(stream, default, interpolationTypeEnum, amplifier, speedFactor, bytesToSkip);

        /// <inheritdoc cref="docs._sample"/>
        public SampleOperatorWrapper Sample(
            string filePath,
            InterpolationTypeEnum interpolationTypeEnum = default,
            double amplifier = 1, double speedFactor = 1, int bytesToSkip = 0)
        {
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                return SampleBase(stream, filePath, interpolationTypeEnum, amplifier, speedFactor, bytesToSkip);
        }

        /// <inheritdoc cref="docs._sample"/>
        private SampleOperatorWrapper SampleBase(
            Stream stream, string filePath,
            InterpolationTypeEnum interpolationTypeEnum,
            double amplifier, double speedFactor, int bytesToSkip)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            if (interpolationTypeEnum == default) interpolationTypeEnum = InterpolationTypeEnum.Line;
            
            Sample sample = _samples.CreateSample(stream);
            sample.Amplifier = 1.0 / sample.SampleDataType.GetMaxAmplitude() * amplifier;
            sample.TimeMultiplier = 1 / speedFactor;
            sample.BytesToSkip = bytesToSkip;
            sample.SetInterpolationTypeEnum(interpolationTypeEnum, Context);

            if (!string.IsNullOrWhiteSpace(filePath))
            {
                sample.Location = Path.GetFullPath(filePath);
                sample.Name = Path.GetFileNameWithoutExtension(filePath);
            }

            var wrapper = _operatorFactory.Sample(sample);
            ((Outlet)wrapper).Operator.Name = sample.Name;

            return wrapper;
        }

        // Play
        
        /// <inheritdoc cref="docs._saveaudio" />
        public Result<AudioFileOutput> Play(
            Func<Outlet> outletFunc,
            double duration = default,
            double volume = default,
            SpeakerSetupEnum speakerSetupEnum = SpeakerSetupEnum.Stereo,
            SampleDataTypeEnum sampleDataTypeEnum = SampleDataTypeEnum.Int16,
            AudioFileFormatEnum audioFileFormatEnum = AudioFileFormatEnum.Wav,
            int samplingRateOverride = default,
            string fileName = default,
            [CallerMemberName] string callerMemberName = null)
        {
            (outletFunc, duration) = ApplyLeadingSilence(outletFunc, duration);

            var result =
                SaveAudio(
                    outletFunc, duration, volume,
                    speakerSetupEnum, sampleDataTypeEnum, audioFileFormatEnum, samplingRateOverride,
                    fileName, callerMemberName);
            
            PlayIfAllowed(result.Data);
            
            return result;
        }
        
        /// <inheritdoc cref="docs._saveaudio" />
        public Result<AudioFileOutput> PlayMono(
            Func<Outlet> outletFunc,
            double duration = default,
            double volume = default,
            SampleDataTypeEnum sampleDataTypeEnum = SampleDataTypeEnum.Int16,
            AudioFileFormatEnum audioFileFormatEnum = AudioFileFormatEnum.Wav,
            int samplingRateOverride = default,
            string fileName = default,
            [CallerMemberName] string callerMemberName = null)
        {
            (outletFunc, duration) = ApplyLeadingSilence(outletFunc, duration);

            var result =
                SaveAudioMono(
                    outletFunc, duration, volume,
                    sampleDataTypeEnum, audioFileFormatEnum, samplingRateOverride,
                    fileName, callerMemberName);
            
            PlayIfAllowed(result.Data);

            return result;
        }
        
        // Save
        
        /// <inheritdoc cref="docs._saveaudio" />
        public Result<AudioFileOutput> SaveAudioMono(
            Func<Outlet> outletFunc,
            double duration = default,
            double volume = default,
            SampleDataTypeEnum sampleDataTypeEnum = SampleDataTypeEnum.Int16,
            AudioFileFormatEnum audioFileFormatEnum = AudioFileFormatEnum.Wav,
            int samplingRateOverride = default,
            string fileName = default,
            [CallerMemberName] string callerMemberName = null)
            => SaveAudio(outletFunc,
                         duration, volume,
                         SpeakerSetupEnum.Mono, sampleDataTypeEnum, audioFileFormatEnum,
                         samplingRateOverride, fileName, callerMemberName);

        /// <inheritdoc cref="docs._saveaudio" />
        public Result<AudioFileOutput> SaveAudio(
            Func<Outlet> func,
            double duration = default,
            double volume = default,
            SpeakerSetupEnum speakerSetupEnum = SpeakerSetupEnum.Stereo,
            SampleDataTypeEnum sampleDataTypeEnum = SampleDataTypeEnum.Int16,
            AudioFileFormatEnum audioFileFormatEnum = AudioFileFormatEnum.Wav,
            int samplingRateOverride = default,
            string fileName = default,
            [CallerMemberName] string callerMemberName = null)
        {
            var originalChannel = Channel;
            try
            {
                switch (speakerSetupEnum)
                {
                    case SpeakerSetupEnum.Mono:
                        Channel = ChannelEnum.Single; var monoOutlet = func();
                        
                        return SaveAudioBase(
                            new[] { monoOutlet },
                            duration, volume,
                            sampleDataTypeEnum, audioFileFormatEnum,
                            samplingRateOverride, fileName, callerMemberName);

                    case SpeakerSetupEnum.Stereo:
                        Channel = Left; var leftOutlet = func();
                        Channel = Right; var rightOutlet = func();
                        
                        return SaveAudioBase(
                            new[] { leftOutlet, rightOutlet },
                            duration, volume,
                            sampleDataTypeEnum, audioFileFormatEnum,
                            samplingRateOverride, fileName, callerMemberName);
                    default:
                        throw new ValueNotSupportedException(speakerSetupEnum);
                }
            }
            finally
            {
                Channel = originalChannel;
            }
        }

        /// <inheritdoc cref="docs._saveaudio" />
        private Result<AudioFileOutput> SaveAudioBase(
            IList<Outlet> channelInputs,
            double duration,
            double volume,
            SampleDataTypeEnum sampleDataTypeEnum,
            AudioFileFormatEnum audioFileFormatEnum,
            int samplingRateOverride,
            string fileName,
            string callerMemberName)
        {
            // Validate Parameters
            if (channelInputs == null) throw new ArgumentNullException(nameof(channelInputs));
            if (channelInputs.Count == 0) throw new ArgumentException("channels.Count == 0", nameof(channelInputs));
            if (channelInputs.Contains(null)) throw new ArgumentException("channels.Contains(null)", nameof(channelInputs));
            if (duration == default) duration = 1;
            if (volume == default) volume = 1;
            fileName = ResolveFileName(fileName, audioFileFormatEnum, callerMemberName);

            WriteLine();
            WriteLine(GetPrettyTitle(fileName));
            WriteLine();
            
            // Validate Input Data
            var warnings = new List<string>();
            foreach (Outlet channelInput in channelInputs)
            {
                channelInput.Assert();
                warnings.AddRange(channelInput.GetWarnings());
            }

            // Configure AudioFileOutput
            AudioFileOutput audioFileOutput = _audioFileOutputManager.CreateAudioFileOutput();
            audioFileOutput.Duration = duration;
            audioFileOutput.FilePath = fileName;
            audioFileOutput.Amplifier = volume * sampleDataTypeEnum.GetMaxAmplitude();
            audioFileOutput.SamplingRate = ResolveSamplingRate(samplingRateOverride);
            audioFileOutput.SetSampleDataTypeEnum(sampleDataTypeEnum);
            audioFileOutput.SetAudioFileFormatEnum(audioFileFormatEnum);
            audioFileOutput.SetSpeakerSetup_WithSideEffects(channelInputs.Count);
            for (int i = 0; i < channelInputs.Count; i++)
            {
                audioFileOutput.AudioFileOutputChannels[i].Outlet = channelInputs[i];
            }

            // Validate AudioFileOutput
            audioFileOutput.Assert();
            warnings.AddRange(audioFileOutput.GetWarnings());

            // Calculate
            var calculator = AudioFileOutputCalculatorFactory.CreateAudioFileOutputCalculator(audioFileOutput);
            var stopWatch = Stopwatch.StartNew();
            calculator.Execute();
            stopWatch.Stop();

            // Report
            {
                // Get Info
                var channelStrings = new List<string>();
                int complexity = 0;

                foreach (var audioFileOutputChannel in audioFileOutput.AudioFileOutputChannels)
                {
                    string stringify = audioFileOutputChannel.Outlet?.Stringify() ?? "";
                    channelStrings.Add(stringify);

                    int stringifyLines = CountLines(stringify);
                    complexity += stringifyLines;
                }

                // Gather Lines
                var lines = new List<string> { "" };

                string realTimeMessage = FormatRealTimeMessage(duration, stopWatch);
                string sep = realTimeMessage != default ? ", " : "";
                
                lines.Add($"{realTimeMessage}{sep}Complexity: {complexity}");
                lines.Add("");

                lines.Add($"Calculation time: {stopWatch.Elapsed.TotalSeconds:F3}s");
                lines.Add($"Output file: {Path.GetFullPath(audioFileOutput.FilePath)}");
                lines.Add("");

                if (warnings.Any())
                {
                    lines.Add("Warnings:");
                    lines.AddRange(warnings.Select(warning => $"- {warning}"));
                    lines.Add("");
                }

                for (var i = 0; i <  audioFileOutput.AudioFileOutputChannels.Count; i++)
                {
                    var audioFileOutputChannel = audioFileOutput.AudioFileOutputChannels[i];
                    var channelString = channelStrings[i];

                    lines.Add($"Calculation Channel {audioFileOutputChannel.GetSpeakerSetupChannel().Channel.Name}:");
                    lines.Add("");
                    lines.Add(channelString);
                    lines.Add("");
                }
                
                // Write Lines
                lines.ForEach(WriteLine);
            }

            var result = warnings.ToResult(audioFileOutput);

            return result;
        }
        
        // Helpers
        
        private (Func<Outlet> func, double duration) ApplyLeadingSilence(Func<Outlet> func, double duration = default)
        {
            if (duration == default) duration = 1;
            
            double duration2 = duration + ConfigHelper.PlayLeadingSilence + ConfigHelper.PlayTrailingSilence;
                
            if (ConfigHelper.PlayLeadingSilence == 0)
            {
                return (func, duration2);
            }

            Outlet func2() => Delay(func(), _[ConfigHelper.PlayLeadingSilence]);
            return (func2, duration2);
        }

        private void PlayIfAllowed(AudioFileOutput audioFileOutput)
        {
            if (ToolingHelper.PlayAllowed(audioFileOutput.GetFileExtension()))
            {
                WriteLine("Playing audio...");
                new SoundPlayer(audioFileOutput.FilePath).PlaySync();
                WriteLine();
            }

            WriteLine("Done.");
            WriteLine();
        }

        private string ResolveFileName(string fileName, AudioFileFormatEnum audioFileFormatEnum, string callerMemberName)
        {
            string fileExtension = audioFileFormatEnum.GetFileExtension();

            if (string.IsNullOrWhiteSpace(fileName))
            {
                return $"{callerMemberName}{fileExtension}";
            }

            if (!fileName.EndsWith(fileExtension))
            {
                fileName += fileExtension;
            }

            return fileName;
        }

        private int ResolveSamplingRate(int samplingRateOverride)
        {
            if (samplingRateOverride != default)
            {
                WriteLine($"Sampling rate override: {samplingRateOverride}");
                return samplingRateOverride;
            }

            {
                int? samplingRate = ToolingHelper.TryGetSamplingRateForNCrunch();
                if (samplingRate != null)
                {
                    WriteLine($"Sampling rate: {samplingRate}");
                    return samplingRate.Value;
                }
            }

            {
                int? samplingRate = ToolingHelper.TryGetSamplingRateForAzurePipelines();
                if (samplingRate != null)
                {
                    WriteLine($"Sampling rate: {samplingRate}");
                    return samplingRate.Value;
                }
            }

            WriteLine($"Sampling rate: {ConfigHelper.DefaultSamplingRate}");
            return ConfigHelper.DefaultSamplingRate;
        }
        
        private static string FormatRealTimeMessage(double duration, Stopwatch stopWatch)
        {
            if (!ToolingHelper.IsRunningInTooling)
            {
                double realTimePercent = duration / stopWatch.Elapsed.TotalSeconds * 100;
                
                string realTimeStatusGlyph;
                if (realTimePercent < 100)
                {
                    realTimeStatusGlyph = "❌";
                }
                else
                { 
                    realTimeStatusGlyph = "✔️";
                }

                var realTimeMessage = $"{realTimeStatusGlyph} {realTimePercent:F0} % Real Time";

                return realTimeMessage;
            }

            return default;
        }
    }
}