using System.Collections.Generic;
using System.Runtime.CompilerServices;

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
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _signalCalculator = signalCalculator;
            _dimensionStack = dimensionStack;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex;
            _nextDimensionStackIndex = dimensionStack.CurrentIndex + 1;

            ResetNonRecursive();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract double? GetTransformedPosition();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double? transformedPosition = GetTransformedPosition();
            if (!transformedPosition.HasValue)
            {
                return 0.0;
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
            // First reset parent, then children,
            // because unlike some other operators,
            // child state is dependent transformed position,
            // which is dependent on parent state.
            ResetNonRecursive();

            // Dimension Transformation
            double? transformedPosition = GetTransformedPosition();
            if (!transformedPosition.HasValue)
            {
                // TODO: There is no meaningful value to fall back to. What to do? Return instead?
                //transformedPosition = _origin;
                return;
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

        private void ResetNonRecursive()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_previousDimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _previousDimensionStackIndex);
#endif
            _origin = position;
        }
    }
}