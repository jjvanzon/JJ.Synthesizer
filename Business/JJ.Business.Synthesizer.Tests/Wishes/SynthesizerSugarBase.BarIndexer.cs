using JJ.Business.Synthesizer.EntityWrappers;

namespace JJ.Business.Synthesizer.Tests.Wishes
{
    public partial class SynthesizerSugarBase
    {
        /// <summary>
        /// Returns duration of a number of bars (start counting at 1).<br/>
        /// Can also return the time of the start of a bar (start counting from 0).
        /// </summary>
        /// <returns> ValueOperatorWrapper which can also be used as an Outlet or a double. </returns>
        public class BarIndexer
        {
            private readonly SynthesizerSugarBase _parent;
            private readonly double _barLength;

            /// <inheritdoc cref="BarIndexer"/>
            internal BarIndexer(SynthesizerSugarBase parent, double barLength)
            {
                _parent = parent; 
                _barLength = barLength;
            }
            
            /// <inheritdoc cref="BarIndexer"/>
            public ValueOperatorWrapper this[double count] 
                => _parent.Value(count * _barLength);
        }
    }
}