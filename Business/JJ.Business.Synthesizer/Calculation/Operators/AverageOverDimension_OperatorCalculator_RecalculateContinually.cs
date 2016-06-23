using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class AverageOverDimension_OperatorCalculator_RecalculateContinually 
        : SumOverDimension_OperatorCalculator_RecalculateContinually
    {
        public AverageOverDimension_OperatorCalculator_RecalculateContinually(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase fromCalculator,
            OperatorCalculatorBase tillCalculator,
            OperatorCalculatorBase stepCalculator,
            DimensionStack dimensionStack)
            : base(signalCalculator, fromCalculator, tillCalculator, stepCalculator, dimensionStack)
        { }

        protected override void RecalculateAggregate()
        {
            base.RecalculateAggregate();

            double step = _stepCalculator.Calculate();

            _aggregate *= step;
        }
    }
}
