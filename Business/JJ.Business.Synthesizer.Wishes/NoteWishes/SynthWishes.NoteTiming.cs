using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Wishes.NoteWishes;

// ReSharper disable once CheckNamespace
namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        // Timing
        
        /// <inheritdoc cref="docs._barindexer"/>
        public BarIndexer bar { get; }
        /// <inheritdoc cref="docs._barsindexer"/>
        public BarsIndexer bars { get; }
        /// <inheritdoc cref="docs._beatindexer"/>
        public BeatIndexer beat { get; }
        /// <inheritdoc cref="docs._beatindexer"/>
        public BeatIndexer b { get; }
        /// <inheritdoc cref="docs._beatsindexer"/>
        public BeatsIndexer beats { get; }
        /// <inheritdoc cref="docs._timeindexer"/>
        public TimeIndexer t { get; }
        /// <inheritdoc cref="docs._beatsindexer"/>
        public BeatsIndexer l { get; }
        /// <inheritdoc cref="docs._beatsindexer"/>
        public BeatsIndexer len { get; }
        /// <inheritdoc cref="docs._beatsindexer"/>
        public BeatsIndexer length { get; }
    }
}