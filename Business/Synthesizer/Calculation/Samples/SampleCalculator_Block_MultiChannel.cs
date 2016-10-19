using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Samples
{
    internal class SampleCalculator_Block_MultiChannel : SampleCalculatorBase
    {
        private ArrayCalculator_MinPositionZero_Block[] _arrayCalculators;

        public SampleCalculator_Block_MultiChannel(Sample sample, byte[] bytes)
            : base(sample, bytes)
        {
            if (sample.GetChannelCount() == 1) throw new EqualException(() => sample.GetChannelCount(), 1);

            double[][] samples = SampleCalculatorHelper.ReadSamples(sample, bytes);

            _arrayCalculators = samples.Select(x => new ArrayCalculator_MinPositionZero_Block(x, _rate)).ToArray();
        }

        public override double CalculateValue(double time, int channelIndex)
        {
            return _arrayCalculators[channelIndex].CalculateValue(time);
        }
    }
}
