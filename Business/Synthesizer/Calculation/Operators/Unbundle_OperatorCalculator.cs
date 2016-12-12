using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Unbundle_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _operandCalculator;
        private readonly double _position;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public Unbundle_OperatorCalculator(
            OperatorCalculatorBase operandCalculator, 
            double position,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { operandCalculator })
        {
            if (operandCalculator == null) throw new NullException(() => operandCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _operandCalculator = operandCalculator;
            _position = position;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
#if !USE_INVAR_INDICES
            _dimensionStack.Push(_position);
#else
            _dimensionStack.Set(_dimensionStackIndex, _position);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
            double result = _operandCalculator.Calculate();

#if !USE_INVAR_INDICES
            _dimensionStack.Pop();
#endif
            return result;
        }
    }
}
