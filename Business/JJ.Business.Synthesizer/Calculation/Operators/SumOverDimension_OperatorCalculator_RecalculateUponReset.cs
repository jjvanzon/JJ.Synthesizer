using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SumOverDimension_OperatorCalculator_RecalculateUponReset : SumOverDimension_OperatorCalculator_Base
    {
        public SumOverDimension_OperatorCalculator_RecalculateUponReset(
            OperatorCalculatorBase signalCalculator, 
            OperatorCalculatorBase fromCalculator, 
            OperatorCalculatorBase tillCalculator, 
            OperatorCalculatorBase stepCalculator, 
            DimensionStack dimensionStack) 
            : base(
                  signalCalculator, 
                  fromCalculator, 
                  tillCalculator, 
                  stepCalculator, 
                  dimensionStack)
        { }

        protected override void ResetNonRecursive()
        {
            RecalculateAggregate();
        }
    }
}
