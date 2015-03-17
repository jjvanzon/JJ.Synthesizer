using JJ.Business.Synthesizer.Calculation.Samples;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    internal class SampleOperator_Calculator : OperatorCalculatorBase
    {
        private ISampleCalculator _sampleCalculator;

        public SampleOperator_Calculator(Sample sample)
        {
            _sampleCalculator = SampleCalculatorFactory.CreateSampleCalculator(sample);
        }

        public override double Calculate(double time, int channelIndex)
        {
            return _sampleCalculator.CalculateValue(time, channelIndex);
        }
    }
}
