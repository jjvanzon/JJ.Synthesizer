using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class AverageOverDimension_OperatorCalculator_RecalculateUponReset 
        : SumOverDimension_OperatorCalculator_RecalculateUponReset
    {
        public AverageOverDimension_OperatorCalculator_RecalculateUponReset(
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
