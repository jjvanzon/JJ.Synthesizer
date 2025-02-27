using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.Wishes
{
    /// <inheritdoc cref="docs._barsindexer"/>
    public class BarsIndexer
    {
        private readonly SynthWishes _synthWishes;
        
        /// <inheritdoc cref="docs._barsindexer"/>
        internal BarsIndexer(SynthWishes synthWishes)
            => _synthWishes = synthWishes;
        
        /// <inheritdoc cref="docs._barsindexer"/>
        public FlowNode this[double count]
            => count * _synthWishes.GetBarLength;
        
        /// <inheritdoc cref="docs._barsindexer"/>
        public FlowNode this[FlowNode count]
            => count * _synthWishes.GetBarLength;
    }
}