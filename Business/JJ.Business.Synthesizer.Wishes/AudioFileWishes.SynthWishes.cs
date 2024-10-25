using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Calculation.AudioFileOutputs;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using static System.Console;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Enums.ChannelEnum;
using static JJ.Business.Synthesizer.Enums.SampleDataTypeEnum;
using static JJ.Business.Synthesizer.Enums.SpeakerSetupEnum;
using static JJ.Business.Synthesizer.Wishes.Helpers.FrameworkWishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.NameHelper;
// ReSharper disable UseObjectOrCollectionInitializer

namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        // Want my static usings, but clashes with System type names.
        public const SampleDataTypeEnum Int16 = SampleDataTypeEnum.Int16;
        public const SampleDataTypeEnum Byte = SampleDataTypeEnum.Byte;
        public const ChannelEnum Single = ChannelEnum.Single;

        private void InitializeAudioFileWishes(IContext context)
        {
            _audioFileOutputManager = ServiceFactory.CreateAudioFileOutputManager(context);
            _sampleManager = ServiceFactory.CreateSampleManager(context);
        }

        private AudioFileOutputManager _audioFileOutputManager;
        private SampleManager _sampleManager;

        // Sample
        
        /// <inheritdoc cref="docs._sample"/>
        public FluentOutlet Sample(
            byte[] bytes, 
            InterpolationTypeEnum interpolationTypeEnum = default,
            double amplifier = 1, double speedFactor = 1, int bytesToSkip = 0)
            => SampleBase(new MemoryStream(bytes), default, interpolationTypeEnum, amplifier, speedFactor, bytesToSkip);
        
        /// <inheritdoc cref="docs._sample"/>
        public FluentOutlet Sample(
            Stream stream,
            InterpolationTypeEnum interpolationTypeEnum = default,
            double amplifier = 1, double speedFactor = 1, int bytesToSkip = 0)
            => SampleBase(stream, default, interpolationTypeEnum, amplifier, speedFactor, bytesToSkip);

        /// <inheritdoc cref="docs._sample"/>
        public FluentOutlet Sample(
            string filePath,
            InterpolationTypeEnum interpolationTypeEnum = default,
            double amplifier = 1, double speedFactor = 1, int bytesToSkip = 0)
        {
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                return SampleBase(stream, filePath, interpolationTypeEnum, amplifier, speedFactor, bytesToSkip);
        }

        /// <inheritdoc cref="docs._sample"/>
        private FluentOutlet SampleBase(
            Stream stream, string filePath,
            InterpolationTypeEnum interpolationTypeEnum,
            double amplifier, double speedFactor, int bytesToSkip)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            if (interpolationTypeEnum == default) interpolationTypeEnum = InterpolationTypeEnum.Line;
            
            Sample sample = _sampleManager.CreateSample(stream);
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
            Outlet outlet = wrapper.Result;
            outlet.Operator.Name = sample.Name;

            return _[outlet];
        }

        // Play

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<AudioFileOutput> Play(
            Func<Outlet> outletFunc,
            double duration,
            double volume,
            SpeakerSetupEnum speakerSetupEnum = Stereo,
            SampleDataTypeEnum sampleDataTypeEnum = Int16,
            AudioFileFormatEnum audioFileFormatEnum = Wav,
            int samplingRateOverride = default,
            string fileName = default,
            [CallerMemberName] string callerMemberName = null)
            => Play(
                outletFunc, _[duration], _[volume], 
                speakerSetupEnum, sampleDataTypeEnum, audioFileFormatEnum, samplingRateOverride, 
                fileName, callerMemberName);
        
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Result<AudioFileOutput> Play(
            Func<Outlet> outletFunc, Outlet duration, double volume,
            SpeakerSetupEnum speakerSetupEnum = Stereo,
            SampleDataTypeEnum sampleDataTypeEnum = Int16,
            AudioFileFormatEnum audioFileFormatEnum = Wav,
            int samplingRateOverride = default,
            string fileName = default, [CallerMemberName] string callerMemberName = null)
            => Play(
                outletFunc, _[duration], _[volume], 
                speakerSetupEnum, sampleDataTypeEnum, audioFileFormatEnum, samplingRateOverride, 
                fileName, callerMemberName);
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Result<AudioFileOutput> Play(
            Func<Outlet> outletFunc, double duration, Outlet volume = null,
            SpeakerSetupEnum speakerSetupEnum = Stereo,
            SampleDataTypeEnum sampleDataTypeEnum = Int16,
            AudioFileFormatEnum audioFileFormatEnum = Wav,
            int samplingRateOverride = default,
            string fileName = default, [CallerMemberName] string callerMemberName = null)
            => Play(
                outletFunc, _[duration], _[volume], 
                speakerSetupEnum, sampleDataTypeEnum, audioFileFormatEnum, samplingRateOverride, 
                fileName, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<AudioFileOutput> Play(
            Func<Outlet> outletFunc, Outlet duration = default, Outlet volume = default,
            SpeakerSetupEnum speakerSetupEnum = Stereo,
            SampleDataTypeEnum sampleDataTypeEnum = Int16,
            AudioFileFormatEnum audioFileFormatEnum = Wav,
            int samplingRateOverride = default,
            string fileName = default, [CallerMemberName] string callerMemberName = null)
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

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<AudioFileOutput> PlayMono(
            Func<Outlet> outletFunc, double duration, double volume,
            SampleDataTypeEnum sampleDataTypeEnum = Int16,
            AudioFileFormatEnum audioFileFormatEnum = Wav,
            int samplingRateOverride = default,
            string fileName = default, [CallerMemberName] string callerMemberName = null)
            => PlayMono(
                outletFunc, _[duration], _[volume],
                sampleDataTypeEnum, audioFileFormatEnum, samplingRateOverride,
                fileName, callerMemberName);
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Result<AudioFileOutput> PlayMono(
            Func<Outlet> outletFunc, Outlet duration, double volume,
            SampleDataTypeEnum sampleDataTypeEnum = Int16,
            AudioFileFormatEnum audioFileFormatEnum = Wav,
            int samplingRateOverride = default,
            string fileName = default, [CallerMemberName] string callerMemberName = null)
            => PlayMono(
                outletFunc, _[duration], _[volume],
                sampleDataTypeEnum, audioFileFormatEnum, samplingRateOverride,
                fileName, callerMemberName);
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Result<AudioFileOutput> PlayMono(
            Func<Outlet> outletFunc, double duration, Outlet volume = default,
            SampleDataTypeEnum sampleDataTypeEnum = Int16,
            AudioFileFormatEnum audioFileFormatEnum = Wav,
            int samplingRateOverride = default,
            string fileName = default, [CallerMemberName] string callerMemberName = null)
            => PlayMono(
                outletFunc, _[duration], _[volume],
                sampleDataTypeEnum, audioFileFormatEnum, samplingRateOverride,
                fileName, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<AudioFileOutput> PlayMono(
            Func<Outlet> outletFunc, Outlet duration = default, Outlet volume = default,
            SampleDataTypeEnum sampleDataTypeEnum = Int16,
            AudioFileFormatEnum audioFileFormatEnum = Wav,
            int samplingRateOverride = default,
            string fileName = default, [CallerMemberName] string callerMemberName = null)
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

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<AudioFileOutput> SaveAudioMono(
            Func<Outlet> outletFunc, double duration, double volume,
            SampleDataTypeEnum sampleDataTypeEnum = Int16,
            AudioFileFormatEnum audioFileFormatEnum = Wav,
            int samplingRateOverride = default,
            string fileName = default, [CallerMemberName] string callerMemberName = null)
            => SaveAudioMono(
                outletFunc, _[duration], _[volume],
                sampleDataTypeEnum, audioFileFormatEnum, samplingRateOverride,
                fileName, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<AudioFileOutput> SaveAudioMono(
            Func<Outlet> outletFunc, Outlet duration, double volume,
            SampleDataTypeEnum sampleDataTypeEnum = Int16,
            AudioFileFormatEnum audioFileFormatEnum = Wav,
            int samplingRateOverride = default,
            string fileName = default, [CallerMemberName] string callerMemberName = null)
            => SaveAudioMono(
                outletFunc, _[duration], _[volume],
                sampleDataTypeEnum, audioFileFormatEnum, samplingRateOverride,
                fileName, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<AudioFileOutput> SaveAudioMono(
            Func<Outlet> outletFunc, double duration, Outlet volume = default,
            SampleDataTypeEnum sampleDataTypeEnum = Int16,
            AudioFileFormatEnum audioFileFormatEnum = Wav,
            int samplingRateOverride = default,
            string fileName = default, [CallerMemberName] string callerMemberName = null)
            => SaveAudioMono(
                outletFunc, _[duration], _[volume],
                sampleDataTypeEnum, audioFileFormatEnum, samplingRateOverride,
                fileName, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<AudioFileOutput> SaveAudioMono(
            Func<Outlet> outletFunc, Outlet duration = default, Outlet volume = default,
            SampleDataTypeEnum sampleDataTypeEnum = Int16,
            AudioFileFormatEnum audioFileFormatEnum = Wav,
            int samplingRateOverride = default,
            string fileName = default, [CallerMemberName] string callerMemberName = null)
            => SaveAudio(outletFunc,
                         duration, volume,
                         Mono, sampleDataTypeEnum, audioFileFormatEnum,
                         samplingRateOverride, fileName, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<AudioFileOutput> SaveAudio(
            Func<Outlet> func,
            double duration,
            double volume,
            SpeakerSetupEnum speakerSetupEnum = Stereo,
            SampleDataTypeEnum sampleDataTypeEnum = Int16,
            AudioFileFormatEnum audioFileFormatEnum = Wav,
            int samplingRateOverride = default,
            string fileName = default, [CallerMemberName] string callerMemberName = null)
            => SaveAudio(
                func, _[duration], _[volume],
                speakerSetupEnum, sampleDataTypeEnum, audioFileFormatEnum,
                samplingRateOverride, fileName, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<AudioFileOutput> SaveAudio(
            Func<Outlet> func,
            Outlet duration,
            double volume,
            SpeakerSetupEnum speakerSetupEnum = Stereo,
            SampleDataTypeEnum sampleDataTypeEnum = Int16,
            AudioFileFormatEnum audioFileFormatEnum = Wav,
            int samplingRateOverride = default,
            string fileName = default, [CallerMemberName] string callerMemberName = null)
            => SaveAudio(
                func, _[duration], _[volume],
                speakerSetupEnum, sampleDataTypeEnum, audioFileFormatEnum,
                samplingRateOverride, fileName, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<AudioFileOutput> SaveAudio(
            Func<Outlet> func,
            double duration,
            Outlet volume = null,
            SpeakerSetupEnum speakerSetupEnum = Stereo,
            SampleDataTypeEnum sampleDataTypeEnum = Int16,
            AudioFileFormatEnum audioFileFormatEnum = Wav,
            int samplingRateOverride = default,
            string fileName = default, [CallerMemberName] string callerMemberName = null)
            => SaveAudio(
                func, _[duration], _[volume],
                speakerSetupEnum, sampleDataTypeEnum, audioFileFormatEnum,
                samplingRateOverride, fileName, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<AudioFileOutput> SaveAudio(
            Func<Outlet> func,
            Outlet duration = default,
            Outlet volume = default,
            SpeakerSetupEnum speakerSetupEnum = Stereo,
            SampleDataTypeEnum sampleDataTypeEnum = Int16,
            AudioFileFormatEnum audioFileFormatEnum = Wav,
            int samplingRateOverride = default,
            string fileName = default, [CallerMemberName] string callerMemberName = null)
        {
            var originalChannel = Channel;
            try
            {
                switch (speakerSetupEnum)
                {
                    case Mono:
                        Channel = Single; var monoOutlet = func();
                        
                        return SaveAudioBase(
                            new[] { monoOutlet },
                            duration, volume,
                            sampleDataTypeEnum, audioFileFormatEnum,
                            samplingRateOverride, fileName, callerMemberName);

                    case Stereo:
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

        /// <inheritdoc cref="docs._saveorplay" />
        private Result<AudioFileOutput> SaveAudioBase(
            IList<Outlet> channelInputs,
            Outlet duration,
            Outlet volume,
            SampleDataTypeEnum sampleDataTypeEnum,
            AudioFileFormatEnum audioFileFormatEnum,
            int samplingRateOverride,
            string fileName, string callerMemberName)
        {
            // Validate Parameters
            if (channelInputs == null) throw new ArgumentNullException(nameof(channelInputs));
            if (channelInputs.Count == 0) throw new ArgumentException("channels.Count == 0", nameof(channelInputs));
            if (channelInputs.Contains(null)) throw new ArgumentException("channels.Contains(null)", nameof(channelInputs));
            
            duration = duration ?? _[1];
            double durationValue = duration.Calculate();
            if (durationValue == 0) durationValue = 1;

            volume = volume ?? _[1];
            double volumeValue = volume.Calculate();
            if (volumeValue == 0) volumeValue = 1;
            
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
            audioFileOutput.Duration = durationValue;
            audioFileOutput.FilePath = fileName;
            audioFileOutput.Amplifier = volumeValue * sampleDataTypeEnum.GetMaxAmplitude();
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

                string realTimeMessage = FormatRealTimeMessage(durationValue, stopWatch);
                string sep = realTimeMessage != default ? " | " : "";
                
                lines.Add($"{realTimeMessage}{sep}Complexity Ｏ ( {complexity} )");
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
        
        private (Func<Outlet> func, Outlet duration) ApplyLeadingSilence(Func<Outlet> func, Outlet duration = default)
        {
            if (duration == default) duration = _[1];
            
            Outlet duration2 = Add(duration, ConfigHelper.PlayLeadingSilence + ConfigHelper.PlayTrailingSilence);
                
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