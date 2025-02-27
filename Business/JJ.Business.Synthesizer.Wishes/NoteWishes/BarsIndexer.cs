using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Wishes.docs;

namespace JJ.Business.Synthesizer.Wishes.NoteWishes
{
    public class BarsIndexer
    {
        private readonly SynthWishes _synthWishes;
        
        /// <inheritdoc cref="_barsindexer"/>
        internal BarsIndexer(SynthWishes synthWishes)
            => _synthWishes = synthWishes;
        
        /// <inheritdoc cref="_barsindexer"/>
        public FlowNode this[double count]
            => count * _synthWishes.GetBarLength;
        
        /// <inheritdoc cref="_barsindexer"/>
        public FlowNode this[FlowNode count]
            => count * _synthWishes.GetBarLength;
    }
}