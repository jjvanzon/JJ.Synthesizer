using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Managers;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    public partial class SynthesizerSugarBase : OperatorFactory
    {
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
        public TimeIndexer t { get; }
    }
}
