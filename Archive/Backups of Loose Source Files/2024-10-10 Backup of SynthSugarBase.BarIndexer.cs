using JJ.Business.Synthesizer.EntityWrappers;

namespace JJ.Business.Synthesizer.Tests.Wishes
{
    public partial class SynthSugarBase
    {
        /// <summary>
        /// Returns the time in seconds of the start of a bar.
        /// </summary>
        /// <returns> ValueOperatorWrapper which can also be used as an Outlet or a double. </returns>
        public class BarIndexer
        {
            private reacdonly SynthSugarBase _parent;
            private readonly double _barLength;

            /// <inheritdoc cref="BarIndexer"/>
            internal BarIndexer(SynthSugarBase parent, double barLength)
            {
                _parent = parent;
                _barLength = barLength;
            }

            /// <inheritdoc cref="BarIndexer"/>
            public ValueOperatorWrapper this[double count]
                => _parent.Value((count - 1) * _barLength);

        }
    }
}