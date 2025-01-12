using System;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Extensions;
using static JJ.Framework.Wishes.JJ_Framework_Common_Wishes.FilledInWishes;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

// ReSharper disable ParameterHidesMember
// ReSharper disable InconsistentNaming

namespace JJ.Business.Synthesizer.Wishes
{
    // Notes
    
    public static class Notes
    {
        public static double SemiToneFactor { get; } = Math.Pow(2.0, 1.0 / 12.0);

        public const double A_Minus1 = 13.75;
        public static double A_Minus1_Sharp { get; } = A_Minus1 * Math.Pow(SemiToneFactor, 1.0);
        public static double B_Minus1 { get; } = A_Minus1 * Math.Pow(SemiToneFactor, 2.0);
        public static double C0  { get; } = A_Minus1 * Math.Pow(SemiToneFactor, 3.0);
        public static double Cs0 { get; } = A_Minus1 * Math.Pow(SemiToneFactor, 4.0);
        public static double D0  { get; } = A_Minus1 * Math.Pow(SemiToneFactor, 5.0);
        public static double Ds0 { get; } = A_Minus1 * Math.Pow(SemiToneFactor, 6.0);
        public static double E0  { get; } = A_Minus1 * Math.Pow(SemiToneFactor, 7.0);
        public static double F0  { get; } = A_Minus1 * Math.Pow(SemiToneFactor, 8.0);
        public static double Fs0 { get; } = A_Minus1 * Math.Pow(SemiToneFactor, 9.0);
        public static double G0  { get; } = A_Minus1 * Math.Pow(SemiToneFactor, 10.0);
        public static double Gs0 { get; } = A_Minus1 * Math.Pow(SemiToneFactor, 11.0);

        public const double A0 = 27.5;
        public static double As0 { get; } = A0 * Math.Pow(SemiToneFactor, 1.0);
        public static double B0  { get; } = A0 * Math.Pow(SemiToneFactor, 2.0);
        public static double C1  { get; } = A0 * Math.Pow(SemiToneFactor, 3.0);
        public static double Cs1 { get; } = A0 * Math.Pow(SemiToneFactor, 4.0);
        public static double D1  { get; } = A0 * Math.Pow(SemiToneFactor, 5.0);
        public static double Ds1 { get; } = A0 * Math.Pow(SemiToneFactor, 6.0);
        public static double E1  { get; } = A0 * Math.Pow(SemiToneFactor, 7.0);
        public static double F1  { get; } = A0 * Math.Pow(SemiToneFactor, 8.0);
        public static double Fs1 { get; } = A0 * Math.Pow(SemiToneFactor, 9.0);
        public static double G1  { get; } = A0 * Math.Pow(SemiToneFactor, 10.0);
        public static double Gs1 { get; } = A0 * Math.Pow(SemiToneFactor, 11.0);

        public const double A1 = 55;
        public static double As1 { get; } = A1 * Math.Pow(SemiToneFactor, 1.0);
        public static double B1  { get; } = A1 * Math.Pow(SemiToneFactor, 2.0);
        public static double C2  { get; } = A1 * Math.Pow(SemiToneFactor, 3.0);
        public static double Cs2 { get; } = A1 * Math.Pow(SemiToneFactor, 4.0);
        public static double D2  { get; } = A1 * Math.Pow(SemiToneFactor, 5.0);
        public static double Ds2 { get; } = A1 * Math.Pow(SemiToneFactor, 6.0);
        public static double E2  { get; } = A1 * Math.Pow(SemiToneFactor, 7.0);
        public static double F2  { get; } = A1 * Math.Pow(SemiToneFactor, 8.0);
        public static double Fs2 { get; } = A1 * Math.Pow(SemiToneFactor, 9.0);
        public static double G2  { get; } = A1 * Math.Pow(SemiToneFactor, 10.0);
        public static double Gs2 { get; } = A1 * Math.Pow(SemiToneFactor, 11.0);

        public const double A2 = 110;
        public static double As2 { get; } = A2 * Math.Pow(SemiToneFactor, 1.0);
        public static double B2  { get; } = A2 * Math.Pow(SemiToneFactor, 2.0);
        public static double C3  { get; } = A2 * Math.Pow(SemiToneFactor, 3.0);
        public static double Cs3 { get; } = A2 * Math.Pow(SemiToneFactor, 4.0);
        public static double D3  { get; } = A2 * Math.Pow(SemiToneFactor, 5.0);
        public static double Ds3 { get; } = A2 * Math.Pow(SemiToneFactor, 6.0);
        public static double E3  { get; } = A2 * Math.Pow(SemiToneFactor, 7.0);
        public static double F3  { get; } = A2 * Math.Pow(SemiToneFactor, 8.0);
        public static double Fs3 { get; } = A2 * Math.Pow(SemiToneFactor, 9.0);
        public static double G3  { get; } = A2 * Math.Pow(SemiToneFactor, 10.0);
        public static double Gs3 { get; } = A2 * Math.Pow(SemiToneFactor, 11.0);

        public const double A3 = 220;
        public static double As3 { get; } = A3 * Math.Pow(SemiToneFactor, 1.0);
        public static double B3  { get; } = A3 * Math.Pow(SemiToneFactor, 2.0);
        public static double C4  { get; } = A3 * Math.Pow(SemiToneFactor, 3.0);
        public static double Cs4 { get; } = A3 * Math.Pow(SemiToneFactor, 4.0);
        public static double D4  { get; } = A3 * Math.Pow(SemiToneFactor, 5.0);
        public static double Ds4 { get; } = A3 * Math.Pow(SemiToneFactor, 6.0);
        public static double E4  { get; } = A3 * Math.Pow(SemiToneFactor, 7.0);
        public static double F4  { get; } = A3 * Math.Pow(SemiToneFactor, 8.0);
        public static double Fs4 { get; } = A3 * Math.Pow(SemiToneFactor, 9.0);
        public static double G4  { get; } = A3 * Math.Pow(SemiToneFactor, 10.0);
        public static double Gs4 { get; } = A3 * Math.Pow(SemiToneFactor, 11.0);

        public const double A4 = 440;
        public static double As4 { get; } = A4 * Math.Pow(SemiToneFactor, 1.0);
        public static double B4  { get; } = A4 * Math.Pow(SemiToneFactor, 2.0);
        public static double C5  { get; } = A4 * Math.Pow(SemiToneFactor, 3.0);
        public static double Cs5 { get; } = A4 * Math.Pow(SemiToneFactor, 4.0);
        public static double D5  { get; } = A4 * Math.Pow(SemiToneFactor, 5.0);
        public static double Ds5 { get; } = A4 * Math.Pow(SemiToneFactor, 6.0);
        public static double E5  { get; } = A4 * Math.Pow(SemiToneFactor, 7.0);
        public static double F5  { get; } = A4 * Math.Pow(SemiToneFactor, 8.0);
        public static double Fs5 { get; } = A4 * Math.Pow(SemiToneFactor, 9.0);
        public static double G5  { get; } = A4 * Math.Pow(SemiToneFactor, 10.0);
        public static double Gs5 { get; } = A4 * Math.Pow(SemiToneFactor, 11.0);

        public const double A5 = 880;
        public static double As5 { get; } = A5 * Math.Pow(SemiToneFactor, 1.0);
        public static double B5  { get; } = A5 * Math.Pow(SemiToneFactor, 2.0);
        public static double C6  { get; } = A5 * Math.Pow(SemiToneFactor, 3.0);
        public static double Cs6 { get; } = A5 * Math.Pow(SemiToneFactor, 4.0);
        public static double D6  { get; } = A5 * Math.Pow(SemiToneFactor, 5.0);
        public static double Ds6 { get; } = A5 * Math.Pow(SemiToneFactor, 6.0);
        public static double E6  { get; } = A5 * Math.Pow(SemiToneFactor, 7.0);
        public static double F6  { get; } = A5 * Math.Pow(SemiToneFactor, 8.0);
        public static double Fs6 { get; } = A5 * Math.Pow(SemiToneFactor, 9.0);
        public static double G6  { get; } = A5 * Math.Pow(SemiToneFactor, 10.0);
        public static double Gs6 { get; } = A5 * Math.Pow(SemiToneFactor, 11.0);

        public const double A6 = 1760;
        public static double As6 { get; } = A6 * Math.Pow(SemiToneFactor, 1.0);
        public static double B6  { get; } = A6 * Math.Pow(SemiToneFactor, 2.0);
        public static double C7  { get; } = A6 * Math.Pow(SemiToneFactor, 3.0);
        public static double Cs7 { get; } = A6 * Math.Pow(SemiToneFactor, 4.0);
        public static double D7  { get; } = A6 * Math.Pow(SemiToneFactor, 5.0);
        public static double Ds7 { get; } = A6 * Math.Pow(SemiToneFactor, 6.0);
        public static double E7  { get; } = A6 * Math.Pow(SemiToneFactor, 7.0);
        public static double F7  { get; } = A6 * Math.Pow(SemiToneFactor, 8.0);
        public static double Fs7 { get; } = A6 * Math.Pow(SemiToneFactor, 9.0);
        public static double G7  { get; } = A6 * Math.Pow(SemiToneFactor, 10.0);
        public static double Gs7 { get; } = A6 * Math.Pow(SemiToneFactor, 11.0);

        public const double A7 = 3520;
        public static double As7 { get; } = A7 * Math.Pow(SemiToneFactor, 1.0);
        public static double B7  { get; } = A7 * Math.Pow(SemiToneFactor, 2.0);
        public static double C8  { get; } = A7 * Math.Pow(SemiToneFactor, 3.0);
        public static double Cs8 { get; } = A7 * Math.Pow(SemiToneFactor, 4.0);
        public static double D8  { get; } = A7 * Math.Pow(SemiToneFactor, 5.0);
        public static double Ds8 { get; } = A7 * Math.Pow(SemiToneFactor, 6.0);
        public static double E8  { get; } = A7 * Math.Pow(SemiToneFactor, 7.0);
        public static double F8  { get; } = A7 * Math.Pow(SemiToneFactor, 8.0);
        public static double Fs8 { get; } = A7 * Math.Pow(SemiToneFactor, 9.0);
        public static double G8  { get; } = A7 * Math.Pow(SemiToneFactor, 10.0);
        public static double Gs8 { get; } = A7 * Math.Pow(SemiToneFactor, 11.0);

        public const double A8 = 7040;
        public static double As8 { get; } = A8 * Math.Pow(SemiToneFactor, 1.0);
        public static double B8  { get; } = A8 * Math.Pow(SemiToneFactor, 2.0);
        public static double C9  { get; } = A8 * Math.Pow(SemiToneFactor, 3.0);
        public static double Cs9 { get; } = A8 * Math.Pow(SemiToneFactor, 4.0);
        public static double D9  { get; } = A8 * Math.Pow(SemiToneFactor, 5.0);
        public static double Ds9 { get; } = A8 * Math.Pow(SemiToneFactor, 6.0);
        public static double E9  { get; } = A8 * Math.Pow(SemiToneFactor, 7.0);
        public static double F9  { get; } = A8 * Math.Pow(SemiToneFactor, 8.0);
        public static double Fs9 { get; } = A8 * Math.Pow(SemiToneFactor, 9.0);
        public static double G9  { get; } = A8 * Math.Pow(SemiToneFactor, 10.0);
        public static double Gs9 { get; } = A8 * Math.Pow(SemiToneFactor, 11.0);

        /*
        /// <param name="octave">Typically between 0 and 8. A 440Hz is in octave 4.</param>
        /// <param name="semiTone">Typically between 1 (C) and 12 (B).</param>
        public double GetNote(int octave, int semiTone)
        {
            const int oneToZeroBasedOffset = - 1
            const int aToCSemiToneOffset = - 9;
            int octaveOffsetFromA4 = octave - 4;
            int semiToneOffsetFromA4 = semiTone + oneToZeroBasedOffset + aToCSemiToneOffset;
            return A4 * Math.Pow(2.0, octaveOffsetFromA4) * Math.Pow(SemiToneFactor, semiToneOffsetFromA4);
        }
        */
    }
    
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
    }

    // Timing Indexers
    
    /// <inheritdoc cref="docs._barindexer"/>
    public class BarIndexer
    {
        private readonly SynthWishes _synthWishes;

        /// <inheritdoc cref="docs._barindexer"/>
        internal BarIndexer(SynthWishes synthWishes) 
            => _synthWishes = synthWishes;
        
        /// <inheritdoc cref="docs._barindexer"/>
        public FlowNode this[double count]
            => (count - 1) * _synthWishes.GetBarLength;
        
        /// <inheritdoc cref="docs._barindexer"/>
        public FlowNode this[FlowNode count]
            => (count - 1) * _synthWishes.GetBarLength;
    }

    /// <inheritdoc cref="docs._barsindexer"/>
    public class BarsIndexer
    {
        private readonly SynthWishes _synthWishes;

        /// <inheritdoc cref="docs._barsindexer"/>
        internal BarsIndexer(SynthWishes synthWishes) 
            => _synthWishes = synthWishes;
        
        /// <inheritdoc cref="docs._barsindexer"/>
        public FlowNode this[double count]
            => count * _synthWishes.GetBarLength;
        
        /// <inheritdoc cref="docs._barsindexer"/>
        public FlowNode this[FlowNode count]
            => count * _synthWishes.GetBarLength;
    }

    /// <inheritdoc cref="docs._beatindexer"/>
    public class BeatIndexer
    {
        private readonly SynthWishes _synthWishes;

        /// <inheritdoc cref="docs._beatindexer"/>
        internal BeatIndexer(SynthWishes synthWishes) 
            => _synthWishes = synthWishes;
        
        /// <inheritdoc cref="docs._beatindexer"/>
        public FlowNode this[double count] 
            => (count - 1) * _synthWishes.GetBeatLength;
        
        /// <inheritdoc cref="docs._beatindexer"/>
        public FlowNode this[FlowNode count]
            => (count - 1) * _synthWishes.GetBeatLength;
    }

    /// <inheritdoc cref="docs._beatsindexer"/>
    public class BeatsIndexer
    {
        private readonly SynthWishes _synthWishes;

        /// <inheritdoc cref="docs._beatsindexer"/>
        internal BeatsIndexer(SynthWishes synthWishes) 
            => _synthWishes = synthWishes;
        
        /// <inheritdoc cref="docs._beatsindexer"/>
        public FlowNode this[double count]
            => count * _synthWishes.GetBeatLength;

        /// <inheritdoc cref="docs._beatsindexer"/>
        public FlowNode this[FlowNode count]
            => count * _synthWishes.GetBeatLength;
    }

    /// <inheritdoc cref="docs._timeindexer"/>
    public class TimeIndexer
    {
        private readonly SynthWishes _synthWishes;

        /// <inheritdoc cref="docs._timeindexer"/>
        internal TimeIndexer(SynthWishes synthWishes) 
            => _synthWishes = synthWishes;
        
        /// <inheritdoc cref="docs._timeindexer"/>
        public FlowNode this[double bar, double beat] 
            => (bar - 1) * _synthWishes.GetBarLength + (beat - 1) * _synthWishes.GetBeatLength;
        
        /// <inheritdoc cref="docs._timeindexer"/>
        public FlowNode this[FlowNode bar, FlowNode beat] 
            => (bar - 1) * _synthWishes.GetBarLength + (beat - 1) * _synthWishes.GetBeatLength;
    }
}