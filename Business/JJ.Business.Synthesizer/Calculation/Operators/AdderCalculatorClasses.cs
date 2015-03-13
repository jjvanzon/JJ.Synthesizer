using JJ.Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class AdderCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase[] _operandCalculators;

        public AdderCalculator(OperatorCalculatorBase[] operandCalculators)
        {
            if (operandCalculators == null) throw new NullException(() => operandCalculators);

            _operandCalculators = operandCalculators;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double result = 0;

            for (int i = 0; i < _operandCalculators.Length; i++)
            {
                result += _operandCalculators[i].Calculate(time, channelIndex);
            }

            return result;
        }
    }

    internal class AdderCalculator3 : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _operandCalculator1;
        private OperatorCalculatorBase _operandCalculator2;
        private OperatorCalculatorBase _operandCalculator3;

        public AdderCalculator3(
            OperatorCalculatorBase operandCalculator1,
            OperatorCalculatorBase operandCalculator2,
            OperatorCalculatorBase operandCalculator3)
        {
            if (operandCalculator1 == null) throw new NullException(() => operandCalculator1);
            if (operandCalculator2 == null) throw new NullException(() => operandCalculator2);
            if (operandCalculator3 == null) throw new NullException(() => operandCalculator3);

            _operandCalculator1 = operandCalculator1;
            _operandCalculator2 = operandCalculator2;
            _operandCalculator3 = operandCalculator3;
        }

        public override double Calculate(double time, int channelIndex)
        {
            return _operandCalculator1.Calculate(time, channelIndex) +
                   _operandCalculator2.Calculate(time, channelIndex) +
                   _operandCalculator3.Calculate(time, channelIndex);
        }
    }

}
