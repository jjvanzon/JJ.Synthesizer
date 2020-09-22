using System;
using System.Runtime.CompilerServices;
using JJ.Framework.Mathematics;

namespace JJ.Business.Synthesizer.Calculation
{
	/// <summary>
	/// +/- 20% faster than Math.Sin.
	/// It also prevents multiplication by 2 PI
	/// by having a cycle be from 0 to 1.
	/// </summary>
	internal static class SineCalculator
	{
		private const int SAMPLES_PER_CYCLE = 44100 / 8; // 100% precision at 8Hz.
		private static readonly double[] _samples = CreateSamples();

		private static double[] CreateSamples()
		{
			var samples = new double[SAMPLES_PER_CYCLE];

			double t = 0;
			const double step = MathHelper.TWO_PI / SAMPLES_PER_CYCLE;

			for (int i = 0; i < SAMPLES_PER_CYCLE; i++)
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
			int i = (int)(positionInCycle * SAMPLES_PER_CYCLE);

			i = i % SAMPLES_PER_CYCLE;

			if (i < 0)
			{
				i = i + SAMPLES_PER_CYCLE;
			}

			double value = _samples[i];
			return value;
		}
	}
}