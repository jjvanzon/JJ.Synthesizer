using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    internal class Substract_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _operandACalculator;
        private OperatorCalculatorBase _operandBCalculator;

        public Substract_OperatorCalculator(
            OperatorCalculatorBase operandACalculator, 
            OperatorCalculatorBase operandBCalculator)
        {
            if (operandACalculator == null) throw new NullException(() => operandACalculator);
            if (operandACalculator is Value_OperatorCalculator) throw new Exception("operandACalculator cannot be a Value_Calculator.");
            if (operandBCalculator == null) throw new NullException(() => operandBCalculator);
            if (operandBCalculator is Value_OperatorCalculator) throw new Exception("operandBCalculator cannot be a Value_Calculator.");

            _operandACalculator = operandACalculator;
            _operandBCalculator = operandBCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double a = _operandACalculator.Calculate(time, channelIndex);
            double b = _operandBCalculator.Calculate(time, channelIndex);
            return a - b;
        }
    }
}