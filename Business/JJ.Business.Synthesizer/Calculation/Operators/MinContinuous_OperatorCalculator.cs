using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class MinContinuous_OperatorCalculator : MinOrMaxContinuous_OperatorCalculatorBase
    {
        public MinContinuous_OperatorCalculator(
            OperatorCalculatorBase signalCalculator, 
            OperatorCalculatorBase fromCalculator, 
            OperatorCalculatorBase tillCalculator, 
            OperatorCalculatorBase sampleCalculator, 
            DimensionStack dimensionStack) 
            : base(signalCalculator, fromCalculator, tillCalculator, sampleCalculator, dimensionStack)
        { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override bool MustOverwrite(double currentValue, double newValue)
        {
            return newValue < currentValue;
        }
    }
}
