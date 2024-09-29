using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Managers;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    public class SynthesizerSugarBase : OperatorFactory
    {
        public class BarIndexer
        {
            private readonly OperatorFactory _this;
            private readonly double _barLength;

            public BarIndexer(OperatorFactory @this, double barLength) 
            { _this = @this; _barLength = barLength; }

            public Outlet this[double count] => _this.Value(count * _barLength);
        }

        private const double DEFAULT_BAR_LENGTH = 4;
        private const double DEFAULT_BEAT_LENGTH = 1;

        protected BarIndexer Bar { get; }   
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

            // Shorthand for Syntactic Sugar
            _ = new ValueIndexer(this);
            Bar = new BarIndexer(this, barLength);
        }

        #region Shorthand for Syntactic Sugar
        /// <summary>
        /// Shorthand for OperatorFactor.Value(123), x.Value(123) or Value(123). Allows using _[123] instead.
        /// Literal numbers need to be wrapped inside a Value Operator so they can always be substituted by
        /// a whole formula / graph / calculation / curve over time.
        /// </summary>
        protected readonly ValueIndexer _;
        #endregion
    }
}
