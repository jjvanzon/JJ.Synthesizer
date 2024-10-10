using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Factories;

namespace JJ.Business.Synthesizer.Tests.Wishes
{
    public partial class SynthSugarBase
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
            internal ValueIndexer(OperatorFactory parent)
            {
                _parent = parent;
            }

            /// <inheritdoc cref="ValueIndexer"/>
            public ValueOperatorWrapper this[double value] => _parent.Value(value);
        }
    }
}