using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Calculation.Samples
{
    internal abstract class LineInterpolation_SampleCalculatorBase : SampleCalculatorBase
    {
        public LineInterpolation_SampleCalculatorBase(Sample sample, byte[] bytes)
            : base(sample, bytes)
        { }

        public override double CalculateValue(double time, int channelIndex)
        {
            if (!_sample.IsActive) return 0;

            double t = time * _rate;
            int t0 = (int)t;

            // Return if sample not in range.
            if (t0 < 0) return 0;
            if (t0 + 1 > _samples.Length - 1) return 0;

            double x0 = _samples[channelIndex, t0];
            double x1 = _samples[channelIndex, t0 + 1];

            double x = x0 + (x1 - x0) * (t - t0);
            return x;
        }
    }
}