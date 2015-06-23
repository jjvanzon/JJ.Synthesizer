using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
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
            if (operandBCalculator is Value_OperatorCalculator) throw new Exception("operandBCalculator cannot be a Value_Calculator.");
            if (originCalculator == null) throw new NullException(() => originCalculator);
            if (originCalculator is Value_OperatorCalculator) throw new Exception("originCalculator cannot be a Value_Calculator.");

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