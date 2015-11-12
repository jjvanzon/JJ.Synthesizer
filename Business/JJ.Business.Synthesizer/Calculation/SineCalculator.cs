using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Mathematics;

namespace JJ.Business.Synthesizer.Calculation
{
    /// <summary>
    /// +/- 20% faster than Math.Sin.
    /// </summary>
    internal static class SineCalculator
    {
        private const int SAMPLES_PER_CYCLE = 44100 / 8; // 100% precision at 8Hz.
        private const double SAMPLES_PER_RADIAN = SAMPLES_PER_CYCLE / Maths.TWO_PI;
        private static readonly double[] _samples = CreateSamples();

        private static double[] CreateSamples()
        {
            var samples = new double[SAMPLES_PER_CYCLE];

            double t = 0;
            double step = Maths.TWO_PI / SAMPLES_PER_CYCLE;
            for (int i = 0; i < SAMPLES_PER_CYCLE; i++)
            {
                double sample = Math.Sin(t);
                samples[i] = sample;

                t += step;
            }

            return samples;
        }

        public static double Sin(double angleInRadians)
        {
            int i = (int)(angleInRadians * SAMPLES_PER_RADIAN);

            i = i % SAMPLES_PER_CYCLE;

            if (i < 0)
            {
                i = i + SAMPLES_PER_CYCLE;
            }

            double x = _samples[i];
            return x;
        }
    }
}