using System;
// ReSharper disable UnusedMember.Global

namespace JJ.Business.Synthesizer.Tests.Helpers
{
	internal static class Frequencies
	{
		public static double A4 => 440.0;

		public static double A0 => A4 / 16.0;
		public static double A1 => A4 / 8.0;
		public static double A2 => A4 / 4.0;
		public static double A3 => A4 / 2.0;
		public static double A5 => A4 * 2.0;
		public static double A6 => A4 * 4.0;
		public static double A7 => A4 * 8.0;
		public static double A8 => A4 * 16.0;

		public static double F4_Sharp { get; } = 369.9944227116344;

		public static double A4_Sharp { get; } = A4 * Math.Pow(2.0, 1.0 / 12.0);
		public static double B4       { get; } = A4 * Math.Pow(2.0, 2.0 / 12.0);
		public static double C5       { get; } = A4 * Math.Pow(2.0, 3.0 / 12.0);
		public static double C5_Sharp { get; } = A4 * Math.Pow(2.0, 4.0 / 12.0);
		public static double D5       { get; } = A4 * Math.Pow(2.0, 5.0 / 12.0);
		public static double D5_Sharp { get; } = A4 * Math.Pow(2.0, 6.0 / 12.0);
		public static double E5       { get; } = A4 * Math.Pow(2.0, 7.0 / 12.0);
		public static double F5       { get; } = A4 * Math.Pow(2.0, 8.0 / 12.0);
		public static double F5_Sharp { get; } = A4 * Math.Pow(2.0, 9.0 / 12.0);
		public static double G5       { get; } = A4 * Math.Pow(2.0, 10.0 / 12.0);
		public static double G5_Sharp { get; } = A4 * Math.Pow(2.0, 11.0 / 12.0);
	}
}
