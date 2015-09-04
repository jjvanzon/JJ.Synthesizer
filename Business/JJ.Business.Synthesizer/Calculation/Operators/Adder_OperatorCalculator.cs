using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Adder_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase[] _operandCalculators;

        public Adder_OperatorCalculator(OperatorCalculatorBase[] operandCalculators)
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
