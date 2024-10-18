using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Calculation.AudioFileOutputs;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Warnings.Entities;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Common;
using JJ.Framework.Configuration;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Enums.ChannelEnum;

namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        // Want my static usings, but clashes with System type names.
        public readonly SampleDataTypeEnum Int16  = SampleDataTypeEnum.Int16;
        public readonly SampleDataTypeEnum Byte   = SampleDataTypeEnum.Byte;
        public readonly ChannelEnum        Single = ChannelEnum.Single;

        private void InitializeAudioFileOutputWishes(IContext context)
        {
            _interpolationTypeRepository = PersistenceHelper.CreateRepository<IInterpolationTypeRepository>(context);
            _audioFileOutputManager      = ServiceFactory.CreateAudioFileOutputManager(context);
        }

        private static readonly ConfigurationSection _configuration = CustomConfigurationManager.GetSection<ConfigurationSection>();

        private IInterpolationTypeRepository _interpolationTypeRepository;
        private AudioFileOutputManager       _audioFileOutputManager;

        private string NewLine => Environment.NewLine;

        /// <summary>
        /// Creates a Sample by reading the file at the given <paramref name="filePath" />.
        /// Sets the <see cref=Sample.Amplifier" /> to scale values to range between -1 and +1.
        /// </summary>
        /// <param name="filePath">The file path of the audio sample to load.</param>
        /// <returns><see cref="SampleOperatorWrapper"/>  that can be used as an <see cref="Outlet"/> too.</returns>
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
                        return SaveAudio(
                            new[] { monoOutlet }, 
                            duration, volume, 
                            sampleDataTypeEnum, audioFileFormatEnum, 
                            samplingRateOverride, fileName, callerMemberName);

                    case SpeakerSetupEnum.Stereo:
                        Channel = Left ; var leftOutlet  = func();
                        Channel = Right; var rightOutlet = func();
                        return SaveAudio(
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
        private Result<AudioFileOutput> SaveAudio(
            IList<Outlet> channels,
            double duration,
            double volume,
            SampleDataTypeEnum sampleDataTypeEnum,
            AudioFileFormatEnum audioFileFormatEnum,
            int samplingRateOverride,
            string fileName,
            string callerMemberName)
        {
            Console.WriteLine();

            // Validate Parameters
            if (channels == null) throw new ArgumentNullException(nameof(channels));
            if (channels.Count == 0) throw new ArgumentException("channels.Count == 0",         nameof(channels));
            if (channels.Contains(null)) throw new ArgumentException("channels.Contains(null)", nameof(channels));
            if (duration == default) duration = _[1];
            if (volume == default) volume = _[1];

            fileName = ResolveFileName(fileName, audioFileFormatEnum, callerMemberName);

            // Validate Input Data
            var warnings = new List<string>();
            foreach (Outlet channel in channels)
            {
                channel.Assert();
                warnings.AddRange(channel.GetWarnings());
            }
            
            // Configure AudioFileOutput
            AudioFileOutput audioFileOutput = _audioFileOutputManager.CreateAudioFileOutput();
            {
                audioFileOutput.Duration     = duration;
                audioFileOutput.FilePath     = fileName;
                audioFileOutput.Amplifier    = volume * sampleDataTypeEnum.GetMaxAmplitude();
                audioFileOutput.SetSampleDataTypeEnum(sampleDataTypeEnum, _sampleDataTypeRepository);
                audioFileOutput.SetAudioFileFormatEnum(audioFileFormatEnum, _audioFileFormatRepository);

                _audioFileOutputManager.SetSpeakerSetup(audioFileOutput, (SpeakerSetupEnum)channels.Count);
                for (int i = 0; i < channels.Count; i++)
                {
                    audioFileOutput.AudioFileOutputChannels[i].Outlet = channels[i];
                }

                SetSamplingRate(audioFileOutput, samplingRateOverride);
            }

            // Validate AudioFileOutput
            _audioFileOutputManager.ValidateAudioFileOutput(audioFileOutput).Verify();
            warnings.AddRange(new AudioFileOutputWarningValidator(audioFileOutput).ValidationMessages.Select(x => x.Text));

            // Calculate
            var calculator = AudioFileOutputCalculatorFactory.CreateAudioFileOutputCalculator(audioFileOutput);
            var stopWatch  = Stopwatch.StartNew();
            calculator.Execute();
            stopWatch.Stop();

            // Report
            var calculationTimeText = $"Calculation time: {stopWatch.Elapsed.TotalSeconds:F3}s{NewLine}";
            var outputFileText      = $"Output file: {Path.GetFullPath(audioFileOutput.FilePath)}";
            string warningText = warnings.Count == 0 ? "" : $"{NewLine}{NewLine}Warnings:{NewLine}" +
                                                            $"{string.Join(NewLine, warnings.Select(x => $"- {x}"))}";

            Console.WriteLine(calculationTimeText + outputFileText + warningText);

            var result = warnings.ToResult(audioFileOutput);
            
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

        // Helpers
        
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
        
        private void SetSamplingRate(AudioFileOutput audioFileOutput, int samplingRateOverride)
        {
            if (samplingRateOverride != default)
            {
                audioFileOutput.SamplingRate = samplingRateOverride;
                Console.WriteLine($"Sampling rate override = {audioFileOutput.SamplingRate}.");
                return;
            }

            audioFileOutput.SamplingRate = _configuration.DefaultSamplingRate;

            SetSamplingRateForTooling(audioFileOutput);
        }

        /// <summary>
        /// Optimizes the given <see cref="AudioFileOutput" /> for tooling environments such as NCrunch and Azure Pipelines.
        /// It can do this by lowering the audio sampling rate for instance.
        /// </summary>
        /// <param name="audioFileOutput"> The <see cref="AudioFileOutput" /> to be optimized. </param>
        private void SetSamplingRateForTooling(AudioFileOutput audioFileOutput)
        {
            if (Environment.GetEnvironmentVariable("NCrunch") != null)
            {
                audioFileOutput.SamplingRate = _configuration.NCrunchSamplingRate;

                if (CurrentTestIsInCategory(_configuration.LongRunningTestCategory))
                {
                    audioFileOutput.SamplingRate = _configuration.NCrunchSamplingRateLongRunning;
                }

                Console.WriteLine($"Setting sampling rate to {audioFileOutput.SamplingRate} " +
                                  "to improve performance of NCrunch code coverage.");
            }

            if (Environment.GetEnvironmentVariable("TF_BUILD") == "True")
            {
                audioFileOutput.SamplingRate = _configuration.AzurePipelinesSamplingRate;

                if (CurrentTestIsInCategory(_configuration.LongRunningTestCategory))
                {
                    audioFileOutput.SamplingRate = _configuration.AzurePipelinesSamplingRateLongRunning;
                }

                Console.WriteLine($"Setting sampling rate to {audioFileOutput.SamplingRate} " +
                                  "to improve performance of Azure Pipelines tests.");
            }
        }

        private bool CurrentTestIsInCategory(string category) =>
            new StackTrace().GetFrames()?
                            .Select(x => x.GetMethod())
                            .SelectMany(method => method.GetCustomAttributes(typeof(TestCategoryAttribute), true))
                            .OfType<TestCategoryAttribute>()
                            .Any(x => x.TestCategories.Contains(category)) ?? false;
    }
}