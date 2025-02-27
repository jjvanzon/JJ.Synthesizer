using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static JJ.Business.Synthesizer.Wishes.NameWishes;
// ReSharper disable ParameterHidesMember
// ReSharper disable once CheckNamespace

namespace JJ.Business.Synthesizer.Wishes
{
    
    public partial class SynthWishes
    {
        // Note Indexers

        // Instrument without Parameters

        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute1() <br/><br/>
        /// _[ Flute1, 0.8 ],
        /// _[ Flute1, 0.8, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            Func<FlowNode> sound,
            double vol, FlowNode len = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(), default, vol, len, ResolveName(sound)); } }

        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute1() <br/><br/>
        /// _[ Flute1 ],
        /// _[ Flute1, MyCurve ],
        /// _[ Flute1, MyCurve, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            Func<FlowNode> sound,
            FlowNode vol = null, FlowNode len = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(), default, vol, len, ResolveName(sound)); } }

        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute1() <br/><br/>
        /// _[ 0.00, Flute1, 0.8 ],
        /// _[ 0.00, Flute1, 0.8, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            double t, Func<FlowNode> sound,
            double vol, FlowNode len = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(), _[t], vol, len, ResolveName(sound)); } }
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute1() <br/><br/>
        /// _[ 0.00, Flute1 ],
        /// _[ 0.00, Flute1, MyCurve ],
        /// _[ 0.00, Flute1, MyCurve, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            double t, Func<FlowNode> sound,
            FlowNode vol = null, FlowNode len = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(), _[t], vol, len, ResolveName(sound)); } }
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute1() <br/><br/>
        /// _[ t[1, 1], Flute1, 0.8 ],
        /// _[ t[1, 1], Flute1, 0.8, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode t, Func<FlowNode> sound,
            double vol, FlowNode len = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(), t, vol, len, ResolveName(sound)); } }

        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute1() <br/><br/>
        /// _[ t[1, 1], Flute1 ],
        /// _[ t[1, 1], Flute1, MyCurve ],
        /// _[ t[1, 1], Flute1, MyCurve, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode t, Func<FlowNode> sound,
            FlowNode vol = null, FlowNode len = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(), t, vol, len, ResolveName(sound)); } }
        
        // Instrument with 1 Parameter Freq
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute2(FlowNode freq) <br/><br/>
        /// _[ A4, Flute2, 0.8 ],
        /// _[ A4, Flute2, 0.8, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode freq, Func<FlowNode, FlowNode> sound,
            double vol, FlowNode len = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(freq), default, vol, len, ResolveName(sound)); } }
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute2(FlowNode freq) <br/><br/>
        /// _[ A4, Flute2 ],
        /// _[ A4, Flute2, MyCurve ],
        /// _[ A4, Flute2, MyCurve, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode freq, Func<FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(freq), default, vol, len, ResolveName(sound)); } }
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute2(FlowNode freq) <br/><br/>
        /// _[ 0.00, A4, Flute2, 0.8 ],
        /// _[ 0.25, C4, Flute2, 0.8, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            double t, FlowNode freq, Func<FlowNode, FlowNode> sound,
            double vol, FlowNode len = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(freq), _[t], vol, len, ResolveName(sound)); } }
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute2(FlowNode freq) <br/><br/>
        /// _[ 0.00, A4, Flute2 ],
        /// _[ 0.25, C4, Flute2, MyCurve ],
        /// _[ 0.50, E5, Flute2, MyCurve, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            double t, FlowNode freq, Func<FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(freq), _[t], vol, len, ResolveName(sound)); } }
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute2(FlowNode freq) <br/><br/>
        /// _[ t[1, 1], A4, Flute2, 0.8 ],
        /// _[ t[1, 2], C5, Flute2, 0.8, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode t, FlowNode freq, Func<FlowNode, FlowNode> sound,
            double vol, FlowNode len = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(freq), t, vol, len, ResolveName(sound)); } }

        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute2(FlowNode freq) <br/><br/>
        /// _[ t[1, 1], A4, Flute2 ],
        /// _[ t[1, 2], C5, Flute2, MyCurve ],
        /// _[ t[1, 3], E5, Flute2, MyCurve, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode t, FlowNode freq, Func<FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(freq), t, vol, len, ResolveName(sound)); } }
        
        // Instrument with 2 Parameters Freq and Len
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute3(FlowNode freq, FlowNode len = null) <br/><br/>
        /// _[ A4, Flute3, 0.8 ],
        /// _[ A4, Flute3, 0.8, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode freq, Func<FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(freq, len), default, vol, len, ResolveName(sound)); } }
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute3(FlowNode freq, FlowNode len = null) <br/><br/>
        /// _[ A4, Flute3 ],
        /// _[ A4, Flute3, MyCurve ],
        /// _[ A4, Flute3, MyCurve, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode freq, Func<FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null]
        { get { len = GetNoteLengthSnapShot(len); return Note(sound(freq, len), default, vol, len, ResolveName(sound)); } }
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute3(FlowNode freq, FlowNode len = null) <br/><br/>
        /// _[ 0.00, A4, Flute3, 0.8 ],
        /// _[ 0.25, C5, Flute3, 0.8, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            double t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(freq, len), _[t], vol, len, ResolveName(sound)); } }
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute3(FlowNode freq, FlowNode len = null) <br/><br/>
        /// _[ 0.00, A4, Flute3 ],
        /// _[ 0.25, C5, Flute3, MyCurve ],
        /// _[ 0.50, E5, Flute3, MyCurve, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            double t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(freq, len), _[t], vol, len, ResolveName(sound)); } }
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute3(FlowNode freq, FlowNode len = null) <br/><br/>
        /// _[ t[1, 1], A4, Flute3, 0.8 ],
        /// _[ t[1, 2], C5, Flute3, 0.8, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(freq, len), t, vol, len, ResolveName(sound)); } }

        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute3(FlowNode freq, FlowNode len = null) <br/><br/>
        /// _[ t[1, 1], A4, Flute3 ],
        /// _[ t[1, 2], C5, Flute3, MyCurve ],
        /// _[ t[1, 3], E5, Flute3, MyCurve, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(freq, len), t, vol, len, ResolveName(sound)); } }

        // Instruments with 1 Effect Parameter (Optional)
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute4(FlowNode freq, FlowNode len = null, FlowNode fx1 = null) <br/><br/>
        /// _[ A4, Flute4, 0.8 ],
        /// _[ A4, Flute4, 0.8, l[0.5] ],
        /// _[ A4, Flute4, 0.8, l[0.5], _[0.14] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, FlowNode fx1 = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(freq, len, fx1), default, vol, len, ResolveName(sound)); } }
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute4(FlowNode freq, FlowNode len = null, FlowNode fx1 = null) <br/><br/>
        /// _[ A4, Flute4 ],
        /// _[ A4, Flute4, MyCurve ],
        /// _[ A4, Flute4, MyCurve, l[0.5] ],
        /// _[ A4, Flute4, MyCurve, l[0.5], _[0.14] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, FlowNode fx1 = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(freq, len, fx1), default, vol, len, ResolveName(sound)); } }
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute4(FlowNode freq, FlowNode len = null, FlowNode fx1 = null) <br/><br/>
        /// _[ 0.00, A4, Flute4, 0.8 ],
        /// _[ 0.25, C5, Flute4, 0.8, l[0.5] ],
        /// _[ 0.50, E5, Flute4, 0.8, l[0.5], _[0.14] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            double t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, FlowNode fx1 = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(freq, len, fx1), _[t], vol, len, ResolveName(sound)); } }
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute4(FlowNode freq, FlowNode len = null, FlowNode fx1 = null) <br/><br/>
        /// _[ 0.00, A4, Flute4 ],
        /// _[ 0.25, C5, Flute4, MyCurve ],
        /// _[ 0.50, E5, Flute4, MyCurve, l[0.5] ],
        /// _[ 0.75, G5, Flute4, MyCurve, l[0.5], _[0.14] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            double t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, FlowNode fx1 = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(freq, len, fx1), _[t], vol, len, ResolveName(sound)); } }
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute4(FlowNode freq, FlowNode len = null, FlowNode fx1 = null) <br/><br/>
        /// _[ t[1, 1], A4, Flute4, 0.8 ],
        /// _[ t[1, 2], C5, Flute4, 0.8, l[0.5] ],
        /// _[ t[1, 3], E5, Flute4, 0.8, l[0.5], _[0.14] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, FlowNode fx1 = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(freq, len, fx1), t, vol, len, ResolveName(sound)); } }

        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute4(FlowNode freq, FlowNode len = null, FlowNode fx1 = null) <br/><br/>
        /// _[ t[1, 1], A4, Flute4 ],
        /// _[ t[1, 2], C5, Flute4, MyCurve ],
        /// _[ t[1, 3], E5, Flute4, MyCurve, l[0.5] ],
        /// _[ t[1, 4], G5, Flute4, MyCurve, l[0.5], _[0.14] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode> sound, 
            FlowNode vol = null, FlowNode len = null, FlowNode fx1 = null] 
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(freq, len, fx1), t, vol, len, ResolveName(sound)); } }
        
        // Instruments with 2 Effect Parameters (Optional)
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute5(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null) <br/><br/>
        /// _[ A4, Flute5, 0.8 ],
        /// _[ A4, Flute5, 0.8, l[0.5] ],
        /// _[ A4, Flute5, 0.8, l[0.5], _[0.14] ],
        /// _[ A4, Flute5, 0.8, l[0.5], _[0.14], _[1.08] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(freq, len, fx1, fx2), default, vol, len, ResolveName(sound)); } }
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute5(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null) <br/><br/>
        /// _[ A4, Flute5 ],
        /// _[ A4, Flute5, MyCurve ],
        /// _[ A4, Flute5, MyCurve, l[0.5] ],
        /// _[ A4, Flute5, MyCurve, l[0.5], _[0.14] ],
        /// _[ A4, Flute5, MyCurve, l[0.5], _[0.14], _[1.08] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(freq, len, fx1, fx2), default, vol, len, ResolveName(sound)); } }
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute5(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null) <br/><br/>
        /// _[ 0.00, A4, Flute5, 0.8 ],
        /// _[ 0.25, C5, Flute5, 0.8, l[0.5] ],
        /// _[ 0.50, E5, Flute5, 0.8, l[0.5], _[0.14] ],
        /// _[ 0.75, G5, Flute5, 0.8, l[0.5], _[0.14], _[1.08] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            double t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(freq, len, fx1, fx2), _[t], vol, len, ResolveName(sound)); } }
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute5(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null) <br/><br/>
        /// _[ 0.00, A4, Flute5 ],
        /// _[ 0.25, C5, Flute5, MyCurve ],
        /// _[ 0.50, E5, Flute5, MyCurve, l[0.5] ],
        /// _[ 0.75, G5, Flute5, MyCurve, l[0.5], _[0.14] ],
        /// _[ 1.00, A5, Flute5, MyCurve, l[0.5], _[0.14], _[1.08] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            double t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(freq, len, fx1, fx2), _[t], vol, len, ResolveName(sound)); } }
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute5(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null) <br/><br/>
        /// _[ t[1, 1], A4, Flute5, 0.8 ],
        /// _[ t[1, 2], C5, Flute5, 0.8, l[0.5] ],
        /// _[ t[1, 3], E5, Flute5, 0.8, l[0.5], _[0.14] ],
        /// _[ t[1, 4], G5, Flute5, 0.8, l[0.5], _[0.14], _[1.08] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(freq, len, fx1, fx2), t, vol, len, ResolveName(sound)); } }

        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute5(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null) <br/><br/>
        /// _[ t[1, 1], A4, Flute5 ],
        /// _[ t[1, 2], C5, Flute5, MyCurve ],
        /// _[ t[1, 3], E5, Flute5, MyCurve, l[0.5] ],
        /// _[ t[1, 4], G5, Flute5, MyCurve, l[0.5], _[0.14] ],
        /// _[ t[2, 1], A5, Flute5, MyCurve, l[0.5], _[0.14], _[1.08] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(freq, len, fx1, fx2), t, vol, len, ResolveName(sound)); } }
        
        // Instruments with 3 Effect Parameters (Optional)
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute6(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null) <br/><br/>
        /// _[ A4, Flute6, 0.8 ],
        /// _[ A4, Flute6, 0.8, l[0.5] ],
        /// _[ A4, Flute6, 0.8, l[0.5], _[0.14] ],
        /// _[ A4, Flute6, 0.8, l[0.5], _[0.14], _[1.08], _[0.03] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(freq, len, fx1, fx2, fx3), default, vol, len, ResolveName(sound)); } }
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute6(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null) <br/><br/>
        /// _[ A4, Flute6 ],
        /// _[ A4, Flute6, MyCurve ],
        /// _[ A4, Flute6, MyCurve, l[0.5] ],
        /// _[ A4, Flute6, MyCurve, l[0.5], _[0.14] ],
        /// _[ A4, Flute6, MyCurve, l[0.5], _[0.14], _[1.08], _[0.03] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(freq, len, fx1, fx2, fx3), default, vol, len, ResolveName(sound)); } }
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute6(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null) <br/><br/>
        /// _[ 0.00, A4, Flute6, 0.8 ],
        /// _[ 0.25, C5, Flute6, 0.8, l[0.5] ],
        /// _[ 0.50, E5, Flute6, 0.8, l[0.5], _[0.14] ],
        /// _[ 0.75, G5, Flute6, 0.8, l[0.5], _[0.14], _[1.08], _[0.03] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            double t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(freq, len, fx1, fx2, fx3), _[t], vol, len, ResolveName(sound)); } }
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute6(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null) <br/><br/>
        /// _[ 0.00, A4, Flute6 ],
        /// _[ 0.25, C5, Flute6, MyCurve ],
        /// _[ 0.50, E5, Flute6, MyCurve, l[0.5] ],
        /// _[ 0.75, G5, Flute6, MyCurve, l[0.5], _[0.14] ],
        /// _[ 1.00, A5, Flute6, MyCurve, l[0.5], _[0.14], _[1.08], _[0.03] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            double t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(freq, len, fx1, fx2, fx3), _[t], vol, len, ResolveName(sound)); } }
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute6(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null) <br/><br/>
        /// _[ t[1, 1], A4, Flute6, 0.8 ],
        /// _[ t[1, 2], C5, Flute6, 0.8, l[0.5] ],
        /// _[ t[1, 3], E5, Flute6, 0.8, l[0.5], _[0.14] ],
        /// _[ t[1, 4], G5, Flute6, 0.8, l[0.5], _[0.14], _[1.08], _[0.03] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(freq, len, fx1, fx2, fx3), t, vol, len, ResolveName(sound)); } }

        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute6(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null) <br/><br/>
        /// _[ t[1, 1], A4, Flute6 ],
        /// _[ t[1, 2], C5, Flute6, MyCurve ],
        /// _[ t[1, 3], E5, Flute6, MyCurve, l[0.5] ],
        /// _[ t[1, 4], G5, Flute6, MyCurve, l[0.5], _[0.14] ],
        /// _[ t[2, 1], A5, Flute6, MyCurve, l[0.5], _[0.14], _[1.08], _[0.03] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(freq, len, fx1, fx2, fx3), t, vol, len, ResolveName(sound)); } }

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
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, FlowNode fx4 = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(freq, len, fx1, fx2, fx3, fx4), default, vol, len, ResolveName(sound)); } }
        
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
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, FlowNode fx4 = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(freq, len, fx1, fx2, fx3, fx4), default, vol, len, ResolveName(sound)); } }
        
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
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            double t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, FlowNode fx4 = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(freq, len, fx1, fx2, fx3, fx4), _[t], vol, len, ResolveName(sound)); } }
        
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
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            double t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, FlowNode fx4 = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(freq, len, fx1, fx2, fx3, fx4), _[t], vol, len, ResolveName(sound)); } }
        
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
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, FlowNode fx4 = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(freq, len, fx1, fx2, fx3, fx4), t, vol, len, ResolveName(sound)); } }
        
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
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, FlowNode fx4 = null]
            { get { len = GetNoteLengthSnapShot(len); return Note(sound(freq, len, fx1, fx2, fx3, fx4), t, vol, len, ResolveName(sound)); } }
    }
}
