using JJ.Business.Synthesizer.Calculation.Samples;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    internal class Sample_OperatorCalculator : OperatorCalculatorBase
    {
        private ISampleCalculator _sampleCalculator;

        public Sample_OperatorCalculator(Sample sample)
        {
            _sampleCalculator = SampleCalculatorFactory.CreateSampleCalculator(sample);
        }

        public override double Calculate(double time, int channelIndex)
        {
            return _sampleCalculator.CalculateValue(time, channelIndex);
        }
    }
}
