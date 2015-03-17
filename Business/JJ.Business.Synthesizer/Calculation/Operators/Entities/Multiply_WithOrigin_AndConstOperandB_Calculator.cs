using JJ.Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    internal class Multiply_WithOrigin_AndConstOperandB_Calculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _operandACalculator;
        private double _operandBValue;
        private OperatorCalculatorBase _originCalculator;

        public Multiply_WithOrigin_AndConstOperandB_Calculator(
            OperatorCalculatorBase operandACalculator, 
            double operandBValue, 
            OperatorCalculatorBase originCalculator)
        {
            if (operandACalculator == null) throw new NullException(() => operandACalculator);
            if (operandACalculator is Value_Calculator) throw new Exception("operandACalculator cannot be a Value_Calculator.");
            if (originCalculator == null) throw new NullException(() => originCalculator);
            if (originCalculator is Value_Calculator) throw new Exception("originCalculator cannot be a Value_Calculator.");

            _operandACalculator = operandACalculator;
            _operandBValue = operandBValue;
            _originCalculator = originCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double origin = _originCalculator.Calculate(time, channelIndex);
            double a = _operandACalculator.Calculate(time, channelIndex);
            return (a - origin) * _operandBValue + origin;
        }
    }
}