using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class GetDimension_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly int _dimensionEnumInt;
        private readonly DimensionStacks _dimensionStack;

        public GetDimension_OperatorCalculator(
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack)
        {
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _dimensionEnumInt = (int)dimensionEnum;
            _dimensionStack = dimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            return _dimensionStack.Get(_dimensionEnumInt);
        }
    }
}
