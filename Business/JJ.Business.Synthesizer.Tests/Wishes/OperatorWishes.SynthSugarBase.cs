using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Persistence.Synthesizer;
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable AssignmentInsteadOfDiscard

namespace JJ.Business.Synthesizer.Tests.Wishes
{
    public partial class SynthSugarBase : OperatorFactory
    {
		private void InitializeOperatorWishes()
            => _ = new ValueIndexer(this);

        public Outlet StrikeNote(Outlet sound, Outlet delay = null, Outlet volume = null)
        {
            if (delay != null) sound = TimeAdd(sound, delay);
            if (volume != null) sound = Multiply(sound, volume);
            return sound;
        }

        /// <inheritdoc cref="docs._default" />
        public Outlet Stretch(Outlet signal, Outlet duration)
            => TimeMultiply(signal, duration ?? _[1]);

        /// <inheritdoc cref="ValueIndexer" />
        public ValueIndexer _;

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
                => _parent = parent;

            /// <inheritdoc cref="ValueIndexer"/>
            public ValueOperatorWrapper this[double value] => _parent.Value(value);
        }

	}
}