using JJ.Business.Synthesizer.EntityWrappers;

namespace JJ.Business.Synthesizer.Tests.Wishes
{
    public partial class SynthSugarBase
    {
        /// <summary>
        /// This TimeIndexer provides shorthand for specifying bar and beat in a musical sense.
        /// Access by bar and beat to get time-based value.
        /// Example usage: t[bar: 2, beat: 1.5] will return the number of seconds.
        /// The numbers are 1-based, so the first bar is bar 1, the first beat is beat 1.
        /// </summary>
        /// <returns> ValueOperatorWrapper also usable as Outlet or double. </returns>
        public class TimeIndexer
        {
            private readonly SynthSugarBase _parent;
            private readonly double _barLength;
            private readonly double _beatLength;

            internal TimeIndexer(SynthSugarBase parent, double barLength, double beatLength)
            {
                _parent = parent;
                _barLength = barLength;
                _beatLength = beatLength;
            }

            /// <inheritdoc cref="TimeIndexer" />
            public ValueOperatorWrapper this[double bar, double beat]
                => _parent.Value((bar - 1) * _barLength + (beat - 1) * _beatLength);
        }
    }
}