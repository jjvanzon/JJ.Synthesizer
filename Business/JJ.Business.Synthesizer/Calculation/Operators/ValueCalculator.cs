using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class ValueCalculator : OperatorCalculatorBase
    {
        private double _value;

        public ValueCalculator(double value)
        {
            _value = value;
        }

        public override double Calculate(double time, int channelIndex)
        {
            return _value;
        }
    }
}
