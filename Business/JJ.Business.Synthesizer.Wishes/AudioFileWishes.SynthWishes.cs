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
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using static System.Console;
using static System.Environment;
using static JJ.Business.Synthesizer.Enums.ChannelEnum;

namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        // Want my static usings, but clashes with System type names.
        public readonly SampleDataTypeEnum Int16 = SampleDataTypeEnum.Int16;
        public readonly SampleDataTypeEnum Byte = SampleDataTypeEnum.Byte;
        public readonly ChannelEnum Single = ChannelEnum.Single;

        private void InitializeAudioFileOutputWishes(IContext context)
        {
            _interpolationTypeRepository = PersistenceHelper.CreateRepository<IInterpolationTypeRepository>(context);
            _audioFileOutputManager = ServiceFactory.CreateAudioFileOutputManager(context);
        }

        private IInterpolationTypeRepository _interpolationTypeRepository;
        private AudioFileOutputManager _audioFileOutputManager;

        /// <summary>
        /// Creates a Sample by reading the file at the given <paramref name="filePath" />.
        /// Sets the <see cref= Sample.Amplifier" /> to scale values to range between -1 and +1.
        /// 
        /// </summary>
        /// <param name="filePath"> The file path of the audio sample to load. </param>
        /// <returns> <see cref="SampleOperatorWrapper" />  that can be used as an <see cref="Outlet" /> too. </returns>
        public SampleOperatorWrapper Sample(
            string filePath,
            InterpolationTypeEnum interpolationTypeEnum = InterpolationTypeEnum.Line)
        {
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Sample sample = Samples.CreateSample(stream);
                sample.Location = Path.GetFullPath(filePath);
                sample.Name = Path.GetFileNameWithoutExtension(filePath);
                sample.SetInterpolationTypeEnum(interpolationTypeEnum, _interpolationTypeRepository);
                sample.Amplifier = 1.0 / sample.SampleDataType.GetMaxAmplitude();

                SampleOperatorWrapper wrapper = Sample(sample);
                ((Outlet)wrapper).Operator.Name = sample.Name;

                return wrapper;
            }
        }

        public Result<AudioFileOutput> Play(
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
            var result =
                SaveAudio(
                    func, duration, volume,
                    speakerSetupEnum, sampleDataTypeEnum, audioFileFormatEnum, samplingRateOverride,
                    fileName, callerMemberName);
            
            PlayAudioConditionally(result.Data);
            
            return result;
        }
        
        public Result<AudioFileOutput> PlayMono(
            Func<Outlet> func,
            double duration = default,
            double volume = default,
            SampleDataTypeEnum sampleDataTypeEnum = SampleDataTypeEnum.Int16,
            AudioFileFormatEnum audioFileFormatEnum = AudioFileFormatEnum.Wav,
            int samplingRateOverride = default,
            string fileName = default,
            [CallerMemberName] string callerMemberName = null)
        {
            var result =
                SaveAudioMono(
                    func, duration, volume,
                    sampleDataTypeEnum, audioFileFormatEnum, samplingRateOverride,
                    fileName, callerMemberName);
            
            PlayAudioConditionally(result.Data);

            return result;
        }
        
        
        /// <inheritdoc cref="docs._saveaudio" />
        public Result<AudioFileOutput> SaveAudioMono(
            Func<Outlet> func,
            double duration = default,
            double volume = default,
            SampleDataTypeEnum sampleDataTypeEnum = SampleDataTypeEnum.Int16,
            AudioFileFormatEnum audioFileFormatEnum = AudioFileFormatEnum.Wav,
            int samplingRateOverride = default,
            string fileName = default,
            [CallerMemberName] string callerMemberName = null)
            => SaveAudio(func,
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
            if (duration == default) duration = _[1];
            if (volume == default) volume = _[1];
            fileName = ResolveFileName(fileName, audioFileFormatEnum, callerMemberName);

            WriteLine();
            WriteLine(NameHelper.GetPrettyTitle(fileName));
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
            _audioFileOutputManager.SetSpeakerSetup(audioFileOutput, (SpeakerSetupEnum)channelInputs.Count);
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
            var calculationTimeText = $"Calculation time: {stopWatch.Elapsed.TotalSeconds:F3}s{NewLine}";
            var outputFileText = $"Output file: {Path.GetFullPath(audioFileOutput.FilePath)}";
            string warningText = warnings.Count == 0 ? "" : $"{NewLine}{NewLine}Warnings:{NewLine}" +
                                                            $"{string.Join(NewLine, warnings.Select(x => $"- {x}"))}";

            WriteLine(calculationTimeText + outputFileText + warningText);
            WriteLine();

            foreach (AudioFileOutputChannel audioFileOutputChannel in audioFileOutput.AudioFileOutputChannels)
            {
                WriteLine($"Calculation Channel {audioFileOutputChannel.GetSpeakerSetupChannel().Channel.Name}:");
                WriteLine();
                WriteLine(audioFileOutputChannel.Outlet?.Stringify());
                WriteLine();
            }

            var result = warnings.ToResult(audioFileOutput);

            return result;
        }

        // Helpers
        
        private void PlayAudioConditionally(AudioFileOutput audioFileOutput)
        {
            if (ToolingHelper.PlayAudioAllowed(audioFileOutput.GetFileExtension()))
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
    }
}