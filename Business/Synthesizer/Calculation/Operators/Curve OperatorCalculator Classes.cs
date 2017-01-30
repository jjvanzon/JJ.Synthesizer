using JJ.Framework.Exceptions;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation.Arrays;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Curve_OperatorCalculator_MinX_NoOriginShifting : OperatorCalculatorBase
    {
        private readonly ArrayCalculator_MinPosition_Line _underlyingCalculator;
        private readonly DimensionStack _dimensionStack;
        // ReSharper disable once NotAccessedField.Local
        private readonly int _dimensionStackIndex;

        public Curve_OperatorCalculator_MinX_NoOriginShifting(ArrayCalculator_MinPosition_Line underlyingCalculator, DimensionStack dimensionStack)
        {
            if (underlyingCalculator == null) throw new NullException(() => underlyingCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _underlyingCalculator = underlyingCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif

            double value = _underlyingCalculator.Calculate(position);

            return value;
        }
    }

    internal class Curve_OperatorCalculator_MinX_WithOriginShifting : OperatorCalculatorBase
    {
        private readonly ArrayCalculator_MinPosition_Line _underlyingCalculator;
        private readonly DimensionStack _dimensionStack;
        // ReSharper disable once NotAccessedField.Local
        private readonly int _dimensionStackIndex;

        private double _origin;

        public Curve_OperatorCalculator_MinX_WithOriginShifting(ArrayCalculator_MinPosition_Line underlyingCalculator, DimensionStack dimensionStack)
        {
            if (underlyingCalculator == null) throw new NullException(() => underlyingCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _underlyingCalculator = underlyingCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;

            ResetPrivate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_dimensionStackIndex);
#endif

#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
            double phase = position - _origin;

            double value = _underlyingCalculator.Calculate(phase);

            return value;
        }

        public override void Reset()
        {
            ResetPrivate();
        }

        private void ResetPrivate()
        {
#if !USE_INVAR_INDICES
            _origin = _dimensionStack.Get();
#else
            _origin = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
        }
    }

    internal class Curve_OperatorCalculator_MinXZero_NoOriginShifting : OperatorCalculatorBase
    {
        private readonly ArrayCalculator_MinPositionZero_Line _underlyingCalculator;
        private readonly DimensionStack _dimensionStack;
        // ReSharper disable once NotAccessedField.Local
        private readonly int _dimensionStackIndex;

        public Curve_OperatorCalculator_MinXZero_NoOriginShifting(ArrayCalculator_MinPositionZero_Line underlyingCalculator, DimensionStack dimensionStack)
        {
            if (underlyingCalculator == null) throw new NullException(() => underlyingCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _underlyingCalculator = underlyingCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif

            double value = _underlyingCalculator.Calculate(position);
            return value;
        }
    }

    internal class Curve_OperatorCalculator_MinXZero_WithOriginShifting : OperatorCalculatorBase
    {
        private readonly ArrayCalculator_MinPositionZero_Line _underlyingCalculator;
        private readonly DimensionStack _dimensionStack;
        // ReSharper disable once NotAccessedField.Local
        private readonly int _dimensionStackIndex;

        private double _origin;

        public Curve_OperatorCalculator_MinXZero_WithOriginShifting(ArrayCalculator_MinPositionZero_Line underlyingCalculator, DimensionStack dimensionStack)
        {
            if (underlyingCalculator == null) throw new NullException(() => underlyingCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _underlyingCalculator = underlyingCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;

            // ReSharper disable once VirtualMemberCallInConstructor
            Reset();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif

            double phase = position - _origin;

            double value = _underlyingCalculator.Calculate(phase);

            return value;
        }

        public override void Reset()
        {
            ResetPrivate();
        }

        private void ResetPrivate()
        {
#if !USE_INVAR_INDICES
            _origin = _dimensionStack.Get();
#else
            _origin = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
        }
    }
}
