using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Wishes.NoteWishes;
using JJ.Business.Synthesizer.Wishes.docs;

// ReSharper disable once CheckNamespace
namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        // Timing
        
        /// <inheritdoc cref="_barindexer"/>
        public BarIndexer bar { get; }
        /// <inheritdoc cref="_barsindexer"/>
        public BarsIndexer bars { get; }
        /// <inheritdoc cref="_beatindexer"/>
        public BeatIndexer beat { get; }
        /// <inheritdoc cref="_beatindexer"/>
        public BeatIndexer b { get; }
        /// <inheritdoc cref="_beatsindexer"/>
        public BeatsIndexer beats { get; }
        /// <inheritdoc cref="_timeindexer"/>
        public TimeIndexer t { get; }
        /// <inheritdoc cref="_beatsindexer"/>
        public BeatsIndexer l { get; }
        /// <inheritdoc cref="_beatsindexer"/>
        public BeatsIndexer len { get; }
        /// <inheritdoc cref="_beatsindexer"/>
        public BeatsIndexer length { get; }
    }
}