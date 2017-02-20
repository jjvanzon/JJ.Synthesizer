using JJ.Framework.Exceptions;
using System;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class TimePower_OperatorCalculator_VarSignal_VarExponent_VarOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _exponentCalculator;
        private readonly OperatorCalculatorBase _originCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _nextDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        public TimePower_OperatorCalculator_VarSignal_VarExponent_VarOrigin(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase exponentCalculator,
            OperatorCalculatorBase originCalculator,
            DimensionStack dimensionStack)
            : base(new[] { signalCalculator, exponentCalculator, originCalculator })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (exponentCalculator == null) throw new NullException(() => exponentCalculator);
            if (originCalculator == null) throw new NullException(() => originCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _signalCalculator = signalCalculator;
            _exponentCalculator = exponentCalculator;
            _originCalculator = originCalculator;
            _dimensionStack = dimensionStack;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex;
            _nextDimensionStackIndex = dimensionStack.CurrentIndex + 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            PushTransformedPosition();

            double result = _signalCalculator.Calculate();

#if !USE_INVAR_INDICES
            _dimensionStack.Pop();
#endif
            return result;
        }

        public override void Reset()
        {
            PushTransformedPosition();

            base.Reset();

#if !USE_INVAR_INDICES
            _dimensionStack.Pop();
#endif
        }

        private void PushTransformedPosition()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_previousDimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _previousDimensionStackIndex);
#endif
            double exponent = _exponentCalculator.Calculate();
            double origin = _originCalculator.Calculate();

            double transformedPosition = TimePower_OperatorCalculator_Helper.GetTransformedPosition(position, exponent, origin);

#if !USE_INVAR_INDICES
            _dimensionStack.Push(transformedPosition);
#else
            _dimensionStack.Set(_nextDimensionStackIndex, transformedPosition);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
#endif
        }
    }

    internal class TimePower_OperatorCalculator_VarSignal_VarExponent_ZeroOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _exponentCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _nextDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        public TimePower_OperatorCalculator_VarSignal_VarExponent_ZeroOrigin(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase exponentCalculator,
            DimensionStack dimensionStack)
            : base(new[] { signalCalculator, exponentCalculator })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (exponentCalculator == null) throw new NullException(() => exponentCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _signalCalculator = signalCalculator;
            _exponentCalculator = exponentCalculator;
            _dimensionStack = dimensionStack;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex;
            _nextDimensionStackIndex = dimensionStack.CurrentIndex + 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            PushTransformedPosition();

            double result = _signalCalculator.Calculate();

#if !USE_INVAR_INDICES
            _dimensionStack.Pop();
#endif
            return result;
        }

        public override void Reset()
        {
            PushTransformedPosition();

            base.Reset();

#if !USE_INVAR_INDICES
            _dimensionStack.Pop();
#endif
        }

        private void PushTransformedPosition()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_previousDimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _previousDimensionStackIndex);
#endif
            double exponent = _exponentCalculator.Calculate();

            double transformedPosition = TimePower_OperatorCalculator_Helper.GetTransformedPosition(position, exponent);

#if !USE_INVAR_INDICES
            _dimensionStack.Push(transformedPosition);
#else
            _dimensionStack.Set(_nextDimensionStackIndex, transformedPosition);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
#endif
        }
    }
}
