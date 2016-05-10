using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate(DimensionStack dimensionStack)
        {
            double x = _calculatorX.Calculate(dimensionStack);

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