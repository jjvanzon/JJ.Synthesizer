using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Absolute_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _calculatorX;

        public Absolute_OperatorCalculator(OperatorCalculatorBase calculatorX)
            : base(new OperatorCalculatorBase[] { calculatorX })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(calculatorX, () => calculatorX);

            _calculatorX = calculatorX;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double x = _calculatorX.Calculate(time, channelIndex);

            if (x >= 0.0)
            {
                return x;
            }
            else
            {
                return -x;
            }
        }
    }
}