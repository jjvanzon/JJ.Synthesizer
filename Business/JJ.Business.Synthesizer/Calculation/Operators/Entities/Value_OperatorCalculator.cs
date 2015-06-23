using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    [DebuggerDisplay("{_value}")]
    internal class Value_OperatorCalculator : OperatorCalculatorBase
    {
        private double _value;

        public Value_OperatorCalculator(double value)
        {
            _value = value;
        }

        public override double Calculate(double time, int channelIndex)
        {
            return _value;
        }
    }
}
