using JJ.Business.Synthesizer.EntityWrappers;

namespace JJ.Business.Synthesizer.Tests.Wishes
{
    public partial class SynthSugarBase
    {
        /// <summary>
        /// Returns duration of a number of bars in seconds.<br/>
        /// </summary>
        /// <returns> ValueOperatorWrapper which can also be used as an Outlet or a double. </returns>
        public class BarsIndexer
        {
            private readonly SynthSugarBase _parent;
            private readonly double _barLength;

            /// <inheritdoc cref="BarsIndexer"/>
            internal BarsIndexer(SynthSugarBase parent, double barLength)
            {
                _parent = parent; 
                _barLength = barLength;
            }

            /// <inheritdoc cref="BarsIndexer"/>
            public ValueOperatorWrapper this[double count] 
                => _parent.Value(count * _barLength);
        }
    }
}