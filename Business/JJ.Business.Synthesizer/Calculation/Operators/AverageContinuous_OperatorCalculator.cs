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
            OperatorCalculatorBase sampleCountCalculator,
            DimensionStack dimensionStack)
            : base(signalCalculator, fromCalculator, tillCalculator, sampleCountCalculator, dimensionStack)
        { }

        protected override void ResetNonRecursive()
        {
            base.ResetNonRecursive();

            double sampleCount = _sampleCountCalculator.Calculate();

            if (sampleCount <= 0)
            {
                return;
            }

            _value /= sampleCount;
        }
    }
}
