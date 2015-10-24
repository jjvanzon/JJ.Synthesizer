using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Add_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _operandACalculator;
        private OperatorCalculatorBase _operandBCalculator;

        public Add_OperatorCalculator(OperatorCalculatorBase operandACalculator, OperatorCalculatorBase operandBCalculator)
        {
            if (operandACalculator == null) throw new NullException(() => operandACalculator);
            if (operandBCalculator == null) throw new NullException(() => operandBCalculator);
            if (operandACalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => operandACalculator);
            if (operandBCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => operandBCalculator);

            _operandACalculator = operandACalculator;
            _operandBCalculator = operandBCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double a = _operandACalculator.Calculate(time, channelIndex);
            double b = _operandBCalculator.Calculate(time, channelIndex);
            return a + b;
        }
    }
}
