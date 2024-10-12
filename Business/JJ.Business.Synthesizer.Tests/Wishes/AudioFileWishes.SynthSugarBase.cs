using System;
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
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Tests.Wishes
{
    public partial class SynthSugarBase
    {
        private void InitializeAudioFileOutputWishes(IContext context)
            => _audioFileOutputManager = TestHelper.CreateAudioFileOutputManager(context);

        private AudioFileOutputManager _audioFileOutputManager;

        private const double DEFAULT_TOTAL_VOLUME = 0.5;
        private const double DEFAULT_TOTAL_TIME = 3.0;
        private string NewLine => Environment.NewLine;
        
        /// <inheritdoc cref="_savewavdocs"/>
        public void SaveWav(
            Outlet outlet,
            double duration = DEFAULT_TOTAL_TIME,
            double volume = DEFAULT_TOTAL_VOLUME,
            string fileName = null,
            [CallerMemberName] string callerMemberName = null)
        {
            // Validate Parameters
            if (outlet == null) throw new ArgumentNullException(nameof(outlet));
            fileName = string.IsNullOrWhiteSpace(fileName) ? $"{callerMemberName}.wav" : fileName;

            // Validate Data
            new RecursiveOperatorValidator(outlet.Operator).Verify();
            var warnings = new RecursiveOperatorWarningValidator(outlet.Operator).ValidationMessages.Select(x => x.Text).ToList();

            // Configure AudioFileOutput
            AudioFileOutput audioFileOutput = _audioFileOutputManager.CreateAudioFileOutput();
            audioFileOutput.Duration = duration;
            audioFileOutput.Amplifier = short.MaxValue * volume;
            audioFileOutput.FilePath = fileName;
            audioFileOutput.AudioFileOutputChannels[0].Outlet = outlet;

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

        /// <inheritdoc cref="_savewavdocs"/>
        public void SaveWav(
            (Outlet left, Outlet right) channels,
            double duration = DEFAULT_TOTAL_TIME,
            double volume = DEFAULT_TOTAL_VOLUME,
            string fileName = null,
            [CallerMemberName] string callerMemberName = null)
        {
            Console.WriteLine();

            // Validate Parameters
            if (channels.left == null) throw new NullException(() => channels.left);
            if (channels.right == null) throw new NullException(() => channels.right);
            fileName = string.IsNullOrWhiteSpace(fileName) ? $"{callerMemberName}.wav" : fileName;

            // Validate Data
            new RecursiveOperatorValidator(channels.left.Operator).Verify();
            new RecursiveOperatorValidator(channels.right.Operator).Verify();
            var warnings = 
                new RecursiveOperatorWarningValidator(channels.left.Operator).ValidationMessages.Union(
                new RecursiveOperatorWarningValidator(channels.right.Operator).ValidationMessages).Select(x => x.Text).ToList();

            // Configure AudioFileOutput
            AudioFileOutput audioFileOutput = _audioFileOutputManager.CreateAudioFileOutput();
            _audioFileOutputManager.SetSpeakerSetup(audioFileOutput, SpeakerSetupEnum.Stereo);
            audioFileOutput.Duration = duration;
            audioFileOutput.Amplifier = short.MaxValue * volume;
            audioFileOutput.FilePath = fileName;
            audioFileOutput.AudioFileOutputChannels[0].Outlet = channels.left;
            audioFileOutput.AudioFileOutputChannels[1].Outlet = channels.right;

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
                audioFileOutput.SamplingRate = 1000;
                Console.WriteLine($"Setting sampling rate to {audioFileOutput.SamplingRate} " +
                                  "to improve NCrunch code coverage performance.");
            }

            if (Environment.GetEnvironmentVariable("TF_BUILD") == "True")
            {
                audioFileOutput.SamplingRate = 11025;
                Console.WriteLine($"Setting sampling rate to {audioFileOutput.SamplingRate} " + 
                                  "to improve Azure Pipelines test performance.");
            }
        }

        #region Docs

        #pragma warning disable CS0169 // Field is never used
        // ReSharper disable once IdentifierTypo

        /// <summary>
        /// Outputs audio to a file. Also, the entity data tied to the outlet will be verified.
        /// </summary>
        object _savewavdocs;

        #endregion
    }
}