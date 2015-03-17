using JJ.Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    internal class Multiply_WithConstOrigin_Calculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _operandACalculator;
        private OperatorCalculatorBase _operandBCalculator;
        private double _originValue;

        public Multiply_WithConstOrigin_Calculator(
            OperatorCalculatorBase operandACalculator, 
            OperatorCalculatorBase operandBCalculator, 
            double originValue)
        {
            if (operandACalculator == null) throw new NullException(() => operandACalculator);
            if (operandACalculator is Value_Calculator) throw new Exception("operandACalculator cannot be a Value_Calculator.");
            if (operandBCalculator == null) throw new NullException(() => operandBCalculator);
            if (operandBCalculator is Value_Calculator) throw new Exception("operandBCalculator cannot be a Value_Calculator.");

            _operandACalculator = operandACalculator;
            _operandBCalculator = operandBCalculator;
            _originValue = originValue;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double a = _operandACalculator.Calculate(time, channelIndex);
            double b = _operandBCalculator.Calculate(time, channelIndex);
            return (a - _originValue) * b + _originValue;
        }
    }
}