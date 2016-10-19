using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class ClosestOverDimension_OperatorCalculator_CollectionRecalculationUponReset : ClosestOverDimension_OperatorCalculator_Base
    {
        public ClosestOverDimension_OperatorCalculator_CollectionRecalculationUponReset(
            OperatorCalculatorBase inputCalculator, 
            OperatorCalculatorBase collectionCalculator, 
            OperatorCalculatorBase fromCalculator, 
            OperatorCalculatorBase tillCalculator, 
            OperatorCalculatorBase stepCalculator, 
            DimensionStack dimensionStack) 
            : base(
                  inputCalculator,
                  collectionCalculator, 
                  fromCalculator, 
                  tillCalculator, 
                  stepCalculator, 
                  dimensionStack)
        { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void ResetNonRecursive()
        {
            RecalculateCollection();
        }
    }
}
