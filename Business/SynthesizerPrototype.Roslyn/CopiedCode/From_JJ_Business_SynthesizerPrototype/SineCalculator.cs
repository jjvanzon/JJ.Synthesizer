using System;
using System.Runtime.CompilerServices;

namespace JJ.Business.SynthesizerPrototype.Roslyn.CopiedCode.From_JJ_Business_SynthesizerPrototype
{
	/// <summary>
	/// +/- 20% faster than Math.Sin.
	/// It also prevents multiplication by 2 PI
	/// by having a cycle be from 0 to 1.
	/// </summary>
	internal static class SineCalculator
	{
		private const double TWO_PI = 6.2831853071795865;
		private const int SAMPLES_PER_CYCLE_INT = 44100 / 8; // 100% precision at 8Hz.
		private const double SAMPLES_PER_CYCLE_DOUBLE = SAMPLES_PER_CYCLE_INT;
		private static readonly double[] _samples = CreateSamples();

		private static double[] CreateSamples()
		{
			var samples = new double[SAMPLES_PER_CYCLE_INT];

			double t = 0;
			const double step = TWO_PI / SAMPLES_PER_CYCLE_INT;

			for (var i = 0; i < SAMPLES_PER_CYCLE_INT; i++)
			{
				double sample = Math.Sin(t);
				samples[i] = sample;

				t += step;
			}

			return samples;
		}

		/// <param name="positionInCycle">From 0 to 1 is one cycle.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Sin(double positionInCycle)
		{
			var i = (int)(positionInCycle * SAMPLES_PER_CYCLE_DOUBLE);

			i = i % SAMPLES_PER_CYCLE_INT;

			if (i < 0)
			{
				i = i + SAMPLES_PER_CYCLE_INT;
			}

			double value = _samples[i];
			return value;
		}
	}
}