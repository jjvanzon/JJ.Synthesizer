using System;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Adder_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase[] _operandCalculators;

        public Adder_OperatorCalculator(OperatorCalculatorBase[] operandCalculators)
            : base(operandCalculators)
        {
            if (operandCalculators == null) throw new NullException(() => operandCalculators);

            _operandCalculators = operandCalculators;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double result = 0;

            for (int i = 0; i < _operandCalculators.Length; i++)
            {
                double result2 = _operandCalculators[i].Calculate(time, channelIndex);

                // Strategically prevent NaN in case of addition, or one sound will destroy the others too.
                if (Double.IsNaN(result2)) continue;

                result += result2;
            }

            return result;
        }
    }
}
