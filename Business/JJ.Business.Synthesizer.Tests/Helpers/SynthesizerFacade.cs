using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Managers;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    public class SynthesizerSugarBase : OperatorFactory
    {
        /// <summary>
        /// Shorthand for OperatorFactor.Value(123), x.Value(123) or Value(123). Allows using _[123] instead.
        /// Literal numbers need to be wrapped inside a Value Operator so they can always be substituted by
        /// a whole formula / graph / calculation / curve over time.
        /// </summary>
        /// <returns>
        /// ValueOperatorWrapper also usable as Outlet or double.
        /// </returns>
        public class ValueIndexer
        {
            private readonly OperatorFactory _parent;
            
            /// <inheritdoc cref="ValueIndexer"/>
            internal ValueIndexer(OperatorFactory parent) => _parent = parent;
            
            /// <inheritdoc cref="ValueIndexer"/>
            public ValueOperatorWrapper this[double value] => _parent.Value(value);
        }

        /// <summary> Returns the time in seconds for a bar, or duration of a number of bars. </summary>
        /// <returns> ValueOperatorWrapper also usable as Outlet or double. </returns>
        public class BarIndexer
        {
            private readonly SynthesizerSugarBase _parent;
            private readonly double _barLength;

            /// <inheritdoc cref="BarIndexer"/>
            internal BarIndexer(SynthesizerSugarBase parent, double barLength)
            {
                _parent = parent; _barLength = barLength;
            }
            
            /// <inheritdoc cref="BarIndexer"/>
            public ValueOperatorWrapper this[double count] 
                => _parent.Value(count * _barLength);
        }

        /// <summary> Returns the duration in seconds for a number of beats. </summary>
        /// <returns> ValueOperatorWrapper also usable as Outlet or double. </returns>
        public class BeatIndexer
        {
            private readonly SynthesizerSugarBase _parent;
            private readonly double _beatLength;

            /// <inheritdoc cref="BeatIndexer"/>
            internal BeatIndexer(SynthesizerSugarBase parent, double beatLength)
            {
                _parent = parent; _beatLength = beatLength;
            }

            /// <inheritdoc cref="BeatIndexer"/>
            public ValueOperatorWrapper this[double count] 
                => _parent.Value(count * _beatLength);
        }

        /// <summary>
        /// TimeIndexer provides shorthand for specifying bar and beat in a musical sense.
        /// Access by bar and beat to get time-based Value.
        /// Example usage: t[bar: 2, beat: 1.5] will return Value for the time.
        /// </summary>
        /// <returns> ValueOperatorWrapper also usable as Outlet or double. </returns>
        public class TimeIndexer
        {
            private readonly SynthesizerSugarBase _parent;
            private readonly double _barLength;
            private readonly double _beatLength;

            internal TimeIndexer(SynthesizerSugarBase parent, double barLength, double beatLength)
            {
                _parent = parent; _barLength = barLength; _beatLength = beatLength;
            }

            /// <inheritdoc cref="TimeIndexer" />
            public ValueOperatorWrapper this[double bar, double beat]
                => _parent.Value(bar * _barLength + beat * _beatLength);
        }

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
        
        #region Shorthand for Syntactic Sugar
        //private Outlet t(double bar, double beat) => Value(bar * BAR + beat * BEAT);
        #endregion
        
        // Adding an instance of TimeIndexer to SynthesizerSugarBase
        public TimeIndexer t { get; }
    }
}
