using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Wishes.docs;

namespace JJ.Business.Synthesizer.Wishes.NoteWishes
{
    /// <inheritdoc cref="_beatsindexer"/>
    public class BeatsIndexer
    {
        private readonly SynthWishes _synthWishes;
        
        /// <inheritdoc cref="_beatsindexer"/>
        internal BeatsIndexer(SynthWishes synthWishes)
            => _synthWishes = synthWishes;
        
        /// <inheritdoc cref="_beatsindexer"/>
        public FlowNode this[double count]
            => count * _synthWishes.GetBeatLength;
        
        /// <inheritdoc cref="_beatsindexer"/>
        public FlowNode this[FlowNode count]
            => count * _synthWishes.GetBeatLength;
    }
}