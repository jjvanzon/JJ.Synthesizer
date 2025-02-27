using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Wishes.docs;

namespace JJ.Business.Synthesizer.Wishes.NoteWishes
{
    /// <inheritdoc cref="_barindexer"/>
    public class BarIndexer
    {
        private readonly SynthWishes _synthWishes;
        
        /// <inheritdoc cref="_barindexer"/>
        internal BarIndexer(SynthWishes synthWishes)
            => _synthWishes = synthWishes;
        
        /// <inheritdoc cref="_barindexer"/>
        public FlowNode this[double count]
            => (count - 1) * _synthWishes.GetBarLength;
        
        /// <inheritdoc cref="_barindexer"/>
        public FlowNode this[FlowNode count]
            => (count - 1) * _synthWishes.GetBarLength;
    }
}