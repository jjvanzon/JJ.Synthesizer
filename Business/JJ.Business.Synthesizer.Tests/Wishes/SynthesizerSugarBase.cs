using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation.AudioFileOutputs;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Warnings;
using JJ.Business.Synthesizer.Warnings.Entities;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;

// ReSharper disable AssignmentInsteadOfDiscard

namespace JJ.Business.Synthesizer.Tests.Wishes
{
    public partial class SynthesizerSugarBase : OperatorFactory
    {
        private const double DEFAULT_TOTAL_VOLUME = 0.5;
        private const double DEFAULT_TOTAL_TIME = 3.0;
        private string NewLine => Environment.NewLine;
        
        private readonly AudioFileOutputManager _audioFileOutputManager;
       
        public SampleManager Samples { get; }
        
        public SynthesizerSugarBase()
            : this(PersistenceHelper.CreateContext())
        { }

        public SynthesizerSugarBase(IContext context)
            : this(context, beat: 1, bar: 4)
        { }

        public SynthesizerSugarBase(IContext context, double beat, double bar)
            : base(PersistenceHelper.CreateRepository<IOperatorRepository>(context),
                   PersistenceHelper.CreateRepository<IInletRepository>(context),
                   PersistenceHelper.CreateRepository<IOutletRepository>(context),
                   PersistenceHelper.CreateRepository<ICurveInRepository>(context),
                   PersistenceHelper.CreateRepository<IValueOperatorRepository>(context),
                   PersistenceHelper.CreateRepository<ISampleOperatorRepository>(context))
        {
            _audioFileOutputManager = TestHelper.CreateAudioFileOutputManager(context);
            _curveFactory = TestHelper.CreateCurveFactory(context);
            Samples = TestHelper.CreateSampleManager(context);

            _ = new ValueIndexer(this);
            this.bar = new BarIndexer(this, bar);
            bars = new BarsIndexer(this, bar);
            this.beat = new BeatIndexer(this, beat);
            beats = new BeatsIndexer(this, beat);
            t = new TimeIndexer(this, bar, beat);
        }

        /// <inheritdoc cref="ValueIndexer" />
        public readonly ValueIndexer _;

        // ReSharper disable InconsistentNaming

        /// <inheritdoc cref="BarIndexer" />
        public BarIndexer bar { get; }

        /// <inheritdoc cref="BarsIndexer" />
        public BarsIndexer bars { get; }

        /// <inheritdoc cref="BeatIndexer" />
        public BeatIndexer beat { get; }

        /// <inheritdoc cref="BeatsIndexer" />
        public BeatsIndexer beats { get; }

        /// <inheritdoc cref="TimeIndexer" />
        public TimeIndexer t { get; }
        
        // ReSharper restore InconsistentNaming
        
        /// <inheritdoc cref="docs._default" />
        public Outlet StrikeNote(Outlet sound, Outlet delay = null, Outlet volume = null)
        {
            if (delay != null) sound = TimeAdd(sound, delay);
            if (volume != null) sound = Multiply(sound, volume);
            return sound;
        }

        /*
        /// <inheritdoc cref="docs._default" />
        public Outlet StretchCurve(Curve curve, Outlet duration)
            => TimeMultiply(CurveIn(curve), duration);
        */
        
        /*
        /// <inheritdoc cref="docs._default" />
        public Outlet StretchCurve(CurveInWrapper curveIn, Outlet duration)
            => TimeMultiply(curveIn, duration);
        */
        
        /// <inheritdoc cref="docs._default" />
        public Outlet Stretch(Outlet signal, Outlet duration)
            => TimeMultiply(signal, duration);

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
    }
}