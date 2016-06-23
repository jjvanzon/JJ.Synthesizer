using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SumContinuous_OperatorCalculator_RecalculateUponReset : SumContinuous_OperatorCalculator_Base
    {
        public SumContinuous_OperatorCalculator_RecalculateUponReset(
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
