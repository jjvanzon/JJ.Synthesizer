using JJ.Business.Synthesizer.Calculation.Samples;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Sample_OperatorCalculator : OperatorCalculatorBase
    {
        private ISampleCalculator _sampleCalculator;

        /// <param name="bytes">nullable</param>
        public Sample_OperatorCalculator(Sample sample, byte[] bytes)
        {
            _sampleCalculator = SampleCalculatorFactory.CreateSampleCalculator(sample, bytes);
        }

        public override double Calculate(double time, int channelIndex)
        {
            return _sampleCalculator.CalculateValue(time, channelIndex);
        }
    }
}
