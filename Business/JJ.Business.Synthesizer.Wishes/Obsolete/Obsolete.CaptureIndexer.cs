using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.Wishes.docs;
using static JJ.Business.Synthesizer.Wishes.Obsolete.CaptureIndexerObsoleteMessages;

namespace JJ.Business.Synthesizer.Wishes.Obsolete
{
    public static class CaptureIndexerObsoleteMessages
    {
        public const string ObsoleteMessage = "CaptureIndex no longer needed. Indexers like _[...] are can be used directly on SynthWishes and FlowNode objects.";
    }
    
    /// <inheritdoc cref="_captureindexer" />
    [Obsolete(ObsoleteMessage, true)]
	public class CaptureIndexer
    {
        // General
        
        private readonly SynthWishes _synthWishes;
        
        /// <inheritdoc cref="_captureindexer" />
        [Obsolete(ObsoleteMessage, true)]
        internal CaptureIndexer(SynthWishes synthWishes) 
            => _synthWishes = synthWishes;
        
        // ReSharper disable once UnusedParameter.Global
        /// <summary>
        /// Crazy conversion operator, reintroducing the discard notation _
        /// sort of, for FlowNodes.
        /// </summary>
        /// <param name="captureIndexer"></param>
        [Obsolete(ObsoleteMessage, true)]
        public static implicit operator FlowNode(CaptureIndexer captureIndexer) => null;
    
        // Basic Indexers
    
        // Fluent
        
        /// <inheritdoc cref="_captureindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[Outlet outlet] => _synthWishes[outlet];

        // Values
        
        /// <inheritdoc cref="_captureindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[double value] => _synthWishes[value];
    
        // NoteWishes
    
        // Instrument without Parameters

        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute1() <br/><br/>
        /// _[ Flute1, 0.8 ],
        /// _[ Flute1, 0.8, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            Func<FlowNode> sound,
            double vol, FlowNode len = null]
            => _synthWishes[sound, vol, len];

        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute1() <br/><br/>
        /// _[ Flute1 ],
        /// _[ Flute1, MyCurve ],
        /// _[ Flute1, MyCurve, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            Func<FlowNode> sound,
            FlowNode vol = null, FlowNode len = null]
            => _synthWishes[sound, vol, len];

        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute1() <br/><br/>
        /// _[ 0.00, Flute1, 0.8 ],
        /// _[ 0.00, Flute1, 0.8, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            double t, Func<FlowNode> sound,
            double vol, FlowNode len = null]
            => _synthWishes[t, sound, vol, len];

        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute1() <br/><br/>
        /// _[ 0.00, Flute1 ],
        /// _[ 0.00, Flute1, MyCurve ],
        /// _[ 0.00, Flute1, MyCurve, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            double t, Func<FlowNode> sound,
            FlowNode vol = null, FlowNode len = null]
            => _synthWishes[t, sound, vol, len];
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute1() <br/><br/>
        /// _[ t[1, 1], Flute1, 0.8 ],
        /// _[ t[1, 1], Flute1, 0.8, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            FlowNode t, Func<FlowNode> sound,
            double vol, FlowNode len = null]
            => _synthWishes[t, sound, vol, len];

        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute1() <br/><br/>
        /// _[ t[1, 1], Flute1 ],
        /// _[ t[1, 1], Flute1, MyCurve ],
        /// _[ t[1, 1], Flute1, MyCurve, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            FlowNode t, Func<FlowNode> sound,
            FlowNode vol = null, FlowNode len = null]
            => _synthWishes[t, sound, vol, len];
        
        // Instrument with 1 Parameter Freq
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute2(FlowNode freq) <br/><br/>
        /// _[ A4, Flute2, 0.8 ],
        /// _[ A4, Flute2, 0.8, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            FlowNode freq, Func<FlowNode, FlowNode> sound,
            double vol, FlowNode len = null]
            => _synthWishes[freq, sound, vol, len];
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute2(FlowNode freq) <br/><br/>
        /// _[ A4, Flute2 ],
        /// _[ A4, Flute2, MyCurve ],
        /// _[ A4, Flute2, MyCurve, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            FlowNode freq, Func<FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null]
            => _synthWishes[freq, sound, vol, len];
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute2(FlowNode freq) <br/><br/>
        /// _[ 0.00, A4, Flute2, 0.8 ],
        /// _[ 0.25, C4, Flute2, 0.8, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            double t, FlowNode freq, Func<FlowNode, FlowNode> sound,
            double vol, FlowNode len = null]
            => _synthWishes[t, freq, sound, vol, len];
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute2(FlowNode freq) <br/><br/>
        /// _[ 0.00, A4, Flute2 ],
        /// _[ 0.25, C4, Flute2, MyCurve ],
        /// _[ 0.50, E5, Flute2, MyCurve, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            double t, FlowNode freq, Func<FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null]
            => _synthWishes[t, freq, sound, vol, len];
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute2(FlowNode freq) <br/><br/>
        /// _[ t[1, 1], A4, Flute2, 0.8 ],
        /// _[ t[1, 2], C5, Flute2, 0.8, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            FlowNode t, FlowNode freq, Func<FlowNode, FlowNode> sound,
            double vol, FlowNode len = null]
            => _synthWishes[t, freq, sound, vol, len];

        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute2(FlowNode freq) <br/><br/>
        /// _[ t[1, 1], A4, Flute2 ],
        /// _[ t[1, 2], C5, Flute2, MyCurve ],
        /// _[ t[1, 3], E5, Flute2, MyCurve, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            FlowNode t, FlowNode freq, Func<FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null]
            => _synthWishes[t, freq, sound, vol, len];
        
        // Instrument with 2 Parameters Freq and Len
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute3(FlowNode freq, FlowNode len = null) <br/><br/>
        /// _[ A4, Flute3, 0.8 ],
        /// _[ A4, Flute3, 0.8, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            FlowNode freq, Func<FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null]
            => _synthWishes[freq, sound, vol, len];
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute3(FlowNode freq, FlowNode len = null) <br/><br/>
        /// _[ A4, Flute3 ],
        /// _[ A4, Flute3, MyCurve ],
        /// _[ A4, Flute3, MyCurve, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            FlowNode freq, Func<FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null]
            => _synthWishes[freq, sound, vol, len];
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute3(FlowNode freq, FlowNode len = null) <br/><br/>
        /// _[ 0.00, A4, Flute3, 0.8 ],
        /// _[ 0.25, C5, Flute3, 0.8, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            double t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null]
            => _synthWishes[t, freq, sound, vol, len];
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute3(FlowNode freq, FlowNode len = null) <br/><br/>
        /// _[ 0.00, A4, Flute3 ],
        /// _[ 0.25, C5, Flute3, MyCurve ],
        /// _[ 0.50, E5, Flute3, MyCurve, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            double t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null]
            => _synthWishes[t, freq, sound, vol, len];
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute3(FlowNode freq, FlowNode len = null) <br/><br/>
        /// _[ t[1, 1], A4, Flute3, 0.8 ],
        /// _[ t[1, 2], C5, Flute3, 0.8, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            FlowNode t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null]
            => _synthWishes[t, freq, sound, vol, len];

        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute3(FlowNode freq, FlowNode len = null) <br/><br/>
        /// _[ t[1, 1], A4, Flute3 ],
        /// _[ t[1, 2], C5, Flute3, MyCurve ],
        /// _[ t[1, 3], E5, Flute3, MyCurve, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            FlowNode t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null]
            => _synthWishes[t, freq, sound, vol, len];

        // Instruments with 1 Effect Parameter (Optional)
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute4(FlowNode freq, FlowNode len = null, FlowNode fx1 = null) <br/><br/>
        /// _[ A4, Flute4, 0.8 ],
        /// _[ A4, Flute4, 0.8, l[0.5] ],
        /// _[ A4, Flute4, 0.8, l[0.5], _[0.14] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, FlowNode fx1 = null]
            => _synthWishes[freq, sound, vol, len, fx1];
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute4(FlowNode freq, FlowNode len = null, FlowNode fx1 = null) <br/><br/>
        /// _[ A4, Flute4 ],
        /// _[ A4, Flute4, MyCurve ],
        /// _[ A4, Flute4, MyCurve, l[0.5] ],
        /// _[ A4, Flute4, MyCurve, l[0.5], _[0.14] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, FlowNode fx1 = null]
            => _synthWishes[freq, sound, vol, len, fx1];
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute4(FlowNode freq, FlowNode len = null, FlowNode fx1 = null) <br/><br/>
        /// _[ 0.00, A4, Flute4, 0.8 ],
        /// _[ 0.25, C5, Flute4, 0.8, l[0.5] ],
        /// _[ 0.50, E5, Flute4, 0.8, l[0.5], _[0.14] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            double t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, FlowNode fx1 = null]
            => _synthWishes[t, freq, sound, vol, len, fx1];
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute4(FlowNode freq, FlowNode len = null, FlowNode fx1 = null) <br/><br/>
        /// _[ 0.00, A4, Flute4 ],
        /// _[ 0.25, C5, Flute4, MyCurve ],
        /// _[ 0.50, E5, Flute4, MyCurve, l[0.5] ],
        /// _[ 0.75, G5, Flute4, MyCurve, l[0.5], _[0.14] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            double t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, FlowNode fx1 = null]
            => _synthWishes[t, freq, sound, vol, len, fx1];
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute4(FlowNode freq, FlowNode len = null, FlowNode fx1 = null) <br/><br/>
        /// _[ t[1, 1], A4, Flute4, 0.8 ],
        /// _[ t[1, 2], C5, Flute4, 0.8, l[0.5] ],
        /// _[ t[1, 3], E5, Flute4, 0.8, l[0.5], _[0.14] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            FlowNode t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, FlowNode fx1 = null]
            => _synthWishes[t, freq, sound, vol, len, fx1];

        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute4(FlowNode freq, FlowNode len = null, FlowNode fx1 = null) <br/><br/>
        /// _[ t[1, 1], A4, Flute4 ],
        /// _[ t[1, 2], C5, Flute4, MyCurve ],
        /// _[ t[1, 3], E5, Flute4, MyCurve, l[0.5] ],
        /// _[ t[1, 4], G5, Flute4, MyCurve, l[0.5], _[0.14] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            FlowNode t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode> sound, 
            FlowNode vol = null, FlowNode len = null, FlowNode fx1 = null] 
            => _synthWishes[t, freq, sound, vol, len, fx1];
        
        // Instruments with 2 Effect Parameters (Optional)
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute5(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null) <br/><br/>
        /// _[ A4, Flute5, 0.8 ],
        /// _[ A4, Flute5, 0.8, l[0.5] ],
        /// _[ A4, Flute5, 0.8, l[0.5], _[0.14] ],
        /// _[ A4, Flute5, 0.8, l[0.5], _[0.14], _[1.08] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null]
            => _synthWishes[freq, sound, vol, len, fx1, fx2];
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute5(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null) <br/><br/>
        /// _[ A4, Flute5 ],
        /// _[ A4, Flute5, MyCurve ],
        /// _[ A4, Flute5, MyCurve, l[0.5] ],
        /// _[ A4, Flute5, MyCurve, l[0.5], _[0.14] ],
        /// _[ A4, Flute5, MyCurve, l[0.5], _[0.14], _[1.08] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null]
            => _synthWishes[freq, sound, vol, len, fx1, fx2];
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute5(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null) <br/><br/>
        /// _[ 0.00, A4, Flute5, 0.8 ],
        /// _[ 0.25, C5, Flute5, 0.8, l[0.5] ],
        /// _[ 0.50, E5, Flute5, 0.8, l[0.5], _[0.14] ],
        /// _[ 0.75, G5, Flute5, 0.8, l[0.5], _[0.14], _[1.08] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            double t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null]
            => _synthWishes[t, freq, sound, vol, len, fx1, fx2];
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute5(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null) <br/><br/>
        /// _[ 0.00, A4, Flute5 ],
        /// _[ 0.25, C5, Flute5, MyCurve ],
        /// _[ 0.50, E5, Flute5, MyCurve, l[0.5] ],
        /// _[ 0.75, G5, Flute5, MyCurve, l[0.5], _[0.14] ],
        /// _[ 1.00, A5, Flute5, MyCurve, l[0.5], _[0.14], _[1.08] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            double t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null]
            => _synthWishes[t, freq, sound, vol, len, fx1, fx2];
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute5(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null) <br/><br/>
        /// _[ t[1, 1], A4, Flute5, 0.8 ],
        /// _[ t[1, 2], C5, Flute5, 0.8, l[0.5] ],
        /// _[ t[1, 3], E5, Flute5, 0.8, l[0.5], _[0.14] ],
        /// _[ t[1, 4], G5, Flute5, 0.8, l[0.5], _[0.14], _[1.08] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            FlowNode t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null]
            => _synthWishes[t, freq, sound, vol, len, fx1, fx2];

        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute5(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null) <br/><br/>
        /// _[ t[1, 1], A4, Flute5 ],
        /// _[ t[1, 2], C5, Flute5, MyCurve ],
        /// _[ t[1, 3], E5, Flute5, MyCurve, l[0.5] ],
        /// _[ t[1, 4], G5, Flute5, MyCurve, l[0.5], _[0.14] ],
        /// _[ t[2, 1], A5, Flute5, MyCurve, l[0.5], _[0.14], _[1.08] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            FlowNode t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null]
            => _synthWishes[t, freq, sound, vol, len, fx1, fx2];
        
        // Instruments with 3 Effect Parameters (Optional)
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute6(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null) <br/><br/>
        /// _[ A4, Flute6, 0.8 ],
        /// _[ A4, Flute6, 0.8, l[0.5] ],
        /// _[ A4, Flute6, 0.8, l[0.5], _[0.14] ],
        /// _[ A4, Flute6, 0.8, l[0.5], _[0.14], _[1.08], _[0.03] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null]
            => _synthWishes[freq, sound, vol, len, fx1, fx2, fx3];
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute6(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null) <br/><br/>
        /// _[ A4, Flute6 ],
        /// _[ A4, Flute6, MyCurve ],
        /// _[ A4, Flute6, MyCurve, l[0.5] ],
        /// _[ A4, Flute6, MyCurve, l[0.5], _[0.14] ],
        /// _[ A4, Flute6, MyCurve, l[0.5], _[0.14], _[1.08], _[0.03] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null]
            => _synthWishes[freq, sound, vol, len, fx1, fx2, fx3];
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute6(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null) <br/><br/>
        /// _[ 0.00, A4, Flute6, 0.8 ],
        /// _[ 0.25, C5, Flute6, 0.8, l[0.5] ],
        /// _[ 0.50, E5, Flute6, 0.8, l[0.5], _[0.14] ],
        /// _[ 0.75, G5, Flute6, 0.8, l[0.5], _[0.14], _[1.08], _[0.03] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            double t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null]
            => _synthWishes[t, freq, sound, vol, len, fx1, fx2, fx3];
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute6(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null) <br/><br/>
        /// _[ 0.00, A4, Flute6 ],
        /// _[ 0.25, C5, Flute6, MyCurve ],
        /// _[ 0.50, E5, Flute6, MyCurve, l[0.5] ],
        /// _[ 0.75, G5, Flute6, MyCurve, l[0.5], _[0.14] ],
        /// _[ 1.00, A5, Flute6, MyCurve, l[0.5], _[0.14], _[1.08], _[0.03] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            double t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null]
            => _synthWishes[t, freq, sound, vol, len, fx1, fx2, fx3];
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute6(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null) <br/><br/>
        /// _[ t[1, 1], A4, Flute6, 0.8 ],
        /// _[ t[1, 2], C5, Flute6, 0.8, l[0.5] ],
        /// _[ t[1, 3], E5, Flute6, 0.8, l[0.5], _[0.14] ],
        /// _[ t[1, 4], G5, Flute6, 0.8, l[0.5], _[0.14], _[1.08], _[0.03] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            FlowNode t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null]
            => _synthWishes[t, freq, sound, vol, len, fx1, fx2, fx3];

        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute6(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null) <br/><br/>
        /// _[ t[1, 1], A4, Flute6 ],
        /// _[ t[1, 2], C5, Flute6, MyCurve ],
        /// _[ t[1, 3], E5, Flute6, MyCurve, l[0.5] ],
        /// _[ t[1, 4], G5, Flute6, MyCurve, l[0.5], _[0.14] ],
        /// _[ t[2, 1], A5, Flute6, MyCurve, l[0.5], _[0.14], _[1.08], _[0.03] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            FlowNode t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null]
            => _synthWishes[t, freq, sound, vol, len, fx1, fx2, fx3];

        // Instruments with 4 Effect Parameters (Optional)
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute7(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, FlowNode fx4 = null) <br/><br/>
        /// _[ A4, Flute7, 0.8 ],
        /// _[ A4, Flute7, 0.8, l[0.5] ],
        /// _[ A4, Flute7, 0.8, l[0.5], _[0.14] ],
        /// _[ A4, Flute7, 0.8, l[0.5], _[0.14], _[1.08] ],
        /// _[ A4, Flute7, 0.8, l[0.5], _[0.14], _[1.08], _[0.03] ],
        /// _[ A4, Flute7, 0.8, l[0.5], _[0.14], _[1.08], _[0.03], _[2.5] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, FlowNode fx4 = null]
            => _synthWishes[freq, sound, vol, len, fx1, fx2, fx3, fx4];
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute7(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, FlowNode fx4 = null) <br/><br/>
        /// _[ A4, Flute7 ],
        /// _[ A4, Flute7, MyCurve ],
        /// _[ A4, Flute7, MyCurve, l[0.5] ],
        /// _[ A4, Flute7, MyCurve, l[0.5], _[0.14] ],
        /// _[ A4, Flute7, MyCurve, l[0.5], _[0.14], _[1.08] ],
        /// _[ A4, Flute7, MyCurve, l[0.5], _[0.14], _[1.08], _[0.03] ],
        /// _[ A4, Flute7, MyCurve, l[0.5], _[0.14], _[1.08], _[0.03], _[2.5] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, FlowNode fx4 = null]
            => _synthWishes[freq, sound, vol, len, fx1, fx2, fx3, fx4];
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute7(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, FlowNode fx4 = null) <br/><br/>
        /// _[ 0.00, A4, Flute7, 0.8 ],
        /// _[ 0.25, C5, Flute7, 0.8, l[0.5] ],
        /// _[ 0.50, E5, Flute7, 0.8, l[0.5], _[0.14] ],
        /// _[ 0.75, G5, Flute7, 0.8, l[0.5], _[0.14], _[1.08] ],
        /// _[ 1.00, A5, Flute7, 0.8, l[0.5], _[0.14], _[1.08], _[0.03] ],
        /// _[ 1.25, A3, Flute7, 0.8, l[0.5], _[0.14], _[1.08], _[0.03], _[2.5] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            double t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, FlowNode fx4 = null]
            => _synthWishes[t, freq, sound, vol, len, fx1, fx2, fx3, fx4];
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute7(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, FlowNode fx4 = null) <br/><br/>
        /// _[ 0.00, A4, Flute7 ],
        /// _[ 0.25, C5, Flute7, MyCurve ],
        /// _[ 0.50, E5, Flute7, MyCurve, l[0.5] ],
        /// _[ 0.75, G5, Flute7, MyCurve, l[0.5], _[0.14] ],
        /// _[ 1.00, A5, Flute7, MyCurve, l[0.5], _[0.14], _[1.08] ],
        /// _[ 1.25, A3, Flute7, MyCurve, l[0.5], _[0.14], _[1.08], _[0.03] ],
        /// _[ 1.50, C4, Flute7, MyCurve, l[0.5], _[0.14], _[1.08], _[0.03], _[2.5] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            double t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, FlowNode fx4 = null]
            => _synthWishes[t, freq, sound, vol, len, fx1, fx2, fx3, fx4];
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute7(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, FlowNode fx4 = null) <br/><br/>
        /// _[ t[1, 1], A4, Flute7, 0.8 ],
        /// _[ t[1, 2], C5, Flute7, 0.8, l[0.5] ],
        /// _[ t[1, 3], E5, Flute7, 0.8, l[0.5], _[0.14] ],
        /// _[ t[1, 4], G5, Flute7, 0.8, l[0.5], _[0.14], _[1.08] ],
        /// _[ t[2, 1], A5, Flute7, 0.8, l[0.5], _[0.14], _[1.08], _[0.03] ],
        /// _[ t[2, 2], A3, Flute7, 0.8, l[0.5], _[0.14], _[1.08], _[0.03], _[2.5] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            FlowNode t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, FlowNode fx4 = null]
            => _synthWishes[t, freq, sound, vol, len, fx1, fx2, fx3, fx4];
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute7(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, FlowNode fx4 = null) <br/><br/>
        /// _[ t[1, 1], A4, Flute7 ],
        /// _[ t[1, 2], C5, Flute7, MyCurve ],
        /// _[ t[1, 3], E5, Flute7, MyCurve, l[0.5] ],
        /// _[ t[1, 4], G5, Flute7, MyCurve, l[0.5], _[0.14] ],
        /// _[ t[2, 1], A5, Flute7, MyCurve, l[0.5], _[0.14], _[1.08] ],
        /// _[ t[2, 2], A3, Flute7, MyCurve, l[0.5], _[0.14], _[1.08], _[0.03] ],
        /// _[ t[2, 3], C4, Flute7, MyCurve, l[0.5], _[0.14], _[1.08], _[0.03], _[2.5] ]
        /// </code></summary>
        /// <inheritdoc cref="_noteindexer" />
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            FlowNode t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, FlowNode fx4 = null]
            => _synthWishes[t, freq, sound, vol, len, fx1, fx2, fx3, fx4];
    
        // Command Notation for CaptureIndexer
    
        // 0 Parameters
        
        /// <inheritdoc cref="_commandindexer"/>
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            Func<FlowNode> command] 
            => _synthWishes[command];
        
        // 1 Parameter
        
        /// <inheritdoc cref="_commandindexer"/>
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            Func<FlowNode, FlowNode> command, 
            FlowNode param1 = null] 
            => _synthWishes[command, param1];

        /// <inheritdoc cref="_commandindexer"/>
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            Func<FlowNode, FlowNode> command,
            double param1]
            => _synthWishes[command, param1];

        // 2 Parameters

        /// <inheritdoc cref="_commandindexer"/>
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode> command, 
            FlowNode param1 = null, FlowNode param2 = null] 
            => _synthWishes[command, param1, param2];

        /// <inheritdoc cref="_commandindexer"/>
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode> command,
            double param1, FlowNode param2 = null]
            => _synthWishes[command, param1, param2];

        /// <inheritdoc cref="_commandindexer"/>
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode> command,
            FlowNode param1, double param2]
            => _synthWishes[command, param1, param2];

        /// <inheritdoc cref="_commandindexer"/>
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode> command,
            double param1, double param2]
            => _synthWishes[command, param1, param2];

        // 3 Parameters

        /// <inheritdoc cref="_commandindexer"/>
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> command, 
            FlowNode param1 = null, FlowNode param2 = null, FlowNode param3 = null] 
            => _synthWishes[command, param1, param2, param3];

        /// <inheritdoc cref="_commandindexer"/>
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> command,
            double param1, FlowNode param2 = null, FlowNode param3 = null]
            => _synthWishes[command, param1, param2, param3];

        /// <inheritdoc cref="_commandindexer"/>
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> command,
            FlowNode param1, double param2, FlowNode param3 = null]
            => _synthWishes[command, param1, param2, param3];

        /// <inheritdoc cref="_commandindexer"/>
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> command,
            FlowNode param1, FlowNode param2, double param3]
            => _synthWishes[command, param1, param2, param3];

        /// <inheritdoc cref="_commandindexer"/>
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> command,
            double param1, double param2, FlowNode param3]
            => _synthWishes[command, param1, param2, param3];

        /// <inheritdoc cref="_commandindexer"/>
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> command,
            double param1, FlowNode param2, double param3]
            => _synthWishes[command, param1, param2, param3];

        /// <inheritdoc cref="_commandindexer"/>
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> command,
            FlowNode param1, double param2, double param3]
            => _synthWishes[command, param1, param2, param3];

        /// <inheritdoc cref="_commandindexer"/>
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> command,
            double param1, double param2, double param3]
            => _synthWishes[command, param1, param2, param3];

        // 4 Parameters

        /// <inheritdoc cref="_commandindexer"/>
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> command, 
            FlowNode param1 = null, FlowNode param2 = null, FlowNode param3 = null, FlowNode param4 = null] 
            => _synthWishes[command, param1, param2, param3, param4];
        
        // 5 Parameters
        
        /// <inheritdoc cref="_commandindexer"/>
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> command, 
            FlowNode param1 = null, FlowNode param2 = null, FlowNode param3 = null, FlowNode param4 = null, FlowNode param5 = null] 
            => _synthWishes[command, param1, param2, param3, param4, param5];
        
        // 6 Parameters
        
        /// <inheritdoc cref="_commandindexer"/>
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> command, 
            FlowNode param1 = null, FlowNode param2 = null, FlowNode param3 = null, FlowNode param4 = null, FlowNode param5 = null, 
            FlowNode param6 = null] 
            => _synthWishes[command, param1, param2, param3, param4, param5, param6];
        
        // 7 Parameters
        
        /// <inheritdoc cref="_commandindexer"/>
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> command, 
            FlowNode param1 = null, FlowNode param2 = null, FlowNode param3 = null, FlowNode param4 = null, FlowNode param5 = null, 
            FlowNode param6 = null, FlowNode param7 = null] 
            => _synthWishes[command, param1, param2, param3, param4, param5, param6, param7];
        
        // 8 Parameters
        
        /// <inheritdoc cref="_commandindexer"/>
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> command, 
            FlowNode param1 = null, FlowNode param2 = null, FlowNode param3 = null, FlowNode param4 = null, FlowNode param5 = null, 
            FlowNode param6 = null, FlowNode param7 = null, FlowNode param8 = null] 
            => _synthWishes[command, param1, param2, param3, param4, param5, param6, param7, param8];
        
        // 9 Parameters
        
        /// <inheritdoc cref="_commandindexer"/>
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> command, 
            FlowNode param1 = null, FlowNode param2 = null, FlowNode param3 = null, FlowNode param4 = null, FlowNode param5 = null, 
            FlowNode param6 = null, FlowNode param7 = null, FlowNode param8 = null, FlowNode param9 = null] 
            => _synthWishes[command, param1, param2, param3, param4, param5, param6, param7, param8, param9];
        
        // 10 Parameters
        
        /// <inheritdoc cref="_commandindexer"/>
        [Obsolete(ObsoleteMessage, true)]
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> command, 
            FlowNode param1 = null, FlowNode param2 = null, FlowNode param3 = null, FlowNode param4 = null, FlowNode param5 = null, 
            FlowNode param6 = null, FlowNode param7 = null, FlowNode param8 = null, FlowNode param9 = null, FlowNode param10 = null] 
            => _synthWishes[command, param1, param2, param3, param4, param5, param6, param7, param8, param9, param10];
    }
}
