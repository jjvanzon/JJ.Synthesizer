using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class AverageOverDimension_OperatorCalculator_RecalculateContinuously 
        : SumOverDimension_OperatorCalculator_RecalculateContinuously
    {
        public AverageOverDimension_OperatorCalculator_RecalculateContinuously(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase fromCalculator,
            OperatorCalculatorBase tillCalculator,
            OperatorCalculatorBase stepCalculator,
            DimensionStack dimensionStack)
            : base(signalCalculator, fromCalculator, tillCalculator, stepCalculator, dimensionStack)
        { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void RecalculateAggregate()
        {
            base.RecalculateAggregate();

            _aggregate *= _step;
        }
    }
}
