using JJ.Business.Synthesizer.EntityWrappers;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    public partial class SynthesizerSugarBase
    {
        /// <summary>
        /// TimeIndexer provides shorthand for specifying bar and beat in a musical sense.
        /// Access by bar and beat to get time-based value.
        /// Example usage: t[bar: 2, beat: 1.5] will return value for the time.
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
    }
}