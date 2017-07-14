using JJ.Framework.Exceptions;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class TimePower_OperatorCalculator_VarSignal_VarExponent_VarOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _exponentCalculator;
        private readonly OperatorCalculatorBase _originCalculator;
        private readonly DimensionStack _dimensionStack;

        public TimePower_OperatorCalculator_VarSignal_VarExponent_VarOrigin(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase exponentCalculator,
            OperatorCalculatorBase originCalculator,
            DimensionStack dimensionStack)
            : base(new[] { signalCalculator, exponentCalculator, originCalculator })
        {
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _signalCalculator = signalCalculator ?? throw new NullException(() => signalCalculator);
            _exponentCalculator = exponentCalculator ?? throw new NullException(() => exponentCalculator);
            _originCalculator = originCalculator ?? throw new NullException(() => originCalculator);
            _dimensionStack = dimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            PushTransformedPosition();

            double result = _signalCalculator.Calculate();

            _dimensionStack.Pop();

            return result;
        }

        public override void Reset()
        {
            PushTransformedPosition();

            base.Reset();

            _dimensionStack.Pop();
        }

        private void PushTransformedPosition()
        {
            double position = _dimensionStack.Get();
            double exponent = _exponentCalculator.Calculate();
            double origin = _originCalculator.Calculate();

            double transformedPosition = TimePower_OperatorCalculator_Helper.GetTransformedPosition(position, exponent, origin);

            _dimensionStack.Push(transformedPosition);
        }
    }

    internal class TimePower_OperatorCalculator_VarSignal_VarExponent_ZeroOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _exponentCalculator;
        private readonly DimensionStack _dimensionStack;

        public TimePower_OperatorCalculator_VarSignal_VarExponent_ZeroOrigin(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase exponentCalculator,
            DimensionStack dimensionStack)
            : base(new[] { signalCalculator, exponentCalculator })
        {
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _signalCalculator = signalCalculator ?? throw new NullException(() => signalCalculator);
            _exponentCalculator = exponentCalculator ?? throw new NullException(() => exponentCalculator);
            _dimensionStack = dimensionStack;
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
