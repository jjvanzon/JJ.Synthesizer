using JJ.Framework.Reflection.Exceptions;
using System;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Reverse_OperatorCalculator_VarSpeed_WithPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _speedCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _nextDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        private double _phase;
        private double _previousPosition;

        public Reverse_OperatorCalculator_VarSpeed_WithPhaseTracking(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase speedCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator,
                speedCalculator
            })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(speedCalculator, () => speedCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _signalCalculator = signalCalculator;
            _speedCalculator = speedCalculator;
            _dimensionStack = dimensionStack;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex;
            _nextDimensionStackIndex = dimensionStack.CurrentIndex + 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_previousDimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _previousDimensionStackIndex);
#endif

            _phase = GetTransformedPosition(position);
#if !USE_INVAR_INDICES
            _dimensionStack.Push(_phase);
#else
            _dimensionStack.Set(_nextDimensionStackIndex, _phase);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
#endif

            double result = _signalCalculator.Calculate();
#if !USE_INVAR_INDICES
            _dimensionStack.Pop();
#endif
            _previousPosition = position;

            return result;
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

            _previousPosition = position;
            _phase = 0.0;

            double transformedPosition = GetTransformedPosition(position);
#if !USE_INVAR_INDICES
            _dimensionStack.Push(transformedPosition);
#else
            _dimensionStack.Set(_nextDimensionStackIndex, transformedPosition);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
#endif

            base.Reset();
#if !USE_INVAR_INDICES
            _dimensionStack.Pop();
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetTransformedPosition(double position)
        {
            double speed = _speedCalculator.Calculate();

            double positionChange = position - _previousPosition;

            // IMPORTANT: To squash things in the output, you have to stretch things in the input.
            double phase = _phase - positionChange * speed;

            return phase;
        }
    }

    internal class Reverse_OperatorCalculator_VarSpeed_NoPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _speedCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _nextDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        public Reverse_OperatorCalculator_VarSpeed_NoPhaseTracking(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase speedCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator,
                speedCalculator
            })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(speedCalculator, () => speedCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _signalCalculator = signalCalculator;
            _speedCalculator = speedCalculator;
            _dimensionStack = dimensionStack;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex;
            _nextDimensionStackIndex = dimensionStack.CurrentIndex + 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_previousDimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _previousDimensionStackIndex);
#endif

            double transformedPosition = GetTransformedPosition(position);
#if !USE_INVAR_INDICES
            _dimensionStack.Push(transformedPosition);
#else
            _dimensionStack.Set(_nextDimensionStackIndex, transformedPosition);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
#endif

            double result = _signalCalculator.Calculate();
#if !USE_INVAR_INDICES
            _dimensionStack.Pop();
#endif
            return result;
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

            double transformedPosition = GetTransformedPosition(position);
#if !USE_INVAR_INDICES
            _dimensionStack.Push(transformedPosition);
#else
            _dimensionStack.Set(_nextDimensionStackIndex, transformedPosition);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
#endif

            base.Reset();
#if !USE_INVAR_INDICES
            _dimensionStack.Pop();
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetTransformedPosition(double position)
        {
            double speed = _speedCalculator.Calculate();

            // IMPORTANT: To squash things in the output, you have to stretch things in the input.
            double transformedPosition = position * speed;

            return transformedPosition;
        }
    }

    internal class Reverse_OperatorCalculator_ConstSpeed_WithOriginShifting : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _speed;
        private readonly DimensionStack _dimensionStack;
        private readonly int _nextDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        private double _origin;

        public Reverse_OperatorCalculator_ConstSpeed_WithOriginShifting(
            OperatorCalculatorBase signalCalculator,
            double speed,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
            if (speed == 0) throw new ZeroException(() => speed);
            if (Double.IsNaN(speed)) throw new NaNException(() => speed);
            if (Double.IsInfinity(speed)) throw new InfinityException(() => speed);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _signalCalculator = signalCalculator;
            _speed = -speed;
            _dimensionStack = dimensionStack;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex;
            _nextDimensionStackIndex = dimensionStack.CurrentIndex + 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_previousDimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _previousDimensionStackIndex);
#endif

            double transformedPosition = GetTransformedPosition(position);

#if !USE_INVAR_INDICES
            _dimensionStack.Push(transformedPosition);
#else
            _dimensionStack.Set(_nextDimensionStackIndex, transformedPosition);
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
            double tranformedPosition = GetTransformedPosition(position);
#if !USE_INVAR_INDICES
            _dimensionStack.Push(tranformedPosition);
#else
            _dimensionStack.Set(_nextDimensionStackIndex, tranformedPosition);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
#endif

            base.Reset();
#if !USE_INVAR_INDICES
            _dimensionStack.Pop();
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetTransformedPosition(double position)
        {
            // IMPORTANT: To squash things in the output, you have to stretch things in the input.
            double transformedPosition = (position - _origin) * _speed;

            return transformedPosition;
        }
    }

    internal class Reverse_OperatorCalculator_ConstSpeed_NoOriginShifting : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _speed;
        private readonly DimensionStack _dimensionStack;
        private readonly int _nextDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        public Reverse_OperatorCalculator_ConstSpeed_NoOriginShifting(
            OperatorCalculatorBase signalCalculator,
            double speed,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
            if (speed == 0) throw new ZeroException(() => speed);
            if (Double.IsNaN(speed)) throw new NaNException(() => speed);
            if (Double.IsInfinity(speed)) throw new InfinityException(() => speed);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _signalCalculator = signalCalculator;
            _speed = -speed;
            _dimensionStack = dimensionStack;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex;
            _nextDimensionStackIndex = dimensionStack.CurrentIndex + 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double transformedPosition = GetTransformedPosition();
#if !USE_INVAR_INDICES
            _dimensionStack.Push(transformedPosition);
#else
            _dimensionStack.Set(_nextDimensionStackIndex, transformedPosition);
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
            double tranformedPosition = GetTransformedPosition();
#if !USE_INVAR_INDICES
            _dimensionStack.Push(tranformedPosition);
#else
            _dimensionStack.Set(_nextDimensionStackIndex, tranformedPosition);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
#endif

            base.Reset();
#if !USE_INVAR_INDICES
            _dimensionStack.Pop();
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetTransformedPosition()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_previousDimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _previousDimensionStackIndex);
#endif

            // IMPORTANT: To squash things in the output, you have to stretch things in the input.
            double transformedPosition = position * _speed;

            return transformedPosition;
        }
    }
}
