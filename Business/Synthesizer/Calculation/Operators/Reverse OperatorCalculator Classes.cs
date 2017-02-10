using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Reverse_OperatorCalculator_VarFactor_WithPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _factorCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _nextDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        private double _phase;
        private double _previousPosition;

        public Reverse_OperatorCalculator_VarFactor_WithPhaseTracking(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase factorCalculator,
            DimensionStack dimensionStack)
            : base(new[]
            {
                signalCalculator,
                factorCalculator
            })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(factorCalculator, () => factorCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _signalCalculator = signalCalculator;
            _factorCalculator = factorCalculator;
            _dimensionStack = dimensionStack;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex;
            _nextDimensionStackIndex = dimensionStack.CurrentIndex + 1;

            ResetNonRecursive();
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
            // First reset parent, then children,
            // because unlike some other operators,
            // child state is dependent transformed position,
            // which is dependent on parent state.
            ResetNonRecursive();

            // Dimension Transformation
            double position = GetPosition();
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

        private void ResetNonRecursive()
        {
            // Phase Tracking
            _previousPosition = GetPosition();
            _phase = 0.0;
        }

        private double GetPosition()
        {
#if !USE_INVAR_INDICES
            return _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_previousDimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _previousDimensionStackIndex);
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetTransformedPosition(double position)
        {
            double factor = _factorCalculator.Calculate();

            double positionChange = position - _previousPosition;

            // IMPORTANT: To squash things in the output, you have to stretch things in the input.
            double phase = _phase - positionChange * factor;

            return phase;
        }
    }

    internal class Reverse_OperatorCalculator_VarFactor_NoPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _factorCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _nextDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        public Reverse_OperatorCalculator_VarFactor_NoPhaseTracking(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase factorCalculator,
            DimensionStack dimensionStack)
            : base(new[]
            {
                signalCalculator,
                factorCalculator
            })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(factorCalculator, () => factorCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _signalCalculator = signalCalculator;
            _factorCalculator = factorCalculator;
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
            double factor = _factorCalculator.Calculate();

            // IMPORTANT: To squash things in the output, you have to stretch things in the input.
            double transformedPosition = position * -factor;

            return transformedPosition;
        }
    }

    internal class Reverse_OperatorCalculator_ConstFactor_WithOriginShifting : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _negativeFactor;
        private readonly DimensionStack _dimensionStack;
        private readonly int _nextDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        private double _origin;

        public Reverse_OperatorCalculator_ConstFactor_WithOriginShifting(
            OperatorCalculatorBase signalCalculator,
            double factor,
            DimensionStack dimensionStack)
            : base(new[] { signalCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertReverseFactor(factor);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _signalCalculator = signalCalculator;
            _negativeFactor = -factor;
            _dimensionStack = dimensionStack;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex;
            _nextDimensionStackIndex = dimensionStack.CurrentIndex + 1;

            ResetNonRecursive();
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
            // First reset parent, then children,
            // because unlike some other operators,
            // child state is dependent transformed position,
            // which is dependent on parent state.
            ResetNonRecursive();

            // Dimension Transformation
            double position = GetPosition();
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

        private void ResetNonRecursive()
        {
            // Origin Shifting
            _origin = GetPosition();
        }

        private double GetPosition()
        {
#if !USE_INVAR_INDICES
            return _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_previousDimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _previousDimensionStackIndex);
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetTransformedPosition(double position)
        {
            // IMPORTANT: To squash things in the output, you have to stretch things in the input.
            double transformedPosition = (position - _origin) * _negativeFactor;

            return transformedPosition;
        }
    }

    internal class Reverse_OperatorCalculator_ConstFactor_NoOriginShifting : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _negativeFactor;
        private readonly DimensionStack _dimensionStack;
        private readonly int _nextDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        public Reverse_OperatorCalculator_ConstFactor_NoOriginShifting(
            OperatorCalculatorBase signalCalculator,
            double factor,
            DimensionStack dimensionStack)
            : base(new[] { signalCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertReverseFactor(factor);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _signalCalculator = signalCalculator;
            _negativeFactor = -factor;
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
            double transformedPosition = position * _negativeFactor;

            return transformedPosition;
        }
    }
}
