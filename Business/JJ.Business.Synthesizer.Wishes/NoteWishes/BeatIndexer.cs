using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Wishes.docs;

namespace JJ.Business.Synthesizer.Wishes.NoteWishes
{
    /// <inheritdoc cref="_beatindexer"/>
    public class BeatIndexer
    {
        private readonly SynthWishes _synthWishes;
        
        /// <inheritdoc cref="_beatindexer"/>
        internal BeatIndexer(SynthWishes synthWishes)
            => _synthWishes = synthWishes;
        
        /// <inheritdoc cref="_beatindexer"/>
        public FlowNode this[double count]
            => (count - 1) * _synthWishes.GetBeatLength;
        
        /// <inheritdoc cref="_beatindexer"/>
        public FlowNode this[FlowNode count]
            => (count - 1) * _synthWishes.GetBeatLength;
    }
}