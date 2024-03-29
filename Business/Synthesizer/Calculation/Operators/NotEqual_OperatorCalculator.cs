﻿using System.Runtime.CompilerServices;

// ReSharper disable once CompareOfFloatsByEqualityOperator
// ReSharper disable once ConvertIfStatementToReturnStatement
// ReSharper disable once RedundantIfElseBlock

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class NotEqual_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _aCalculator;
        private readonly OperatorCalculatorBase _bCalculator;

        public NotEqual_OperatorCalculator(
            OperatorCalculatorBase aCalculator,
            OperatorCalculatorBase bCalculator)
            : base(new[] { aCalculator, bCalculator })
        {
            _aCalculator = aCalculator;
            _bCalculator = bCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double a = _aCalculator.Calculate();
            double b = _bCalculator.Calculate();

            if (a != b)
            {
                return 1.0;
            }
            else
            {
                return 0.0;
            }
        }
    }
}