using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Bundle_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase[] _operands;
        private readonly double _operandCountDouble;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;
        
        public Bundle_OperatorCalculator(DimensionStack dimensionStack, IList<OperatorCalculatorBase> operands)
            : base(operands)
        {
            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
            _operands = operands.ToArray();
            _operandCountDouble = operands.Count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.PopAndGet();
#else
            double position = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
            double result;

            if (ConversionHelper.CanCastToNonNegativeInt32WithMax(position, _operandCountDouble))
            {
                int positionInt = (int)position;

                OperatorCalculatorBase operand = _operands[positionInt];

                result = operand.Calculate();
            }
            else
            {
                result = 0.0;
            }

#if !USE_INVAR_INDICES
            _dimensionStack.Push(position);
#endif
            return result;
        }
    }
}
