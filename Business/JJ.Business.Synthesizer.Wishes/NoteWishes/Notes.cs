using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.Wishes
{
    public static class Notes
    {
        public static double SemiToneFactor { get; } = Math.Pow(2.0, 1.0 / 12.0);
        
        public const  double A_Minus1 = 13.75;
        public static double A_Minus1_Sharp { get; } = A_Minus1 * Math.Pow(SemiToneFactor, 1.0);
        public static double B_Minus1       { get; } = A_Minus1 * Math.Pow(SemiToneFactor, 2.0);
        public static double C0             { get; } = A_Minus1 * Math.Pow(SemiToneFactor, 3.0);
        public static double Cs0            { get; } = A_Minus1 * Math.Pow(SemiToneFactor, 4.0);
        public static double D0             { get; } = A_Minus1 * Math.Pow(SemiToneFactor, 5.0);
        public static double Ds0            { get; } = A_Minus1 * Math.Pow(SemiToneFactor, 6.0);
        public static double E0             { get; } = A_Minus1 * Math.Pow(SemiToneFactor, 7.0);
        public static double F0             { get; } = A_Minus1 * Math.Pow(SemiToneFactor, 8.0);
        public static double Fs0            { get; } = A_Minus1 * Math.Pow(SemiToneFactor, 9.0);
        public static double G0             { get; } = A_Minus1 * Math.Pow(SemiToneFactor, 10.0);
        public static double Gs0            { get; } = A_Minus1 * Math.Pow(SemiToneFactor, 11.0);
        
        public const  double A0 = 27.5;
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
        
        public const  double A1 = 55;
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
        
        public const  double A2 = 110;
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
        
        public const  double A3 = 220;
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
        
        public const  double A4 = 440;
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
        
        public const  double A5 = 880;
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
        
        public const  double A6 = 1760;
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
        
        public const  double A7 = 3520;
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
        
        public const  double A8 = 7040;
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
}