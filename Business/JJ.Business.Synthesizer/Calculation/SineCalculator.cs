using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Mathematics;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation
{
    internal class SineCalculator
    {
#if DEBUG
        private readonly int _samplesPerCycle;
#endif
        private readonly double _samplesPerRadian;
        private readonly double[] _samples;

        public SineCalculator(int samplesPerCycle)
        {
            if (samplesPerCycle < 1) throw new LessThanException(() => samplesPerCycle, 1);
#if DEBUG
            _samplesPerCycle = samplesPerCycle;
#endif
            // The calculation requires an extra entry at the end.
            _samples = new double[samplesPerCycle + 1];

            double t = 0;
            double step = Maths.TWO_PI / samplesPerCycle;
            for (int i = 0; i < samplesPerCycle; i++)
            {
                double sineSample = Math.Sin(t);
                _samples[i] = sineSample;

                t += step;
            }

            // The calculation requires an extra entry at the end.
            _samples[samplesPerCycle] = _samples[0];

            _samplesPerRadian = samplesPerCycle / Maths.TWO_PI;
        }

        public double Sin(double angleInRadians)
        {
            angleInRadians = angleInRadians % Maths.TWO_PI;
            if (angleInRadians < 0.0)
            {
                angleInRadians += Maths.TWO_PI;
            }

            double i = angleInRadians * _samplesPerRadian;

            int i0 = (int)i;

            double x0 = _samples[i0];
            double x1 = _samples[i0 + 1];

            double x = x0 + (x1 - x0) * (i - i0);
            return x;
        }
    }
}