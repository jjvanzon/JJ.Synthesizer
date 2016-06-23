using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class MaxContinuous_OperatorCalculator_RecalculateContinually : MinOrMaxContinuous_OperatorCalculatorBase
    {
        public MaxContinuous_OperatorCalculator_RecalculateContinually(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase fromCalculator,
            OperatorCalculatorBase tillCalculator,
            OperatorCalculatorBase stepCalculator,
            DimensionStack dimensionStack)
            : base(signalCalculator, fromCalculator, tillCalculator, stepCalculator, dimensionStack)
        { }

        public override double Calculate()
        {
            RecalculateAggregate();

            return base.Calculate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override bool MustOverwrite(double currentValue, double newValue)
        {
            return newValue > currentValue;
        }
    }
}
