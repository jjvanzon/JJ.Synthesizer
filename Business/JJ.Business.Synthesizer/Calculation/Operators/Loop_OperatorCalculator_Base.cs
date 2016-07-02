using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal abstract class Loop_OperatorCalculator_Base : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        protected readonly DimensionStack _dimensionStack;
        protected readonly int _nextDimensionStackIndex;
        protected readonly int _previousDimensionStackIndex;

        protected double _origin;

        public Loop_OperatorCalculator_Base(
            OperatorCalculatorBase signalCalculator,
            DimensionStack dimensionStack,
            IList<OperatorCalculatorBase> childOperatorCalculators)
            : base(childOperatorCalculators)
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

            _signalCalculator = signalCalculator;
            _dimensionStack = dimensionStack;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex;
            _nextDimensionStackIndex = dimensionStack.CurrentIndex + 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract double? GetTransformedPosition();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double? transformedPosition = GetTransformedPosition();
            if (!transformedPosition.HasValue)
            {
                return 0;
            }

#if !USE_INVAR_INDICES
            _dimensionStack.Push(transformedPosition.Value);
#else
            _dimensionStack.Set(_nextDimensionStackIndex, transformedPosition.Value);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
#endif

            double value = _signalCalculator.Calculate();
#if !USE_INVAR_INDICES
            _dimensionStack.Pop();
#endif
            return value;
        }

        public override void Reset()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_previousDimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _previousDimensionStackIndex);
#endif

            // Origin Shifting
            _origin = position;

            // Dimension Transformation
            double? transformedPosition = GetTransformedPosition();
            if (!transformedPosition.HasValue)
            {
                // TODO: There is no meaningful value to fall back to.
                // What to do?
                transformedPosition = position;
            }

#if !USE_INVAR_INDICES
            _dimensionStack.Push(transformedPosition.Value);
#else
            _dimensionStack.Set(_nextDimensionStackIndex, transformedPosition.Value);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
#endif

            base.Reset();

#if !USE_INVAR_INDICES
            _dimensionStack.Pop();
#endif
        }
    }
}