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
    internal abstract class BlockInterpolation_SampleCalculatorBase : SampleCalculatorBase
    {
        public BlockInterpolation_SampleCalculatorBase(Sample sample)
            : base(sample)
        { }

        public override double CalculateValue(int channelIndex, double time)
        {
            if (!_sample.IsActive) return 0;

            double t = time * _rate;
            int t0 = (int)t;

            // Return if sample not in range.
            if (t0 < 0) return 0;
            if (t0 > _samples.Length - 1) return 0;

            double value = _samples[channelIndex, t0];
            return value;
        }
    }
}