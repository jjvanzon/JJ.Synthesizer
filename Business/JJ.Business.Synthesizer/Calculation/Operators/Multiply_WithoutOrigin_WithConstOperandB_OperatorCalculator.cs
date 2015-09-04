using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Multiply_WithoutOrigin_WithConstOperandB_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _operandACalculator;
        private double _operandBValue;

        public Multiply_WithoutOrigin_WithConstOperandB_OperatorCalculator(OperatorCalculatorBase operandACalculator, double operandBValue)
        {
            if (operandACalculator == null) throw new NullException(() => operandACalculator);
            if (operandACalculator is Value_OperatorCalculator) throw new Exception("operandACalculator cannot be a Value_OperatorCalculator.");

            _operandACalculator = operandACalculator;
            _operandBValue = operandBValue;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double a = _operandACalculator.Calculate(time, channelIndex);
            return a * _operandBValue;
        }
    }
}
