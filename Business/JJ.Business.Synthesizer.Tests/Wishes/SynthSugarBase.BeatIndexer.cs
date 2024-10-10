using JJ.Business.Synthesizer.EntityWrappers;

namespace JJ.Business.Synthesizer.Tests.Wishes
{
    public partial class SynthSugarBase
    {
        /// <summary>
        /// Returns the start time of a beat in seconds.
        /// </summary>
        /// <returns> ValueOperatorWrapper which can also be used as an Outlet or a double. </returns>
        public class BeatIndexer
        {
            private readonly SynthSugarBase _parent;
            private readonly double _beatLength;

            /// <inheritdoc cref="BeatIndexer"/>
            internal BeatIndexer(SynthSugarBase parent, double beatLength)
            {
                _parent = parent; 
                _beatLength = beatLength;
            }

            /// <inheritdoc cref="BeatIndexer"/>
            public ValueOperatorWrapper this[double count]
                => _parent.Value((count - 1) * _beatLength);
        }
    }
}