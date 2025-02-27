using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Wishes.NoteWishes;

// ReSharper disable once CheckNamespace
namespace JJ.Business.Synthesizer.Wishes
{

    // FlowNode
    
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

        // Note Operator
        
        /// <inheritdoc cref="docs._default" />
        public FlowNode Note(
            FlowNode delay = null, FlowNode volume = default, FlowNode duration = default,
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => _synthWishes.Note(this, delay, volume, duration, name, callerMemberName);
        
        /// <inheritdoc cref="docs._default" />
        public FlowNode Note(
            FlowNode sound, FlowNode delay, double volume, FlowNode duration,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Note(sound, delay, volume, duration, name, callerMemberName);
        
        /// <inheritdoc cref="docs._default" />
        public FlowNode Note(
            FlowNode delay, double volume,
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => _synthWishes.Note(this, delay, volume, name, callerMemberName);

        // Note Indexers

        // Instrument without Parameters

        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute1();
        /// _
        /// [ Flute1, 0.8 ]
        /// [ Flute1, 0.8, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            Func<FlowNode> sound,
            double vol, FlowNode len = null,
            [CallerMemberName] string callerMemberName = null]
            => Add(_[sound, vol, len]).SetName(callerMemberName);

        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute1();
        /// _
        /// [ Flute1 ]
        /// [ Flute1, MyCurve ]
        /// [ Flute1, MyCurve, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            Func<FlowNode> sound,
            FlowNode vol = null, FlowNode len = null,
            [CallerMemberName] string callerMemberName = null]
            => Add(_[sound, vol, len]).SetName(callerMemberName);

        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute1();
        /// _
        /// [ 0.00, Flute1, 0.8 ]
        /// [ 0.00, Flute1, 0.8, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            double t, Func<FlowNode> sound,
            double vol, FlowNode len = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[t, sound, vol, len]).SetName(callerMemberName);

        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute1();
        /// _
        /// [ 0.00, Flute1 ]
        /// [ 0.00, Flute1, MyCurve ]
        /// [ 0.00, Flute1, MyCurve, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            double t, Func<FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[t, sound, vol, len]).SetName(callerMemberName);
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute1();
        /// _
        /// [ t[1, 1], Flute1, 0.8 ]
        /// [ t[1, 1], Flute1, 0.8, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode t, Func<FlowNode> sound,
            double vol, FlowNode len = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[t, sound, vol, len]).SetName(callerMemberName);

        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute1();
        /// _
        /// [ t[1, 1], Flute1 ]
        /// [ t[1, 1], Flute1, MyCurve ]
        /// [ t[1, 1], Flute1, MyCurve, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode t, Func<FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[t, sound, vol, len]).SetName(callerMemberName);
        
        // Instrument with 1 Parameter Freq
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute2(FlowNode freq);
        /// _
        /// [ A4, Flute2, 0.8 ]
        /// [ A4, Flute2, 0.8, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode freq, Func<FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[freq, sound, vol, len]).SetName(callerMemberName);
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute2(FlowNode freq);
        /// _
        /// [ A4, Flute2 ]
        /// [ A4, Flute2, MyCurve ]
        /// [ A4, Flute2, MyCurve, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode freq, Func<FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[freq, sound, vol, len]).SetName(callerMemberName);
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute2(FlowNode freq);
        /// _
        /// [ 0.00, A4, Flute2, 0.8 ]
        /// [ 0.25, C4, Flute2, 0.8, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            double t, FlowNode freq, Func<FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[t, freq, sound, vol, len]).SetName(callerMemberName);
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute2(FlowNode freq);
        /// _
        /// [ 0.00, A4, Flute2 ]
        /// [ 0.25, C4, Flute2, MyCurve ]
        /// [ 0.50, E5, Flute2, MyCurve, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            double t, FlowNode freq, Func<FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[t, freq, sound, vol, len]).SetName(callerMemberName);
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute2(FlowNode freq);
        /// _
        /// [ t[1, 1], A4, Flute2, 0.8 ]
        /// [ t[1, 2], C5, Flute2, 0.8, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode t, FlowNode freq, Func<FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[t, freq, sound, vol, len]).SetName(callerMemberName);

        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute2(FlowNode freq);
        /// _
        /// [ t[1, 1], A4, Flute2 ]
        /// [ t[1, 2], C5, Flute2, MyCurve ]
        /// [ t[1, 3], E5, Flute2, MyCurve, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode t, FlowNode freq, Func<FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[t, freq, sound, vol, len]).SetName(callerMemberName);
        
        // Instrument with 2 Parameters Freq and Len
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute3(FlowNode freq, FlowNode len = null);
        /// _
        /// [ A4, Flute3, 0.8 ]
        /// [ A4, Flute3, 0.8, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode freq, Func<FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[freq, sound, vol, len]).SetName(callerMemberName);
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute3(FlowNode freq, FlowNode len = null);
        /// _
        /// [ A4, Flute3 ]
        /// [ A4, Flute3, MyCurve ]
        /// [ A4, Flute3, MyCurve, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode freq, Func<FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[freq, sound, vol, len]).SetName(callerMemberName);
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute3(FlowNode freq, FlowNode len = null);
        /// _
        /// [ 0.00, A4, Flute3, 0.8 ]
        /// [ 0.25, C5, Flute3, 0.8, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            double t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[t, freq, sound, vol, len]).SetName(callerMemberName);
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute3(FlowNode freq, FlowNode len = null);
        /// _
        /// [ 0.00, A4, Flute3 ]
        /// [ 0.25, C5, Flute3, MyCurve ]
        /// [ 0.50, E5, Flute3, MyCurve, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            double t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[t, freq, sound, vol, len]).SetName(callerMemberName);
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute3(FlowNode freq, FlowNode len = null);
        /// _
        /// [ t[1, 1], A4, Flute3, 0.8 ]
        /// [ t[1, 2], C5, Flute3, 0.8, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null,
            [CallerMemberName] string callerMemberName = null]
            => Add(_[t, freq, sound, vol, len]).SetName(callerMemberName);

        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute3(FlowNode freq, FlowNode len = null);
        /// _
        /// [ t[1, 1], A4, Flute3 ]
        /// [ t[1, 2], C5, Flute3, MyCurve ]
        /// [ t[1, 3], E5, Flute3, MyCurve, l[0.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[t, freq, sound, vol, len]).SetName(callerMemberName);

        // Instruments with 1 Effect Parameter (Optional)
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute4(FlowNode freq, FlowNode len = null, FlowNode fx1 = null);
        /// _
        /// [ A4, Flute4, 0.8 ]
        /// [ A4, Flute4, 0.8, l[0.5] ]
        /// [ A4, Flute4, 0.8, l[0.5], _[0.14] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, FlowNode fx1 = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[freq, sound, vol, len, fx1]).SetName(callerMemberName);
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute4(FlowNode freq, FlowNode len = null, FlowNode fx1 = null);
        /// _
        /// [ A4, Flute4 ]
        /// [ A4, Flute4, MyCurve ]
        /// [ A4, Flute4, MyCurve, l[0.5] ]
        /// [ A4, Flute4, MyCurve, l[0.5], _[0.14] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, FlowNode fx1 = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[freq, sound, vol, len, fx1]).SetName(callerMemberName);
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute4(FlowNode freq, FlowNode len = null, FlowNode fx1 = null);
        /// _
        /// [ 0.00, A4, Flute4, 0.8 ]
        /// [ 0.25, C5, Flute4, 0.8, l[0.5] ]
        /// [ 0.50, E5, Flute4, 0.8, l[0.5], _[0.14] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            double t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, FlowNode fx1 = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[t, freq, sound, vol, len, fx1]).SetName(callerMemberName);
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute4(FlowNode freq, FlowNode len = null, FlowNode fx1 = null);
        /// _
        /// [ 0.00, A4, Flute4 ]
        /// [ 0.25, C5, Flute4, MyCurve ]
        /// [ 0.50, E5, Flute4, MyCurve, l[0.5] ]
        /// [ 0.75, G5, Flute4, MyCurve, l[0.5], _[0.14] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            double t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, FlowNode fx1 = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[t, freq, sound, vol, len, fx1]).SetName(callerMemberName);
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute4(FlowNode freq, FlowNode len = null, FlowNode fx1 = null);
        /// _
        /// [ t[1, 1], A4, Flute4, 0.8 ]
        /// [ t[1, 2], C5, Flute4, 0.8, l[0.5] ]
        /// [ t[1, 3], E5, Flute4, 0.8, l[0.5], _[0.14] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, FlowNode fx1 = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[t, freq, sound, vol, len, fx1]).SetName(callerMemberName);

        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute4(FlowNode freq, FlowNode len = null, FlowNode fx1 = null);
        /// _
        /// [ t[1, 1], A4, Flute4 ]
        /// [ t[1, 2], C5, Flute4, MyCurve ]
        /// [ t[1, 3], E5, Flute4, MyCurve, l[0.5] ]
        /// [ t[1, 4], G5, Flute4, MyCurve, l[0.5], _[0.14] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode> sound, 
            FlowNode vol = null, FlowNode len = null, FlowNode fx1 = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[t, freq, sound, vol, len, fx1]).SetName(callerMemberName);
        
        // Instruments with 2 Effect Parameters (Optional)
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute5(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null);
        /// _
        /// [ A4, Flute5, 0.8 ]
        /// [ A4, Flute5, 0.8, l[0.5] ]
        /// [ A4, Flute5, 0.8, l[0.5], _[0.14] ]
        /// [ A4, Flute5, 0.8, l[0.5], _[0.14], _[1.08] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[freq, sound, vol, len, fx1, fx2]).SetName(callerMemberName);
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute5(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null);
        /// _
        /// [ A4, Flute5 ]
        /// [ A4, Flute5, MyCurve ]
        /// [ A4, Flute5, MyCurve, l[0.5] ]
        /// [ A4, Flute5, MyCurve, l[0.5], _[0.14] ]
        /// [ A4, Flute5, MyCurve, l[0.5], _[0.14], _[1.08] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[freq, sound, vol, len, fx1, fx2]).SetName(callerMemberName);
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute5(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null);
        /// _
        /// [ 0.00, A4, Flute5, 0.8 ]
        /// [ 0.25, C5, Flute5, 0.8, l[0.5] ]
        /// [ 0.50, E5, Flute5, 0.8, l[0.5], _[0.14] ]
        /// [ 0.75, G5, Flute5, 0.8, l[0.5], _[0.14], _[1.08] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            double t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[t, freq, sound, vol, len, fx1, fx2]).SetName(callerMemberName);
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute5(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null);
        /// _
        /// [ 0.00, A4, Flute5 ]
        /// [ 0.25, C5, Flute5, MyCurve ]
        /// [ 0.50, E5, Flute5, MyCurve, l[0.5] ]
        /// [ 0.75, G5, Flute5, MyCurve, l[0.5], _[0.14] ]
        /// [ 1.00, A5, Flute5, MyCurve, l[0.5], _[0.14], _[1.08] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            double t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[t, freq, sound, vol, len, fx1, fx2]).SetName(callerMemberName);
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute5(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null);
        /// _
        /// [ t[1, 1], A4, Flute5, 0.8 ]
        /// [ t[1, 2], C5, Flute5, 0.8, l[0.5] ]
        /// [ t[1, 3], E5, Flute5, 0.8, l[0.5], _[0.14] ]
        /// [ t[1, 4], G5, Flute5, 0.8, l[0.5], _[0.14], _[1.08] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[t, freq, sound, vol, len, fx1, fx2]).SetName(callerMemberName);

        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute5(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null);
        /// _
        /// [ t[1, 1], A4, Flute5 ]
        /// [ t[1, 2], C5, Flute5, MyCurve ]
        /// [ t[1, 3], E5, Flute5, MyCurve, l[0.5] ]
        /// [ t[1, 4], G5, Flute5, MyCurve, l[0.5], _[0.14] ]
        /// [ t[2, 1], A5, Flute5, MyCurve, l[0.5], _[0.14], _[1.08] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[t, freq, sound, vol, len, fx1, fx2]).SetName(callerMemberName);
        
        // Instruments with 3 Effect Parameters (Optional)
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute6(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null);
        /// _
        /// [ A4, Flute6, 0.8 ]
        /// [ A4, Flute6, 0.8, l[0.5] ]
        /// [ A4, Flute6, 0.8, l[0.5], _[0.14] ]
        /// [ A4, Flute6, 0.8, l[0.5], _[0.14], _[1.08], _[0.03] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[freq, sound, vol, len, fx1, fx2, fx3]).SetName(callerMemberName);
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute6(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null);
        /// _
        /// [ A4, Flute6 ]
        /// [ A4, Flute6, MyCurve ]
        /// [ A4, Flute6, MyCurve, l[0.5] ]
        /// [ A4, Flute6, MyCurve, l[0.5], _[0.14] ]
        /// [ A4, Flute6, MyCurve, l[0.5], _[0.14], _[1.08], _[0.03] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[freq, sound, vol, len, fx1, fx2, fx3]).SetName(callerMemberName);
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute6(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null);
        /// _
        /// [ 0.00, A4, Flute6, 0.8 ]
        /// [ 0.25, C5, Flute6, 0.8, l[0.5] ]
        /// [ 0.50, E5, Flute6, 0.8, l[0.5], _[0.14] ]
        /// [ 0.75, G5, Flute6, 0.8, l[0.5], _[0.14], _[1.08], _[0.03] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            double t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[t, freq, sound, vol, len, fx1, fx2, fx3]).SetName(callerMemberName);
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute6(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null);
        /// _
        /// [ 0.00, A4, Flute6 ]
        /// [ 0.25, C5, Flute6, MyCurve ]
        /// [ 0.50, E5, Flute6, MyCurve, l[0.5] ]
        /// [ 0.75, G5, Flute6, MyCurve, l[0.5], _[0.14] ]
        /// [ 1.00, A5, Flute6, MyCurve, l[0.5], _[0.14], _[1.08], _[0.03] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            double t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[t, freq, sound, vol, len, fx1, fx2, fx3]).SetName(callerMemberName);
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute6(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null);
        /// _
        /// [ t[1, 1], A4, Flute6, 0.8 ]
        /// [ t[1, 2], C5, Flute6, 0.8, l[0.5] ]
        /// [ t[1, 3], E5, Flute6, 0.8, l[0.5], _[0.14] ]
        /// [ t[1, 4], G5, Flute6, 0.8, l[0.5], _[0.14], _[1.08], _[0.03] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[t, freq, sound, vol, len, fx1, fx2, fx3]).SetName(callerMemberName);

        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute6(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null);
        /// _
        /// [ t[1, 1], A4, Flute6 ]
        /// [ t[1, 2], C5, Flute6, MyCurve ]
        /// [ t[1, 3], E5, Flute6, MyCurve, l[0.5] ]
        /// [ t[1, 4], G5, Flute6, MyCurve, l[0.5], _[0.14] ]
        /// [ t[2, 1], A5, Flute6, MyCurve, l[0.5], _[0.14], _[1.08], _[0.03] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[t, freq, sound, vol, len, fx1, fx2, fx3]).SetName(callerMemberName);

        // Instruments with 4 Effect Parameters (Optional)
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute7(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, FlowNode fx4 = null);
        /// _
        /// [ A4, Flute7, 0.8 ]
        /// [ A4, Flute7, 0.8, l[0.5] ]
        /// [ A4, Flute7, 0.8, l[0.5], _[0.14] ]
        /// [ A4, Flute7, 0.8, l[0.5], _[0.14], _[1.08] ]
        /// [ A4, Flute7, 0.8, l[0.5], _[0.14], _[1.08], _[0.03] ]
        /// [ A4, Flute7, 0.8, l[0.5], _[0.14], _[1.08], _[0.03], _[2.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, FlowNode fx4 = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[freq, sound, vol, len, fx1, fx2, fx3, fx4]).SetName(callerMemberName);
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute7(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, FlowNode fx4 = null);
        /// _
        /// [ A4, Flute7 ]
        /// [ A4, Flute7, MyCurve ]
        /// [ A4, Flute7, MyCurve, l[0.5] ]
        /// [ A4, Flute7, MyCurve, l[0.5], _[0.14] ]
        /// [ A4, Flute7, MyCurve, l[0.5], _[0.14], _[1.08] ]
        /// [ A4, Flute7, MyCurve, l[0.5], _[0.14], _[1.08], _[0.03] ]
        /// [ A4, Flute7, MyCurve, l[0.5], _[0.14], _[1.08], _[0.03], _[2.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, FlowNode fx4 = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[freq, sound, vol, len, fx1, fx2, fx3, fx4]).SetName(callerMemberName);
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute7(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, FlowNode fx4 = null);
        /// _
        /// [ 0.00, A4, Flute7, 0.8 ]
        /// [ 0.25, C5, Flute7, 0.8, l[0.5] ]
        /// [ 0.50, E5, Flute7, 0.8, l[0.5], _[0.14] ]
        /// [ 0.75, G5, Flute7, 0.8, l[0.5], _[0.14], _[1.08] ]
        /// [ 1.00, A5, Flute7, 0.8, l[0.5], _[0.14], _[1.08], _[0.03] ]
        /// [ 1.25, A3, Flute7, 0.8, l[0.5], _[0.14], _[1.08], _[0.03], _[2.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            double t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, FlowNode fx4 = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[t, freq, sound, vol, len, fx1, fx2, fx3, fx4]).SetName(callerMemberName);
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute7(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, FlowNode fx4 = null);
        /// _
        /// [ 0.00, A4, Flute7 ]
        /// [ 0.25, C5, Flute7, MyCurve ]
        /// [ 0.50, E5, Flute7, MyCurve, l[0.5] ]
        /// [ 0.75, G5, Flute7, MyCurve, l[0.5], _[0.14] ]
        /// [ 1.00, A5, Flute7, MyCurve, l[0.5], _[0.14], _[1.08] ]
        /// [ 1.25, A3, Flute7, MyCurve, l[0.5], _[0.14], _[1.08], _[0.03] ]
        /// [ 1.50, C4, Flute7, MyCurve, l[0.5], _[0.14], _[1.08], _[0.03], _[2.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            double t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, FlowNode fx4 = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[t, freq, sound, vol, len, fx1, fx2, fx3, fx4]).SetName(callerMemberName);
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute7(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, FlowNode fx4 = null);
        /// _
        /// [ t[1, 1], A4, Flute7, 0.8 ]
        /// [ t[1, 2], C5, Flute7, 0.8, l[0.5] ]
        /// [ t[1, 3], E5, Flute7, 0.8, l[0.5], _[0.14] ]
        /// [ t[1, 4], G5, Flute7, 0.8, l[0.5], _[0.14], _[1.08] ]
        /// [ t[2, 1], A5, Flute7, 0.8, l[0.5], _[0.14], _[1.08], _[0.03] ]
        /// [ t[2, 2], A3, Flute7, 0.8, l[0.5], _[0.14], _[1.08], _[0.03], _[2.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            double vol, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, FlowNode fx4 = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[t, freq, sound, vol, len, fx1, fx2, fx3, fx4]).SetName(callerMemberName);
        
        /// <summary>
        /// <strong> Note Indexers </strong> <code>
        /// FlowNode Flute7(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, FlowNode fx4 = null);
        /// _
        /// [ t[1, 1], A4, Flute7 ]
        /// [ t[1, 2], C5, Flute7, MyCurve ]
        /// [ t[1, 3], E5, Flute7, MyCurve, l[0.5] ]
        /// [ t[1, 4], G5, Flute7, MyCurve, l[0.5], _[0.14] ]
        /// [ t[2, 1], A5, Flute7, MyCurve, l[0.5], _[0.14], _[1.08] ]
        /// [ t[2, 2], A3, Flute7, MyCurve, l[0.5], _[0.14], _[1.08], _[0.03] ]
        /// [ t[2, 3], C4, Flute7, MyCurve, l[0.5], _[0.14], _[1.08], _[0.03], _[2.5] ]
        /// </code></summary>
        /// <inheritdoc cref="docs._noteindexer" />
        public FlowNode this[
            FlowNode t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound,
            FlowNode vol = null, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, FlowNode fx4 = null, 
            [CallerMemberName] string callerMemberName = null]
            => Add(_[t, freq, sound, vol, len, fx1, fx2, fx3, fx4]).SetName(callerMemberName);
    }}
