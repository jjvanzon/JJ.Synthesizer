﻿using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Absolute_OperatorCalculator_VarX : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _xCalculator;

        public Absolute_OperatorCalculator_VarX(OperatorCalculatorBase xCalculator)
            : base(new OperatorCalculatorBase[] { xCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(xCalculator, () => xCalculator);

            _xCalculator = xCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double x = _xCalculator.Calculate();

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