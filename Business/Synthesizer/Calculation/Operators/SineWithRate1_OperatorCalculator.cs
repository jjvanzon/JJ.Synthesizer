﻿using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SineWithRate1_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _positionCalculator;

        public SineWithRate1_OperatorCalculator(OperatorCalculatorBase positionCalculator)
            : base(new[] { positionCalculator })
            => _positionCalculator = positionCalculator;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _positionCalculator.Calculate();
            double value = SineCalculator.Sin(position);
            return value;
        }
    }
}