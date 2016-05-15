using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class GetDimension_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public GetDimension_OperatorCalculator(DimensionStack dimensionStack)
        {
            OperatorCalculatorHelper.AssertDimensionStack_ForReaders(dimensionStack);

            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            return _dimensionStack.Get(_dimensionStackIndex);
        }
    }
}
