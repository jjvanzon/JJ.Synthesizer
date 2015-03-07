using JJ.Business.Synthesizer.Calculation.Samples;
using JJ.Framework.IO;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.Calculation.Samples
{
    internal abstract class LineInterpolation_SampleCalculatorBase : SampleCalculatorBase
    {
        public LineInterpolation_SampleCalculatorBase(SampleChannel sampleChannel, int bytesPerSample)
            : base(sampleChannel)
        {
            _samples = SampleCalculatorHelper.ReadSamples(sampleChannel, bytesPerSample, ReadValue);
        }

        protected abstract double ReadValue(BinaryReader binaryReader);

        public override double CalculateValue(double time)
        {
            if (!_sample.IsActive) return 0;

            double t = time * _rate;
            int t0 = (int)t;

            // Return if sample not in range.
            if (t0 < 0) return 0;
            if (t0 + 1 > _samples.Length - 1) return 0;

            double x0 = _samples[t0];
            double x1 = _samples[t0 + 1];

            double x = x0 + (x1 - x0) * (t - t0);
            return x;
        }
    }
}