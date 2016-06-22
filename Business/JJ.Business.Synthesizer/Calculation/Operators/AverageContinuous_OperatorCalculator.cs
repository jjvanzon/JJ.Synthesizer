using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class AverageContinuous_OperatorCalculator : SumContinuous_OperatorCalculator
    {
        public AverageContinuous_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase fromCalculator,
            OperatorCalculatorBase tillCalculator,
            OperatorCalculatorBase stepCalculator,
            DimensionStack dimensionStack)
            : base(signalCalculator, fromCalculator, tillCalculator, stepCalculator, dimensionStack)
        { }

        protected override void ResetNonRecursive()
        {
            base.ResetNonRecursive();

            double step = _stepCalculator.Calculate();

            _aggregate *= step;
        }
    }
}
