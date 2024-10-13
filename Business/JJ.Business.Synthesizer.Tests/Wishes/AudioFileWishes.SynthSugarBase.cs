using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation.AudioFileOutputs;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Warnings;
using JJ.Business.Synthesizer.Warnings.Entities;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Tests.Wishes
{
    public partial class SynthSugarBase
    {
        private void InitializeAudioFileOutputWishes(IContext context)
            => _audioFileOutputManager = TestHelper.CreateAudioFileOutputManager(context);

        private AudioFileOutputManager _audioFileOutputManager;

        private string NewLine => Environment.NewLine;
        
        /// <inheritdoc cref="_savewavdocs"/>
        public void SaveWav(
            Outlet monoChannel,
            double? duration = default,
            double? volume = default,
            string fileName = null,
            [CallerMemberName] string callerMemberName = null)
            => SaveWav(new[] { monoChannel }, duration, volume, fileName, callerMemberName);
        
        /// <inheritdoc cref="_savewavdocs"/>
        public void SaveWav(
            (Outlet left, Outlet right) channels,
            double? duration = default,
            double? volume = default,
            string fileName = null,
            [CallerMemberName] string callerMemberName = null)
            => SaveWav(new[] { channels.left, channels.right }, duration, volume, fileName, callerMemberName);
        
        /// <inheritdoc cref="_savewavdocs"/>
        private void SaveWav(
            IList<Outlet> channels,
            double? duration = default,
            double? volume = default,
            string fileName = null,
            string callerMemberName = null)
        {
            Console.WriteLine();

            // Validate Parameters
            if (channels == null) throw new NullException(() => channels);
            if (channels.Count == 0) throw new ArgumentException("channels.Count == 0", nameof(channels));
            if (channels.Contains(null)) throw new ArgumentException("channels.Contains(null)", nameof(channels));
            
            fileName = string.IsNullOrWhiteSpace(fileName) ? $"{callerMemberName}.wav" : fileName;
            duration = duration ?? ConfigurationHelper.DefaultOutputDuration;
            volume = volume ?? ConfigurationHelper.DefaultOutputVolume;

            // Validate Input Data
            var warnings = new List<string>();
            foreach (Outlet channel in channels)
            {
                new RecursiveOperatorValidator(channel.Operator).Verify();
                new RecursiveOperatorValidator(channel.Operator).Verify();
                IValidator warningValidator = new RecursiveOperatorWarningValidator(channel.Operator);
                warnings.AddRange(warningValidator.ValidationMessages.Select(x => x.Text));
            }

            // Configure AudioFileOutput
            AudioFileOutput audioFileOutput = _audioFileOutputManager.CreateAudioFileOutput();
            _audioFileOutputManager.SetSpeakerSetup(audioFileOutput, (SpeakerSetupEnum)channels.Count);
            audioFileOutput.Duration = duration.Value;
            audioFileOutput.SamplingRate = ConfigurationHelper.DefaultSamplingRate;
            audioFileOutput.Amplifier = short.MaxValue * volume.Value;
            audioFileOutput.FilePath = fileName;

            for (int i = 0; i < channels.Count; i++)
            {
                audioFileOutput.AudioFileOutputChannels[i].Outlet = channels[i];
            }

            OptimizeForTooling(audioFileOutput);

            // Validate AudioFileOutput
            _audioFileOutputManager.ValidateAudioFileOutput(audioFileOutput).Verify();
            warnings.AddRange(new AudioFileOutputWarningValidator(audioFileOutput).ValidationMessages.Select(x => x.Text));

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

            Console.WriteLine(calculationTimeText + outputFileText + warningText);
        }

        /// <summary>
        /// Optimizes the given <see cref="AudioFileOutput"/> for tooling environments such as NCrunch and Azure Pipelines.
        /// It can do this by lowering the audio sampling rate for instance.
        /// </summary>
        /// <param name="audioFileOutput">The <see cref="AudioFileOutput"/> to be optimized.</param>
        private void OptimizeForTooling(AudioFileOutput audioFileOutput)
        {
            if (Environment.GetEnvironmentVariable("NCrunch") != null)
            {
                audioFileOutput.SamplingRate = ConfigurationHelper.NCrunchSamplingRate;
                Console.WriteLine($"Setting sampling rate to {audioFileOutput.SamplingRate} " +
                                  "to improve NCrunch code coverage performance.");
            }

            if (Environment.GetEnvironmentVariable("TF_BUILD") == "True")
            {
                audioFileOutput.SamplingRate = ConfigurationHelper.AzurePipelinesSamplingRate;
                Console.WriteLine($"Setting sampling rate to {audioFileOutput.SamplingRate} " + 
                                  "to improve Azure Pipelines test performance.");
            }
        }

        #region Docs

        #pragma warning disable CS0169 // Field is never used
        // ReSharper disable once IdentifierTypo

        /// <summary>
        /// Outputs audio to a file. Also, the entity data tied to the outlet will be verified.
        /// If parameters are not provided, defaults will be employed.
        /// Some of these defaults you can set in the configuration file.
        /// </summary>
        object _savewavdocs;

        #endregion
    }
}