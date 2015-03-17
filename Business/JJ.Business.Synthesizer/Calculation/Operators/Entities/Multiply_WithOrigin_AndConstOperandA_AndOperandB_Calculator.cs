using JJ.Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    internal class Multiply_WithOrigin_AndConstOperandA_AndOperandB_Calculator : OperatorCalculatorBase
    {
        private double _operandAValue;
        private double _operandBValue;
        private OperatorCalculatorBase _originCalculator;

        public Multiply_WithOrigin_AndConstOperandA_AndOperandB_Calculator(
            double operandAValue, 
            double operandBValue, 
            OperatorCalculatorBase originCalculator)
        {
            if (originCalculator == null) throw new NullException(() => originCalculator);
            if (originCalculator is Value_Calculator) throw new Exception("originCalculator cannot be a Value_Calculator.");

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