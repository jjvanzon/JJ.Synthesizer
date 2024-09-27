using System;

// ReSharper disable UnusedMember.Global

namespace JJ.Business.Synthesizer.Tests.Helpers
{
	internal static class Frequencies
	{
		public static double SemiToneFactor { get; } = Math.Pow(2.0, 1.0 / 12.0);

		public const  double A_Minus1 = 13.75;
		public static double A_Minus1_Sharp { get; } = A_Minus1 * Math.Pow(SemiToneFactor, 1.0);
		public static double B_Minus1       { get; } = A_Minus1 * Math.Pow(SemiToneFactor, 2.0);
		public static double C0             { get; } = A_Minus1 * Math.Pow(SemiToneFactor, 3.0);
		public static double C0_Sharp       { get; } = A_Minus1 * Math.Pow(SemiToneFactor, 4.0);
		public static double D0             { get; } = A_Minus1 * Math.Pow(SemiToneFactor, 5.0);
		public static double D0_Sharp       { get; } = A_Minus1 * Math.Pow(SemiToneFactor, 6.0);
		public static double E0             { get; } = A_Minus1 * Math.Pow(SemiToneFactor, 7.0);
		public static double F0             { get; } = A_Minus1 * Math.Pow(SemiToneFactor, 8.0);
		public static double F0_Sharp       { get; } = A_Minus1 * Math.Pow(SemiToneFactor, 9.0);
		public static double G0             { get; } = A_Minus1 * Math.Pow(SemiToneFactor, 10.0);
		public static double G0_Sharp       { get; } = A_Minus1 * Math.Pow(SemiToneFactor, 11.0);

		public const  double A0 = 27.5;
		public static double A0_Sharp { get; } = A0 * Math.Pow(SemiToneFactor, 1.0);
		public static double B0       { get; } = A0 * Math.Pow(SemiToneFactor, 2.0);
		public static double C1       { get; } = A0 * Math.Pow(SemiToneFactor, 3.0);
		public static double C1_Sharp { get; } = A0 * Math.Pow(SemiToneFactor, 4.0);
		public static double D1       { get; } = A0 * Math.Pow(SemiToneFactor, 5.0);
		public static double D1_Sharp { get; } = A0 * Math.Pow(SemiToneFactor, 6.0);
		public static double E1       { get; } = A0 * Math.Pow(SemiToneFactor, 7.0);
		public static double F1       { get; } = A0 * Math.Pow(SemiToneFactor, 8.0);
		public static double F1_Sharp { get; } = A0 * Math.Pow(SemiToneFactor, 9.0);
		public static double G1       { get; } = A0 * Math.Pow(SemiToneFactor, 10.0);
		public static double G1_Sharp { get; } = A0 * Math.Pow(SemiToneFactor, 11.0);

		public const  double A1 = 55;
		public static double A1_Sharp { get; } = A1 * Math.Pow(SemiToneFactor, 1.0);
		public static double B1       { get; } = A1 * Math.Pow(SemiToneFactor, 2.0);
		public static double C2       { get; } = A1 * Math.Pow(SemiToneFactor, 3.0);
		public static double C2_Sharp { get; } = A1 * Math.Pow(SemiToneFactor, 4.0);
		public static double D2       { get; } = A1 * Math.Pow(SemiToneFactor, 5.0);
		public static double D2_Sharp { get; } = A1 * Math.Pow(SemiToneFactor, 6.0);
		public static double E2       { get; } = A1 * Math.Pow(SemiToneFactor, 7.0);
		public static double F2       { get; } = A1 * Math.Pow(SemiToneFactor, 8.0);
		public static double F2_Sharp { get; } = A1 * Math.Pow(SemiToneFactor, 9.0);
		public static double G2       { get; } = A1 * Math.Pow(SemiToneFactor, 10.0);
		public static double G2_Sharp { get; } = A1 * Math.Pow(SemiToneFactor, 11.0);

		public const  double A2 = 110;
		public static double A2_Sharp { get; } = A2 * Math.Pow(SemiToneFactor, 1.0);
		public static double B2       { get; } = A2 * Math.Pow(SemiToneFactor, 2.0);
		public static double C3       { get; } = A2 * Math.Pow(SemiToneFactor, 3.0);
		public static double C3_Sharp { get; } = A2 * Math.Pow(SemiToneFactor, 4.0);
		public static double D3       { get; } = A2 * Math.Pow(SemiToneFactor, 5.0);
		public static double D3_Sharp { get; } = A2 * Math.Pow(SemiToneFactor, 6.0);
		public static double E3       { get; } = A2 * Math.Pow(SemiToneFactor, 7.0);
		public static double F3       { get; } = A2 * Math.Pow(SemiToneFactor, 8.0);
		public static double F3_Sharp { get; } = A2 * Math.Pow(SemiToneFactor, 9.0);
		public static double G3       { get; } = A2 * Math.Pow(SemiToneFactor, 10.0);
		public static double G3_Sharp { get; } = A2 * Math.Pow(SemiToneFactor, 11.0);

		public const  double A3 = 220;
		public static double A3_Sharp { get; } = A3 * Math.Pow(SemiToneFactor, 1.0);
		public static double B3       { get; } = A3 * Math.Pow(SemiToneFactor, 2.0);
		public static double C4       { get; } = A3 * Math.Pow(SemiToneFactor, 3.0);
		public static double C4_Sharp { get; } = A3 * Math.Pow(SemiToneFactor, 4.0);
		public static double D4       { get; } = A3 * Math.Pow(SemiToneFactor, 5.0);
		public static double D4_Sharp { get; } = A3 * Math.Pow(SemiToneFactor, 6.0);
		public static double E4       { get; } = A3 * Math.Pow(SemiToneFactor, 7.0);
		public static double F4       { get; } = A3 * Math.Pow(SemiToneFactor, 8.0);
		public static double F4_Sharp { get; } = A3 * Math.Pow(SemiToneFactor, 9.0);
		public static double G4       { get; } = A3 * Math.Pow(SemiToneFactor, 10.0);
		public static double G4_Sharp { get; } = A3 * Math.Pow(SemiToneFactor, 11.0);

		public const  double A4 = 440;
		public static double A4_Sharp { get; } = A4 * Math.Pow(SemiToneFactor, 1.0);
		public static double B4       { get; } = A4 * Math.Pow(SemiToneFactor, 2.0);
		public static double C5       { get; } = A4 * Math.Pow(SemiToneFactor, 3.0);
		public static double C5_Sharp { get; } = A4 * Math.Pow(SemiToneFactor, 4.0);
		public static double D5       { get; } = A4 * Math.Pow(SemiToneFactor, 5.0);
		public static double D5_Sharp { get; } = A4 * Math.Pow(SemiToneFactor, 6.0);
		public static double E5       { get; } = A4 * Math.Pow(SemiToneFactor, 7.0);
		public static double F5       { get; } = A4 * Math.Pow(SemiToneFactor, 8.0);
		public static double F5_Sharp { get; } = A4 * Math.Pow(SemiToneFactor, 9.0);
		public static double G5       { get; } = A4 * Math.Pow(SemiToneFactor, 10.0);
		public static double G5_Sharp { get; } = A4 * Math.Pow(SemiToneFactor, 11.0);

		public const  double A5 = 880;
		public static double A5_Sharp { get; } = A5 * Math.Pow(SemiToneFactor, 1.0);
		public static double B5       { get; } = A5 * Math.Pow(SemiToneFactor, 2.0);
		public static double C6       { get; } = A5 * Math.Pow(SemiToneFactor, 3.0);
		public static double C6_Sharp { get; } = A5 * Math.Pow(SemiToneFactor, 4.0);
		public static double D6       { get; } = A5 * Math.Pow(SemiToneFactor, 5.0);
		public static double D6_Sharp { get; } = A5 * Math.Pow(SemiToneFactor, 6.0);
		public static double E6       { get; } = A5 * Math.Pow(SemiToneFactor, 7.0);
		public static double F6       { get; } = A5 * Math.Pow(SemiToneFactor, 8.0);
		public static double F6_Sharp { get; } = A5 * Math.Pow(SemiToneFactor, 9.0);
		public static double G6       { get; } = A5 * Math.Pow(SemiToneFactor, 10.0);
		public static double G6_Sharp { get; } = A5 * Math.Pow(SemiToneFactor, 11.0);

		public const  double A6 = 1760;
		public static double A6_Sharp { get; } = A6 * Math.Pow(SemiToneFactor, 1.0);
		public static double B6       { get; } = A6 * Math.Pow(SemiToneFactor, 2.0);
		public static double C7       { get; } = A6 * Math.Pow(SemiToneFactor, 3.0);
		public static double C7_Sharp { get; } = A6 * Math.Pow(SemiToneFactor, 4.0);
		public static double D7       { get; } = A6 * Math.Pow(SemiToneFactor, 5.0);
		public static double D7_Sharp { get; } = A6 * Math.Pow(SemiToneFactor, 6.0);
		public static double E7       { get; } = A6 * Math.Pow(SemiToneFactor, 7.0);
		public static double F7       { get; } = A6 * Math.Pow(SemiToneFactor, 8.0);
		public static double F7_Sharp { get; } = A6 * Math.Pow(SemiToneFactor, 9.0);
		public static double G7       { get; } = A6 * Math.Pow(SemiToneFactor, 10.0);
		public static double G7_Sharp { get; } = A6 * Math.Pow(SemiToneFactor, 11.0);

		public const  double A7 = 3520;
		public static double A7_Sharp { get; } = A7 * Math.Pow(SemiToneFactor, 1.0);
		public static double B7       { get; } = A7 * Math.Pow(SemiToneFactor, 2.0);
		public static double C8       { get; } = A7 * Math.Pow(SemiToneFactor, 3.0);
		public static double C8_Sharp { get; } = A7 * Math.Pow(SemiToneFactor, 4.0);
		public static double D8       { get; } = A7 * Math.Pow(SemiToneFactor, 5.0);
		public static double D8_Sharp { get; } = A7 * Math.Pow(SemiToneFactor, 6.0);
		public static double E8       { get; } = A7 * Math.Pow(SemiToneFactor, 7.0);
		public static double F8       { get; } = A7 * Math.Pow(SemiToneFactor, 8.0);
		public static double F8_Sharp { get; } = A7 * Math.Pow(SemiToneFactor, 9.0);
		public static double G8       { get; } = A7 * Math.Pow(SemiToneFactor, 10.0);
		public static double G8_Sharp { get; } = A7 * Math.Pow(SemiToneFactor, 11.0);

		public const  double A8 = 7040;
		public static double A8_Sharp { get; } = A8 * Math.Pow(SemiToneFactor, 1.0);
		public static double B8       { get; } = A8 * Math.Pow(SemiToneFactor, 2.0);
		public static double C9       { get; } = A8 * Math.Pow(SemiToneFactor, 3.0);
		public static double C9_Sharp { get; } = A8 * Math.Pow(SemiToneFactor, 4.0);
		public static double D9       { get; } = A8 * Math.Pow(SemiToneFactor, 5.0);
		public static double D9_Sharp { get; } = A8 * Math.Pow(SemiToneFactor, 6.0);
		public static double E9       { get; } = A8 * Math.Pow(SemiToneFactor, 7.0);
		public static double F9       { get; } = A8 * Math.Pow(SemiToneFactor, 8.0);
		public static double F9_Sharp { get; } = A8 * Math.Pow(SemiToneFactor, 9.0);
		public static double G9       { get; } = A8 * Math.Pow(SemiToneFactor, 10.0);
		public static double G9_Sharp { get; } = A8 * Math.Pow(SemiToneFactor, 11.0);

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