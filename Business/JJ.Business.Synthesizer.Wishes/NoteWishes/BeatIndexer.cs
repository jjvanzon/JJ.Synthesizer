using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.Wishes
{
    /// <inheritdoc cref="docs._beatindexer"/>
    public class BeatIndexer
    {
        private readonly SynthWishes _synthWishes;
        
        /// <inheritdoc cref="docs._beatindexer"/>
        internal BeatIndexer(SynthWishes synthWishes)
            => _synthWishes = synthWishes;
        
        /// <inheritdoc cref="docs._beatindexer"/>
        public FlowNode this[double count]
            => (count - 1) * _synthWishes.GetBeatLength;
        
        /// <inheritdoc cref="docs._beatindexer"/>
        public FlowNode this[FlowNode count]
            => (count - 1) * _synthWishes.GetBeatLength;
    }
}