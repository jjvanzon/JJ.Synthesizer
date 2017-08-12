//using System.Runtime.CompilerServices;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    internal class Reverse_OperatorCalculator_VarFactor_WithPhaseTracking : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly OperatorCalculatorBase _signalCalculator;
//        private readonly OperatorCalculatorBase _factorCalculator;
//        private readonly DimensionStack _dimensionStack;

//        private double _phase;
//        private double _previousPosition;

//        public Reverse_OperatorCalculator_VarFactor_WithPhaseTracking(
//            OperatorCalculatorBase signalCalculator,
//            OperatorCalculatorBase factorCalculator,
//            DimensionStack dimensionStack)
//            : base(new[]
//            {
//                signalCalculator,
//                factorCalculator
//            })
//        {
//            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
//            OperatorCalculatorHelper.AssertChildOperatorCalculator(factorCalculator, () => factorCalculator);
//            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

//            _signalCalculator = signalCalculator;
//            _factorCalculator = factorCalculator;
//            _dimensionStack = dimensionStack;

//            ResetNonRecursive();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double position = _dimensionStack.Get();

//            _phase = GetTransformedPosition(position);
//            _dimensionStack.Push(_phase);

//            double result = _signalCalculator.Calculate();
//            _dimensionStack.Pop();

//            _previousPosition = position;

//            return result;
//        }

//        public override void Reset()
//        {
//            // First reset parent, then children,
//            // because unlike some other operators,
//            // child state is dependent transformed position,
//            // which is dependent on parent state.
//            ResetNonRecursive();

//            // Dimension Transformation
//            double position = GetPosition();
//            double transformedPosition = GetTransformedPosition(position);

//            _dimensionStack.Push(transformedPosition);

//            base.Reset();

//            _dimensionStack.Pop();
//        }

//        private void ResetNonRecursive()
//        {
//            // Phase Tracking
//            _previousPosition = GetPosition();
//            _phase = 0.0;
//        }

//        private double GetPosition()
//        {
//            return _dimensionStack.Get();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        private double GetTransformedPosition(double position)
//        {
//            double factor = _factorCalculator.Calculate();

//            double positionChange = position - _previousPosition;

//            // IMPORTANT: To squash things in the output, you have to stretch things in the input.
//            double phase = _phase - positionChange * factor;

//            return phase;
//        }
//    }

//    internal class Reverse_OperatorCalculator_VarFactor_NoPhaseTracking : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly OperatorCalculatorBase _signalCalculator;
//        private readonly OperatorCalculatorBase _factorCalculator;
//        private readonly DimensionStack _dimensionStack;

//        public Reverse_OperatorCalculator_VarFactor_NoPhaseTracking(
//            OperatorCalculatorBase signalCalculator,
//            OperatorCalculatorBase factorCalculator,
//            DimensionStack dimensionStack)
//            : base(new[]
//            {
//                signalCalculator,
//                factorCalculator
//            })
//        {
//            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
//            OperatorCalculatorHelper.AssertChildOperatorCalculator(factorCalculator, () => factorCalculator);
//            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

//            _signalCalculator = signalCalculator;
//            _factorCalculator = factorCalculator;
//            _dimensionStack = dimensionStack;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double position = _dimensionStack.Get();

//            double transformedPosition = GetTransformedPosition(position);

//            _dimensionStack.Push(transformedPosition);

//            double result = _signalCalculator.Calculate();

//            _dimensionStack.Pop();

//            return result;
//        }

//        public override void Reset()
//        {
//            double position = _dimensionStack.Get();

//            double transformedPosition = GetTransformedPosition(position);
//            _dimensionStack.Push(transformedPosition);
//            base.Reset();

//            _dimensionStack.Pop();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        private double GetTransformedPosition(double position)
//        {
//            double factor = _factorCalculator.Calculate();

//            // IMPORTANT: To squash things in the output, you have to stretch things in the input.
//            double transformedPosition = position * -factor;

//            return transformedPosition;
//        }
//    }

//    internal class Reverse_OperatorCalculator_ConstFactor_WithOriginShifting : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly OperatorCalculatorBase _signalCalculator;
//        private readonly double _negativeFactor;
//        private readonly DimensionStack _dimensionStack;

//        private double _origin;

//        public Reverse_OperatorCalculator_ConstFactor_WithOriginShifting(
//            OperatorCalculatorBase signalCalculator,
//            double factor,
//            DimensionStack dimensionStack)
//            : base(new[] { signalCalculator })
//        {
//            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
//            OperatorCalculatorHelper.AssertReverseFactor(factor);
//            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

//            _signalCalculator = signalCalculator;
//            _negativeFactor = -factor;
//            _dimensionStack = dimensionStack;

//            ResetNonRecursive();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double position = _dimensionStack.Get();

//            double transformedPosition = GetTransformedPosition(position);

//            _dimensionStack.Push(transformedPosition);

//            double value = _signalCalculator.Calculate();

//            _dimensionStack.Pop();

//            return value;
//        }

//        public override void Reset()
//        {
//            // First reset parent, then children,
//            // because unlike some other operators,
//            // child state is dependent transformed position,
//            // which is dependent on parent state.
//            ResetNonRecursive();

//            // Dimension Transformation
//            double position = GetPosition();
//            double tranformedPosition = GetTransformedPosition(position);

//            _dimensionStack.Push(tranformedPosition);

//            base.Reset();

//            _dimensionStack.Pop();
//        }

//        private void ResetNonRecursive()
//        {
//            // Origin Shifting
//            _origin = GetPosition();
//        }

//        private double GetPosition()
//        {
//            return _dimensionStack.Get();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        private double GetTransformedPosition(double position)
//        {
//            // IMPORTANT: To squash things in the output, you have to stretch things in the input.
//            double transformedPosition = (position - _origin) * _negativeFactor;

//            return transformedPosition;
//        }
//    }

//    internal class Reverse_OperatorCalculator_ConstFactor_NoOriginShifting : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly OperatorCalculatorBase _signalCalculator;
//        private readonly double _negativeFactor;
//        private readonly DimensionStack _dimensionStack;

//        public Reverse_OperatorCalculator_ConstFactor_NoOriginShifting(
//            OperatorCalculatorBase signalCalculator,
//            double factor,
//            DimensionStack dimensionStack)
//            : base(new[] { signalCalculator })
//        {
//            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
//            OperatorCalculatorHelper.AssertReverseFactor(factor);
//            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

//            _signalCalculator = signalCalculator;
//            _negativeFactor = -factor;
//            _dimensionStack = dimensionStack;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double transformedPosition = GetTransformedPosition();

//            _dimensionStack.Push(transformedPosition);

//            double value = _signalCalculator.Calculate();

//            _dimensionStack.Pop();

//            return value;
//        }

//        public override void Reset()
//        {
//            double tranformedPosition = GetTransformedPosition();

//            _dimensionStack.Push(tranformedPosition);

//            base.Reset();

//            _dimensionStack.Pop();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        private double GetTransformedPosition()
//        {
//            double position = _dimensionStack.Get();

//            // IMPORTANT: To squash things in the output, you have to stretch things in the input.
//            double transformedPosition = position * _negativeFactor;

//            return transformedPosition;
//        }
//    }
//}
