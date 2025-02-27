using System;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Extensions;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

// ReSharper disable ParameterHidesMember
// ReSharper disable InconsistentNaming

// ReSharper disable once CheckNamespace
namespace JJ.Business.Synthesizer.Wishes
{
    // Notes
    
    public partial class SynthWishes
    {
        // Notes
        
        public FlowNode A_Minus1 => _[Notes.A_Minus1];
        public FlowNode A_Minus1_Sharp => _[Notes.A_Minus1_Sharp];
        public FlowNode B_Minus1 => _[Notes.B_Minus1];
        public FlowNode C0  => _[Notes.C0 ];
        public FlowNode Cs0 => _[Notes.Cs0];
        public FlowNode D0  => _[Notes.D0 ];
        public FlowNode Ds0 => _[Notes.Ds0];
        public FlowNode E0  => _[Notes.E0 ];
        public FlowNode F0  => _[Notes.F0 ];
        public FlowNode Fs0 => _[Notes.Fs0];
        public FlowNode G0  => _[Notes.G0 ];
        public FlowNode Gs0 => _[Notes.Gs0];
        public FlowNode A0  => _[Notes.A0 ];
        public FlowNode As0 => _[Notes.As0];
        public FlowNode B0  => _[Notes.B0 ];
        public FlowNode C1  => _[Notes.C1 ];
        public FlowNode Cs1 => _[Notes.Cs1];
        public FlowNode D1  => _[Notes.D1 ];
        public FlowNode Ds1 => _[Notes.Ds1];
        public FlowNode E1  => _[Notes.E1 ];
        public FlowNode F1  => _[Notes.F1 ];
        public FlowNode Fs1 => _[Notes.Fs1];
        public FlowNode G1  => _[Notes.G1 ];
        public FlowNode Gs1 => _[Notes.Gs1];
        public FlowNode A1  => _[Notes.A1 ];
        public FlowNode As1 => _[Notes.As1];
        public FlowNode B1  => _[Notes.B1 ];
        public FlowNode C2  => _[Notes.C2 ];
        public FlowNode Cs2 => _[Notes.Cs2];
        public FlowNode D2  => _[Notes.D2 ];
        public FlowNode Ds2 => _[Notes.Ds2];
        public FlowNode E2  => _[Notes.E2 ];
        public FlowNode F2  => _[Notes.F2 ];
        public FlowNode Fs2 => _[Notes.Fs2];
        public FlowNode G2  => _[Notes.G2 ];
        public FlowNode Gs2 => _[Notes.Gs2];
        public FlowNode A2  => _[Notes.A2 ];
        public FlowNode As2 => _[Notes.As2];
        public FlowNode B2  => _[Notes.B2 ];
        public FlowNode C3  => _[Notes.C3 ];
        public FlowNode Cs3 => _[Notes.Cs3];
        public FlowNode D3  => _[Notes.D3 ];
        public FlowNode Ds3 => _[Notes.Ds3];
        public FlowNode E3  => _[Notes.E3 ];
        public FlowNode F3  => _[Notes.F3 ];
        public FlowNode Fs3 => _[Notes.Fs3];
        public FlowNode G3  => _[Notes.G3 ];
        public FlowNode Gs3 => _[Notes.Gs3];
        public FlowNode A3  => _[Notes.A3 ];
        public FlowNode As3 => _[Notes.As3];
        public FlowNode B3  => _[Notes.B3 ];
        public FlowNode C4  => _[Notes.C4 ];
        public FlowNode Cs4 => _[Notes.Cs4];
        public FlowNode D4  => _[Notes.D4 ];
        public FlowNode Ds4 => _[Notes.Ds4];
        public FlowNode E4  => _[Notes.E4 ];
        public FlowNode F4  => _[Notes.F4 ];
        public FlowNode Fs4 => _[Notes.Fs4];
        public FlowNode G4  => _[Notes.G4 ];
        public FlowNode Gs4 => _[Notes.Gs4];
        public FlowNode A4  => _[Notes.A4 ];
        public FlowNode As4 => _[Notes.As4];
        public FlowNode B4  => _[Notes.B4 ];
        public FlowNode C5  => _[Notes.C5 ];
        public FlowNode Cs5 => _[Notes.Cs5];
        public FlowNode D5  => _[Notes.D5 ];
        public FlowNode Ds5 => _[Notes.Ds5];
        public FlowNode E5  => _[Notes.E5 ];
        public FlowNode F5  => _[Notes.F5 ];
        public FlowNode Fs5 => _[Notes.Fs5];
        public FlowNode G5  => _[Notes.G5 ];
        public FlowNode Gs5 => _[Notes.Gs5];
        public FlowNode A5  => _[Notes.A5 ];
        public FlowNode As5 => _[Notes.As5];
        public FlowNode B5  => _[Notes.B5 ];
        public FlowNode C6  => _[Notes.C6 ];
        public FlowNode Cs6 => _[Notes.Cs6];
        public FlowNode D6  => _[Notes.D6 ];
        public FlowNode Ds6 => _[Notes.Ds6];
        public FlowNode E6  => _[Notes.E6 ];
        public FlowNode F6  => _[Notes.F6 ];
        public FlowNode Fs6 => _[Notes.Fs6];
        public FlowNode G6  => _[Notes.G6 ];
        public FlowNode Gs6 => _[Notes.Gs6];
        public FlowNode A6  => _[Notes.A6 ];
        public FlowNode As6 => _[Notes.As6];
        public FlowNode B6  => _[Notes.B6 ];
        public FlowNode C7  => _[Notes.C7 ];
        public FlowNode Cs7 => _[Notes.Cs7];
        public FlowNode D7  => _[Notes.D7 ];
        public FlowNode Ds7 => _[Notes.Ds7];
        public FlowNode E7  => _[Notes.E7 ];
        public FlowNode F7  => _[Notes.F7 ];
        public FlowNode Fs7 => _[Notes.Fs7];
        public FlowNode G7  => _[Notes.G7 ];
        public FlowNode Gs7 => _[Notes.Gs7];
        public FlowNode A7  => _[Notes.A7 ];
        public FlowNode As7 => _[Notes.As7];
        public FlowNode B7  => _[Notes.B7 ];
        public FlowNode C8  => _[Notes.C8 ];
        public FlowNode Cs8 => _[Notes.Cs8];
        public FlowNode D8  => _[Notes.D8 ];
        public FlowNode Ds8 => _[Notes.Ds8];
        public FlowNode E8  => _[Notes.E8 ];
        public FlowNode F8  => _[Notes.F8 ];
        public FlowNode Fs8 => _[Notes.Fs8];
        public FlowNode G8  => _[Notes.G8 ];
        public FlowNode Gs8 => _[Notes.Gs8];
        public FlowNode A8  => _[Notes.A8 ];
        public FlowNode As8 => _[Notes.As8];
        public FlowNode B8  => _[Notes.B8 ];
        public FlowNode C9  => _[Notes.C9 ];
        public FlowNode Cs9 => _[Notes.Cs9];
        public FlowNode D9  => _[Notes.D9 ];
        public FlowNode Ds9 => _[Notes.Ds9];
        public FlowNode E9  => _[Notes.E9 ];
        public FlowNode F9  => _[Notes.F9 ];
        public FlowNode Fs9 => _[Notes.Fs9];
        public FlowNode G9  => _[Notes.G9 ];
        public FlowNode Gs9 => _[Notes.Gs9];

        public FlowNode SemiToneFactor => _[Notes.SemiToneFactor];

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

        // Note Operator
        
        /// <inheritdoc cref="docs._note" />
        public FlowNode Note(
            FlowNode sound, FlowNode delay = default, FlowNode volume = default, FlowNode noteLength = default, 
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            // A little optimization, because so slow...
            bool delayFilledIn = delay != null && delay.AsConst != 0;
            bool volumeFilledIn = volume != null && volume.AsConst != 1;

            // Resolve Name
            string resolvedName = ResolveName(name, sound, callerMemberName);
            if (FilledIn(resolvedName))
            {
                resolvedName += " " + MemberName();
            }
            
            // Resolve NoteLength
            noteLength = GetNoteLengthSnapShot(noteLength);
            
            // Apply Volume
            if (volumeFilledIn)
            {
                sound *= volume.Stretch(noteLength / GetVolumeDuration(volume));
            }
            
            // Defer Taping
            sound = sound.Tape(noteLength).SetName(resolvedName);
            
            // Apply Delay
            if (delayFilledIn) sound = Delay(sound, delay);
            
            // Extend AudioLength
            EnsureAudioLength(delay + noteLength);
            
            return sound.SetName(resolvedName);
        }
        
        /// <inheritdoc cref="docs._note" />
        public FlowNode Note(
            FlowNode sound, FlowNode delay, double volume, FlowNode noteLength, 
            string name = null, [CallerMemberName] string callerMemberName = null)
            => Note(sound, delay, _[volume], noteLength, name, callerMemberName);
        
        /// <inheritdoc cref="docs._note" />
        public FlowNode Note(
            FlowNode sound, FlowNode delay, double volume,
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => Note(sound, delay, _[volume], default, name, callerMemberName);
               
        private static double GetVolumeDuration(FlowNode volume)
        {
            if (volume.IsCurve)
            {
                return volume.UnderlyingCurve().Nodes.Max(x => x.Time);
            }
            
            if (volume.IsSample)
            {
                return volume.UnderlyingSample().GetDuration();
            }
            
            return 1;
        }

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