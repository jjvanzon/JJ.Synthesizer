using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Multiply_WithOrigin_AndConstOperandA_AndOperandB_OperatorCalculator : OperatorCalculatorBase
    {
        private double _operandAValue;
        private double _operandBValue;
        private OperatorCalculatorBase _originCalculator;

        public Multiply_WithOrigin_AndConstOperandA_AndOperandB_OperatorCalculator(
            double operandAValue, 
            double operandBValue, 
            OperatorCalculatorBase originCalculator)
        {
            if (originCalculator == null) throw new NullException(() => originCalculator);
            if (originCalculator is Number_OperatorCalculator) throw new Exception("originCalculator cannot be a Value_OperatorCalculator.");

            _operandAValue = operandAValue;
            _operandBValue = operandBValue;
            _originCalculator = originCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double origin = _originCalculator.Calculate(time, channelIndex);
            return (_operandAValue - origin) * _operandBValue + origin;
        }
    }
}