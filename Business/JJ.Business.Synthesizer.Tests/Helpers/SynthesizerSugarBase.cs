﻿using JJ.Business.Synthesizer.Calculation.AudioFileOutputs;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Warnings;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System;
using System.Linq;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    public partial class SynthesizerSugarBase : OperatorFactory
    {
        private const double DEFAULT_TOTAL_VOLUME = 0.5;
        private const double DEFAULT_TOTAL_TIME = 3.0;
        private string NewLine => Environment.NewLine;

        protected readonly CurveFactory CurveFactory;
        protected readonly AudioFileOutputManager AudioFileOutputManager;

        protected SynthesizerSugarBase()
            : this(PersistenceHelper.CreateContext(), beat: 1, bar: 4)
        { }

        protected SynthesizerSugarBase(IContext context, double beat, double bar)
            : base(PersistenceHelper.CreateRepository<IOperatorRepository>(context),
                PersistenceHelper.CreateRepository<IInletRepository>(context),
                PersistenceHelper.CreateRepository<IOutletRepository>(context),
                PersistenceHelper.CreateRepository<ICurveInRepository>(context),
                PersistenceHelper.CreateRepository<IValueOperatorRepository>(context),
                PersistenceHelper.CreateRepository<ISampleOperatorRepository>(context))
        {
            CurveFactory = TestHelper.CreateCurveFactory(context);
            AudioFileOutputManager = TestHelper.CreateAudioFileOutputManager(context);

            _ = new ValueIndexer(this);
            Bar = new BarIndexer(this, bar);
            Beat = new BeatIndexer(this, beat);
            t = new TimeIndexer(this, bar, beat);
        }

        /// <inheritdoc cref="ValueIndexer"/>
        protected readonly ValueIndexer _;

        /// <inheritdoc cref="BarIndexer"/>
        protected BarIndexer Bar { get; }

        /// <inheritdoc cref="BeatIndexer"/>
        protected BeatIndexer Beat { get; }
        
        // ReSharper disable once InconsistentNaming
        /// <inheritdoc cref="TimeIndexer"/>
        protected TimeIndexer t { get; }

        /// <summary>
        /// Wraps up a test for FM synthesis and outputs the result to a file.
        /// Also, the entity data will be verified.
        /// </summary>
        protected void CreateAudioFile(
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
            
            var warningValidator = new RecursiveOperatorWarningValidator(outlet.Operator);
            IList<string> warnings = warningValidator.ValidationMessages.Select(x => x.Text).ToArray();

            // Configure AudioFileOutput
            AudioFileOutput audioFileOutput = AudioFileOutputManager.CreateAudioFileOutput();
            audioFileOutput.Duration = duration;
            audioFileOutput.Amplifier = short.MaxValue * volume;
            audioFileOutput.FilePath = fileName;
            audioFileOutput.AudioFileOutputChannels[0].Outlet = outlet;

            // Assert AudioFileOutput
            AudioFileOutputManager.ValidateAudioFileOutput(audioFileOutput).Verify();

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
    }
}