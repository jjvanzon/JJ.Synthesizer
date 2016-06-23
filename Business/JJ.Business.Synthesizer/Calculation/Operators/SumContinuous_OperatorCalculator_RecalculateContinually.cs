using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SumContinuous_OperatorCalculator_RecalculateContinually : SumContinuous_OperatorCalculator_Base
    {
        public SumContinuous_OperatorCalculator_RecalculateContinually(
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            RecalculateAggregate();

            return _aggregate;
        }
    }
}
