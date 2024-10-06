using JJ.Business.Synthesizer.EntityWrappers;

namespace JJ.Business.Synthesizer.Tests.Wishes
{
    public partial class SynthesizerSugarBase
    {
        /// <summary>
        /// Returns duration of a number of beats in seconds.
        /// </summary>
        /// <returns> ValueOperatorWrapper which can also be used as an Outlet or a double. </returns>
        public class BeatsIndexer
        {
            private readonly SynthesizerSugarBase _parent;
            private readonly double _beatLength;

            /// <inheritdoc cref="BeatsIndexer"/>
            internal BeatsIndexer(SynthesizerSugarBase parent, double beatLength)
            {
                _parent = parent; 
                _beatLength = beatLength;
            }

            /// <inheritdoc cref="BeatsIndexer"/>
            public ValueOperatorWrapper this[double count] 
                => _parent.Value(count * _beatLength);
        }
    }
}