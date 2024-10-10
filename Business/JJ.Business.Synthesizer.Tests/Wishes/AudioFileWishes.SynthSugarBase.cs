using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation.AudioFileOutputs;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Warnings;
using JJ.Business.Synthesizer.Warnings.Entities;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
// ReSharper disable LocalizableElement
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable AssignmentInsteadOfDiscard
// ReSharper disable MemberCanBePrivate.Global

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

        /// <summary>
        /// Wraps up a test and outputs the result to a file.
        /// Also, the entity data tied to the outlet will be verified.
        /// </summary>
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

            OptimizeForCodeCoverage(audioFileOutput);

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

        private void OptimizeForCodeCoverage(AudioFileOutput audioFileOutput)
        {
            // Lower sampling rate for NCrunch
            int samplingRateForCodeCoverage = 1000;
            if (Environment.GetEnvironmentVariable("NCrunch") != null)
            {
                Console.WriteLine($"Setting samplingrate to {samplingRateForCodeCoverage} to improve NCrunch code coverage performance.");
                audioFileOutput.SamplingRate = samplingRateForCodeCoverage;
            }
        }
    }
}