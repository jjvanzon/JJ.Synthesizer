using System;

// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming

namespace JJ.Business.Synthesizer.Wishes
{
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
    
    // SynthWishes Notes
    
    public partial class SynthWishes
    {
        public FluentOutlet SemiToneFactor => _[Notes.SemiToneFactor];

        public FluentOutlet A_Minus1 => _[Notes.A_Minus1];
        public FluentOutlet A_Minus1_Sharp => _[Notes.A_Minus1_Sharp];
        public FluentOutlet B_Minus1 => _[Notes.B_Minus1];
        public FluentOutlet C0  => _[Notes.C0 ];
        public FluentOutlet Cs0 => _[Notes.Cs0];
        public FluentOutlet D0  => _[Notes.D0 ];
        public FluentOutlet Ds0 => _[Notes.Ds0];
        public FluentOutlet E0  => _[Notes.E0 ];
        public FluentOutlet F0  => _[Notes.F0 ];
        public FluentOutlet Fs0 => _[Notes.Fs0];
        public FluentOutlet G0  => _[Notes.G0 ];
        public FluentOutlet Gs0 => _[Notes.Gs0];
        public FluentOutlet A0  => _[Notes.A0 ];
        public FluentOutlet As0 => _[Notes.As0];
        public FluentOutlet B0  => _[Notes.B0 ];
        public FluentOutlet C1  => _[Notes.C1 ];
        public FluentOutlet Cs1 => _[Notes.Cs1];
        public FluentOutlet D1  => _[Notes.D1 ];
        public FluentOutlet Ds1 => _[Notes.Ds1];
        public FluentOutlet E1  => _[Notes.E1 ];
        public FluentOutlet F1  => _[Notes.F1 ];
        public FluentOutlet Fs1 => _[Notes.Fs1];
        public FluentOutlet G1  => _[Notes.G1 ];
        public FluentOutlet Gs1 => _[Notes.Gs1];
        public FluentOutlet A1  => _[Notes.A1 ];
        public FluentOutlet As1 => _[Notes.As1];
        public FluentOutlet B1  => _[Notes.B1 ];
        public FluentOutlet C2  => _[Notes.C2 ];
        public FluentOutlet Cs2 => _[Notes.Cs2];
        public FluentOutlet D2  => _[Notes.D2 ];
        public FluentOutlet Ds2 => _[Notes.Ds2];
        public FluentOutlet E2  => _[Notes.E2 ];
        public FluentOutlet F2  => _[Notes.F2 ];
        public FluentOutlet Fs2 => _[Notes.Fs2];
        public FluentOutlet G2  => _[Notes.G2 ];
        public FluentOutlet Gs2 => _[Notes.Gs2];
        public FluentOutlet A2  => _[Notes.A2 ];
        public FluentOutlet As2 => _[Notes.As2];
        public FluentOutlet B2  => _[Notes.B2 ];
        public FluentOutlet C3  => _[Notes.C3 ];
        public FluentOutlet Cs3 => _[Notes.Cs3];
        public FluentOutlet D3  => _[Notes.D3 ];
        public FluentOutlet Ds3 => _[Notes.Ds3];
        public FluentOutlet E3  => _[Notes.E3 ];
        public FluentOutlet F3  => _[Notes.F3 ];
        public FluentOutlet Fs3 => _[Notes.Fs3];
        public FluentOutlet G3  => _[Notes.G3 ];
        public FluentOutlet Gs3 => _[Notes.Gs3];
        public FluentOutlet A3  => _[Notes.A3 ];
        public FluentOutlet As3 => _[Notes.As3];
        public FluentOutlet B3  => _[Notes.B3 ];
        public FluentOutlet C4  => _[Notes.C4 ];
        public FluentOutlet Cs4 => _[Notes.Cs4];
        public FluentOutlet D4  => _[Notes.D4 ];
        public FluentOutlet Ds4 => _[Notes.Ds4];
        public FluentOutlet E4  => _[Notes.E4 ];
        public FluentOutlet F4  => _[Notes.F4 ];
        public FluentOutlet Fs4 => _[Notes.Fs4];
        public FluentOutlet G4  => _[Notes.G4 ];
        public FluentOutlet Gs4 => _[Notes.Gs4];
        public FluentOutlet A4  => _[Notes.A4 ];
        public FluentOutlet As4 => _[Notes.As4];
        public FluentOutlet B4  => _[Notes.B4 ];
        public FluentOutlet C5  => _[Notes.C5 ];
        public FluentOutlet Cs5 => _[Notes.Cs5];
        public FluentOutlet D5  => _[Notes.D5 ];
        public FluentOutlet Ds5 => _[Notes.Ds5];
        public FluentOutlet E5  => _[Notes.E5 ];
        public FluentOutlet F5  => _[Notes.F5 ];
        public FluentOutlet Fs5 => _[Notes.Fs5];
        public FluentOutlet G5  => _[Notes.G5 ];
        public FluentOutlet Gs5 => _[Notes.Gs5];
        public FluentOutlet A5  => _[Notes.A5 ];
        public FluentOutlet As5 => _[Notes.As5];
        public FluentOutlet B5  => _[Notes.B5 ];
        public FluentOutlet C6  => _[Notes.C6 ];
        public FluentOutlet Cs6 => _[Notes.Cs6];
        public FluentOutlet D6  => _[Notes.D6 ];
        public FluentOutlet Ds6 => _[Notes.Ds6];
        public FluentOutlet E6  => _[Notes.E6 ];
        public FluentOutlet F6  => _[Notes.F6 ];
        public FluentOutlet Fs6 => _[Notes.Fs6];
        public FluentOutlet G6  => _[Notes.G6 ];
        public FluentOutlet Gs6 => _[Notes.Gs6];
        public FluentOutlet A6  => _[Notes.A6 ];
        public FluentOutlet As6 => _[Notes.As6];
        public FluentOutlet B6  => _[Notes.B6 ];
        public FluentOutlet C7  => _[Notes.C7 ];
        public FluentOutlet Cs7 => _[Notes.Cs7];
        public FluentOutlet D7  => _[Notes.D7 ];
        public FluentOutlet Ds7 => _[Notes.Ds7];
        public FluentOutlet E7  => _[Notes.E7 ];
        public FluentOutlet F7  => _[Notes.F7 ];
        public FluentOutlet Fs7 => _[Notes.Fs7];
        public FluentOutlet G7  => _[Notes.G7 ];
        public FluentOutlet Gs7 => _[Notes.Gs7];
        public FluentOutlet A7  => _[Notes.A7 ];
        public FluentOutlet As7 => _[Notes.As7];
        public FluentOutlet B7  => _[Notes.B7 ];
        public FluentOutlet C8  => _[Notes.C8 ];
        public FluentOutlet Cs8 => _[Notes.Cs8];
        public FluentOutlet D8  => _[Notes.D8 ];
        public FluentOutlet Ds8 => _[Notes.Ds8];
        public FluentOutlet E8  => _[Notes.E8 ];
        public FluentOutlet F8  => _[Notes.F8 ];
        public FluentOutlet Fs8 => _[Notes.Fs8];
        public FluentOutlet G8  => _[Notes.G8 ];
        public FluentOutlet Gs8 => _[Notes.Gs8];
        public FluentOutlet A8  => _[Notes.A8 ];
        public FluentOutlet As8 => _[Notes.As8];
        public FluentOutlet B8  => _[Notes.B8 ];
        public FluentOutlet C9  => _[Notes.C9 ];
        public FluentOutlet Cs9 => _[Notes.Cs9];
        public FluentOutlet D9  => _[Notes.D9 ];
        public FluentOutlet Ds9 => _[Notes.Ds9];
        public FluentOutlet E9  => _[Notes.E9 ];
        public FluentOutlet F9  => _[Notes.F9 ];
        public FluentOutlet Fs9 => _[Notes.Fs9];
        public FluentOutlet G9  => _[Notes.G9 ];
        public FluentOutlet Gs9 => _[Notes.Gs9];
    }

}