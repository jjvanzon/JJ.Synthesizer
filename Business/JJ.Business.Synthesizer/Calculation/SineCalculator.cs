using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Mathematics;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation
{
    internal class SineCalculator
    {
        private readonly int _samplesPerCycle;
        private readonly double _samplesPerRadian;
        private readonly double[] _samples;

        public SineCalculator()
            : this(44100 / 8) // enough for 100% precision at 8Hz.
        { }

        private SineCalculator(int samplesPerCycle)
        {
            if (samplesPerCycle < 1) throw new LessThanException(() => samplesPerCycle, 1);

            _samplesPerCycle = samplesPerCycle;

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

        // Less float operations: +/- 20% faster.
        public double Sin(double angleInRadians)
        {
            int i = (int)(angleInRadians * _samplesPerRadian);

            i = i % _samplesPerCycle;

            if (i < 0)
            {
                i = _samplesPerCycle - i;
            }

            double x = _samples[i];
            return x;
        }
    }
}