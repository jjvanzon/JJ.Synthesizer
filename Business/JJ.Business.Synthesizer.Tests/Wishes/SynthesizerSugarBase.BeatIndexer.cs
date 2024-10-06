using JJ.Business.Synthesizer.EntityWrappers;

namespace JJ.Business.Synthesizer.Tests.Wishes
{
    public partial class SynthesizerSugarBase
    {
        /// <summary>
        /// Returns duration of a number of beats (start counting at 1).<br/>
        /// Can also return the start time of a beat (start counting from 0).
        /// </summary>
        /// <returns> ValueOperatorWrapper which can also be used as an Outlet or a double. </returns>
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