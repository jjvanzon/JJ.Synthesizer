using JJ.Business.Synthesizer.EntityWrappers;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    public partial class SynthesizerSugarBase
    {
        /// <summary> Returns the time in seconds for a bar, or duration of a number of bars. </summary>
        /// <returns> ValueOperatorWrapper also usable as Outlet or double. </returns>
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