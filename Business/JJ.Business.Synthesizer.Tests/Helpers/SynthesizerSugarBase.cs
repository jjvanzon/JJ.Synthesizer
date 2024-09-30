using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Managers;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    public partial class SynthesizerSugarBase : OperatorFactory
    {
        private const double DEFAULT_BAR_LENGTH = 4;
        private const double DEFAULT_BEAT_LENGTH = 1;

        protected readonly CurveFactory CurveFactory;
        protected readonly AudioFileOutputManager AudioFileOutputManager;

        protected SynthesizerSugarBase()
            : this(PersistenceHelper.CreateContext(), DEFAULT_BAR_LENGTH, DEFAULT_BEAT_LENGTH)
        { }

        protected SynthesizerSugarBase(IContext context, double barLength, double beatLength)
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
            Bar = new BarIndexer(this, barLength);
            Beat = new BeatIndexer(this, beatLength);
            t = new TimeIndexer(this, barLength, beatLength);
        }

        /// <inheritdoc cref="ValueIndexer"/>
        protected readonly ValueIndexer _;

        /// <inheritdoc cref="BarIndexer"/>
        protected BarIndexer Bar { get; }

        /// <inheritdoc cref="BeatIndexer"/>
        protected BeatIndexer Beat { get; }
        
        // ReSharper disable once InconsistentNaming
        /// <inheritdoc cref="TimeIndexer"/>
        public TimeIndexer t { get; }
    }
}
