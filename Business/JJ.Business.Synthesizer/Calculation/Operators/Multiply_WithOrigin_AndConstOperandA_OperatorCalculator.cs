using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Multiply_WithOrigin_AndConstOperandA_OperatorCalculator : OperatorCalculatorBase
    {
        private double _operandAValue;
        private OperatorCalculatorBase _operandBCalculator;
        private OperatorCalculatorBase _originCalculator;

        public Multiply_WithOrigin_AndConstOperandA_OperatorCalculator(
            double operandAValue, 
            OperatorCalculatorBase operandBCalculator, 
            OperatorCalculatorBase originCalculator)
        {
            if (operandBCalculator == null) throw new NullException(() => operandBCalculator);
            if (operandBCalculator is Value_OperatorCalculator) throw new Exception("operandBCalculator cannot be a Value_OperatorCalculator.");
            if (originCalculator == null) throw new NullException(() => originCalculator);
            if (originCalculator is Value_OperatorCalculator) throw new Exception("originCalculator cannot be a Value_OperatorCalculator.");

            _operandAValue = operandAValue;
            _operandBCalculator = operandBCalculator;
            _originCalculator = originCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double origin = _originCalculator.Calculate(time, channelIndex);
            double b = _operandBCalculator.Calculate(time, channelIndex);
            return (_operandAValue - origin) * b + origin;
        }
    }
}