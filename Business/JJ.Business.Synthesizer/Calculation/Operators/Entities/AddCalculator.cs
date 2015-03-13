using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    internal class AddCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _operandACalculator;
        private OperatorCalculatorBase _operandBCalculator;

        public AddCalculator(OperatorCalculatorBase operandACalculator, OperatorCalculatorBase operandBCalculator)
        {
            if (operandACalculator == null) throw new NullException(() => operandACalculator);
            if (operandBCalculator == null) throw new NullException(() => operandBCalculator);

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
