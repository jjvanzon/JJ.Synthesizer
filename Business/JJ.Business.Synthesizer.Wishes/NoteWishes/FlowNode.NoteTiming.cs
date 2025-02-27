using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Wishes.NoteWishes;

// ReSharper disable once CheckNamespace
namespace JJ.Business.Synthesizer.Wishes
{
    public partial class FlowNode
    {
        // Timing
        
        /// <inheritdoc cref="docs._timeindexer"/>
        public TimeIndexer t => _synthWishes.t;
        /// <inheritdoc cref="docs._barindexer"/>
        public BarIndexer bar => _synthWishes.bar;
        /// <inheritdoc cref="docs._beatindexer"/>
        public BeatIndexer beat => _synthWishes.beat;
        /// <inheritdoc cref="docs._beatindexer"/>
        public BeatIndexer b => _synthWishes.b;
        /// <inheritdoc cref="docs._barsindexer"/>
        public BarsIndexer bars => _synthWishes.bars;
        /// <inheritdoc cref="docs._beatsindexer"/>
        public BeatsIndexer beats => _synthWishes.beats;
        /// <inheritdoc cref="docs._beatsindexer"/>
        public BeatsIndexer l => _synthWishes.l;
        /// <inheritdoc cref="docs._beatsindexer"/>
        public BeatsIndexer len => _synthWishes.len;
        /// <inheritdoc cref="docs._beatsindexer"/>
        public BeatsIndexer length  => _synthWishes.length;
    }
    
}
