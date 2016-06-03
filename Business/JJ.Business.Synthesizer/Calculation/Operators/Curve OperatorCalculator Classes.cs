using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Calculation.Curves;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Curve_OperatorCalculator_MinX_NoOriginShifting : OperatorCalculatorBase
    {
        private readonly CurveCalculator_MinX _curveCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public Curve_OperatorCalculator_MinX_NoOriginShifting(CurveCalculator_MinX curveCalculator, DimensionStack dimensionStack)
        {
            if (curveCalculator == null) throw new NullException(() => curveCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForReaders(dimensionStack);

            _curveCalculator = curveCalculator;
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

            double value = _curveCalculator.CalculateY(position);

            return value;
        }
    }

    internal class Curve_OperatorCalculator_MinX_WithOriginShifting : OperatorCalculatorBase
    {
        private readonly CurveCalculator_MinX _curveCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _origin;

        public Curve_OperatorCalculator_MinX_WithOriginShifting(CurveCalculator_MinX curveCalculator, DimensionStack dimensionStack)
        {
            if (curveCalculator == null) throw new NullException(() => curveCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForReaders(dimensionStack);

            _curveCalculator = curveCalculator;
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

            double phase = position - _origin;

            double value = _curveCalculator.CalculateY(phase);

            return value;
        }

        public override void Reset()
        {
#if !USE_INVAR_INDICES
            _origin = _dimensionStack.Get();
#else
            _origin = _dimensionStack.Get(_dimensionStackIndex);
#endif

#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif

            base.Reset();
        }
    }

    internal class Curve_OperatorCalculator_MinXZero_NoOriginShifting : OperatorCalculatorBase
    {
        private readonly CurveCalculator_MinXZero _curveCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public Curve_OperatorCalculator_MinXZero_NoOriginShifting(CurveCalculator_MinXZero curveCalculator, DimensionStack dimensionStack)
        {
            if (curveCalculator == null) throw new NullException(() => curveCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForReaders(dimensionStack);

            _curveCalculator = curveCalculator;
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

            double value = _curveCalculator.CalculateY(position);
            return value;
        }
    }

    internal class Curve_OperatorCalculator_MinXZero_WithOriginShifting : OperatorCalculatorBase
    {
        private readonly CurveCalculator_MinXZero _curveCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _origin;

        public Curve_OperatorCalculator_MinXZero_WithOriginShifting(CurveCalculator_MinXZero curveCalculator, DimensionStack dimensionStack)
        {
            if (curveCalculator == null) throw new NullException(() => curveCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForReaders(dimensionStack);

            _curveCalculator = curveCalculator;
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

            double phase = position - _origin;

            double value = _curveCalculator.CalculateY(phase);

            return value;
        }

        public override void Reset()
        {
#if !USE_INVAR_INDICES
            _origin = _dimensionStack.Get();
#else
            _origin = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif

            base.Reset();
        }
    }
}
