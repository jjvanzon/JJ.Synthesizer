using JJ.Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    internal class Multiply_WithConstOrigin_AndOperandA_Calculator : OperatorCalculatorBase
    {
        private double _operandAValue;
        private OperatorCalculatorBase _operandBCalculator;
        private double _originValue;

        public Multiply_WithConstOrigin_AndOperandA_Calculator(
            double operandAValue, 
            OperatorCalculatorBase operandBCalculator, 
            double originValue)
        {
            if (operandBCalculator == null) throw new NullException(() => operandBCalculator);

            _operandAValue = operandAValue;
            _operandBCalculator = operandBCalculator;
            _originValue = originValue;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double b = _operandBCalculator.Calculate(time, channelIndex);
            return (_operandAValue - _originValue) * b + _originValue;
        }
    }
}