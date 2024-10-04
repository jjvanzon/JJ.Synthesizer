using JJ.Business.Synthesizer.EntityWrappers;

namespace JJ.Business.Synthesizer.Tests.Wishes
{
    public partial class SynthesizerSugarBase
    {
        /// <summary> Returns the duration in seconds for a number of beats. </summary>
        /// <returns> ValueOperatorWrapper also usable as Outlet or double. </returns>
        public class BeatIndexer
        {
            private readonly SynthesizerSugarBase _parent;
            private readonly double _beatLength;

            /// <inheritdoc cref="BeatIndexer"/>
            internal BeatIndexer(SynthesizerSugarBase parent, double beatLength)
            {
                _parent = parent; 
                _beatLength = beatLength;
            }

            /// <inheritdoc cref="BeatIndexer"/>
            public ValueOperatorWrapper this[double count] 
                => _parent.Value(count * _beatLength);
        }
    }
}