using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Not_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _calculatorX;

        public Not_OperatorCalculator(OperatorCalculatorBase calculatorX)
            : base(new OperatorCalculatorBase[] { calculatorX })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(calculatorX, () => calculatorX);

            _calculatorX = calculatorX;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double x = _calculatorX.Calculate(time, channelIndex);

            bool xIsFalse = x == 0.0;

            if (xIsFalse) return 1.0;
            else return 0.0;
        }
    }
}