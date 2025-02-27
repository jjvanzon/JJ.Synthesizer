using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Wishes.docs;

namespace JJ.Business.Synthesizer.Wishes.NoteWishes
{
    /// <inheritdoc cref="_timeindexer"/>
    public class TimeIndexer
    {
        private readonly SynthWishes _synthWishes;
        
        /// <inheritdoc cref="_timeindexer"/>
        internal TimeIndexer(SynthWishes synthWishes)
            => _synthWishes = synthWishes;
        
        /// <inheritdoc cref="_timeindexer"/>
        public FlowNode this[double bar, double beat]
            => (bar - 1) * _synthWishes.GetBarLength + (beat - 1) * _synthWishes.GetBeatLength;
        
        /// <inheritdoc cref="_timeindexer"/>
        public FlowNode this[FlowNode bar, FlowNode beat]
            => (bar - 1) * _synthWishes.GetBarLength + (beat - 1) * _synthWishes.GetBeatLength;
    }
}