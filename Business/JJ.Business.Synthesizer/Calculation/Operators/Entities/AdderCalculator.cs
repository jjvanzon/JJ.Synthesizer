using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Reflection;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    internal class AdderCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase[] _operandCalculators;

        public AdderCalculator(OperatorCalculatorBase[] operandCalculators)
        {
            if (operandCalculators == null) throw new NullException(() => operandCalculators);

            _operandCalculators = operandCalculators;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double result = 0;

            for (int i = 0; i < _operandCalculators.Length; i++)
            {
                result += _operandCalculators[i].Calculate(time, channelIndex);
            }

            return result;
        }
    }
}
