//using System;
//using System.Linq.Expressions;
//using JJ.Framework.Exceptions;
//using System.Runtime.CompilerServices;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    // TODO: Remove outcommented code.

//    //internal class Squash_OperatorCalculator_ZeroOrigin : OperatorCalculatorBase_WithChildCalculators
//    //{
//    //    private readonly OperatorCalculatorBase _signalCalculator;
//    //    private readonly OperatorCalculatorBase _factorCalculator;
//    //    private readonly DimensionStack _dimensionStack;

//    //    public Squash_OperatorCalculator_ZeroOrigin(
//    //        OperatorCalculatorBase signalCalculator,
//    //        OperatorCalculatorBase factorCalculator,
//    //        DimensionStack dimensionStack)
//    //        : base(new[]
//    //        {
//    //            signalCalculator,
//    //            factorCalculator
//    //        })
//    //    {
//    //        if (signalCalculator == null) throw new NullException(() => signalCalculator);
//    //        if (factorCalculator == null) throw new NullException(() => factorCalculator);
//    //        if (dimensionStack == null) throw new NullException(() => dimensionStack);

//    //        _signalCalculator = signalCalculator;
//    //        _factorCalculator = factorCalculator;
//    //        _dimensionStack = dimensionStack;
//    //    }

//    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    //    public override double Calculate()
//    //    {
//    //        double transformedPosition = GetTransformedPosition();

//    //        _dimensionStack.Push(transformedPosition);

//    //        double result = _signalCalculator.Calculate();

//    //        _dimensionStack.Pop();
//    //        return result;
//    //    }

//    //    public override void Reset()
//    //    {
//    //        double transformedPosition = GetTransformedPosition();

//    //        _dimensionStack.Push(transformedPosition);
//    //        base.Reset();

//    //        _dimensionStack.Pop();
//    //    }

//    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    //    private double GetTransformedPosition()
//    //    {
//    //        double position = _dimensionStack.Get();

//    //        double factor = _factorCalculator.Calculate();

//    //        // IMPORTANT: To squash things in the output, you have to stretch things in the input.
//    //        double transformedPosition = position * factor;

//    //        return transformedPosition;
//    //    }
//    //}

//    //internal class Squash_OperatorCalculator_WithOrigin : OperatorCalculatorBase_WithChildCalculators
//    //{
//    //    private readonly OperatorCalculatorBase _signalCalculator;
//    //    private readonly OperatorCalculatorBase _factorCalculator;
//    //    private readonly OperatorCalculatorBase _originCalculator;
//    //    private readonly DimensionStack _dimensionStack;

//    //    public Squash_OperatorCalculator_WithOrigin(
//    //        OperatorCalculatorBase signalCalculator,
//    //        OperatorCalculatorBase factorCalculator,
//    //        OperatorCalculatorBase originCalculator,
//    //        DimensionStack dimensionStack)
//    //        : base(new[]
//    //        {
//    //            signalCalculator,
//    //            factorCalculator,
//    //            originCalculator
//    //        })
//    //    {
//    //        if (signalCalculator == null) throw new NullException(() => signalCalculator);
//    //        if (factorCalculator == null) throw new NullException(() => factorCalculator);
//    //        if (originCalculator == null) throw new NullException(() => originCalculator);
//    //        if (dimensionStack == null) throw new NullException(() => dimensionStack);

//    //        _signalCalculator = signalCalculator;
//    //        _factorCalculator = factorCalculator;
//    //        _originCalculator = originCalculator;
//    //        _dimensionStack = dimensionStack;
//    //    }

//    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    //    public override double Calculate()
//    //    {
//    //        double transformedPosition = GetTransformedPosition();

//    //        _dimensionStack.Push(transformedPosition);

//    //        double result = _signalCalculator.Calculate();

//    //        _dimensionStack.Pop();

//    //        return result;
//    //    }

//    //    public override void Reset()
//    //    {
//    //        double transformedPosition = GetTransformedPosition();

//    //        _dimensionStack.Push(transformedPosition);

//    //        base.Reset();

//    //        _dimensionStack.Pop();
//    //    }

//    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    //    private double GetTransformedPosition()
//    //    {
//    //        double position = _dimensionStack.Get();

//    //        double factor = _factorCalculator.Calculate();
//    //        double origin = _originCalculator.Calculate();

//    //        // IMPORTANT: To squash things in the output, you have to stretch things in the input.
//    //        double transformedPosition = (position - origin) * factor + origin;

//    //        return transformedPosition;
//    //    }
//    //}

//    //// For Time Dimension

//    //internal class Squash_OperatorCalculator_VarFactor_WithPhaseTracking : OperatorCalculatorBase_WithChildCalculators
//    //{
//    //    private readonly OperatorCalculatorBase _signalCalculator;
//    //    private readonly OperatorCalculatorBase _factorCalculator;
//    //    private readonly DimensionStack _dimensionStack;

//    //    private double _phase;
//    //    private double _previousPosition;

//    //    public Squash_OperatorCalculator_VarFactor_WithPhaseTracking(
//    //        OperatorCalculatorBase signalCalculator,
//    //        OperatorCalculatorBase factorCalculator,
//    //        DimensionStack dimensionStack)
//    //        : base(new[] { signalCalculator, factorCalculator })
//    //    {
//    //        if (signalCalculator == null) throw new NullException(() => signalCalculator);
//    //        if (factorCalculator == null) throw new NullException(() => factorCalculator);
//    //        if (dimensionStack == null) throw new NullException(() => dimensionStack);

//    //        _signalCalculator = signalCalculator;
//    //        _factorCalculator = factorCalculator;
//    //        _dimensionStack = dimensionStack;

//    //        ResetNonRecursive();
//    //    }

//    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    //    public override double Calculate()
//    //    {
//    //        double position = _dimensionStack.Get();

//    //        _phase = GetTransformedPosition(position);

//    //        _dimensionStack.Push(_phase);

//    //        double result = _signalCalculator.Calculate();

//    //        _dimensionStack.Pop();
//    //        _previousPosition = position;

//    //        return result;
//    //    }

//    //    public override void Reset()
//    //    {
//    //        // First reset parent, then children,
//    //        // because unlike some other operators,
//    //        // child state is dependent transformed position,
//    //        // which is dependent on parent state.
//    //        ResetNonRecursive();

//    //        // Dimension Transformation
//    //        double position = GetPosition();
//    //        double transformedPosition = GetTransformedPosition(position);

//    //        _dimensionStack.Push(transformedPosition);

//    //        base.Reset();

//    //        _dimensionStack.Pop();
//    //    }

//    //    private void ResetNonRecursive()
//    //    {
//    //        // Phase Tracking
//    //        _previousPosition = GetPosition();
//    //        _phase = 0.0;
//    //    }

//    //    private double GetPosition()
//    //    {
//    //        return _dimensionStack.Get();
//    //    }

//    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    //    private double GetTransformedPosition(double position)
//    //    {
//    //        double factor = _factorCalculator.Calculate();

//    //        double distance = position - _previousPosition;

//    //        // IMPORTANT: To squash things in the output, you have to stretch things in the input.
//    //        double phase = _phase + distance * factor;

//    //        return phase;
//    //    }
//    //}

//    //internal class Squash_OperatorCalculator_ConstFactor_WithOriginShifting : OperatorCalculatorBase_WithChildCalculators
//    //{
//    //    private readonly OperatorCalculatorBase _signalCalculator;
//    //    private readonly double _factor;
//    //    private readonly DimensionStack _dimensionStack;

//    //    private double _origin;

//    //    public Squash_OperatorCalculator_ConstFactor_WithOriginShifting(
//    //        OperatorCalculatorBase signalCalculator,
//    //        double factor,
//    //        DimensionStack dimensionStack)
//    //        : base(new[] { signalCalculator })
//    //    {
//    //        OperatorCalculatorHelper.AssertFactor(factor);
//    //        if (dimensionStack == null) throw new NullException(() => dimensionStack);

//    //        _signalCalculator = signalCalculator ?? throw new NullException(() => signalCalculator);
//    //        _factor = factor;
//    //        _dimensionStack = dimensionStack;

//    //        ResetNonRecursive();
//    //    }

//    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    //    public override double Calculate()
//    //    {
//    //        double position = _dimensionStack.Get();

//    //        double transformedPosition = GetTransformedPosition(position);

//    //        _dimensionStack.Push(transformedPosition);

//    //        double result = _signalCalculator.Calculate();

//    //        _dimensionStack.Pop();

//    //        return result;
//    //    }

//    //    public override void Reset()
//    //    {
//    //        // First reset parent, then children,
//    //        // because unlike some other operators,
//    //        // child state is dependent transformed position,
//    //        // which is dependent on parent state.
//    //        ResetNonRecursive();

//    //        // Dimension Transformation
//    //        double position = GetPosition();
//    //        double transformedPosition = GetTransformedPosition(position);

//    //        _dimensionStack.Push(transformedPosition);

//    //        base.Reset();

//    //        _dimensionStack.Pop();
//    //    }

//    //    private void ResetNonRecursive()
//    //    {
//    //        // Origin Shifting
//    //        _origin = GetPosition();
//    //    }

//    //    private double GetPosition()
//    //    {
//    //        return _dimensionStack.Get();
//    //    }

//    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    //    private double GetTransformedPosition(double position)
//    //    {
//    //        // IMPORTANT: To squash things in the output, you have to stretch things in the input.
//    //        double transformedPosition = (position - _origin) * _factor + _origin;
//    //        return transformedPosition;
//    //    }
//    //}

//    // With Position Output

//    internal class Squash_OperatorCalculator_ZeroOrigin_WithPositionOutput : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly OperatorCalculatorBase _positionCalculator;
//        private readonly OperatorCalculatorBase _factorCalculator;

//        public Squash_OperatorCalculator_ZeroOrigin_WithPositionOutput(
//            OperatorCalculatorBase positionCalculator,
//            OperatorCalculatorBase factorCalculator)
//            : base(new[]
//            {
//                positionCalculator,
//                factorCalculator
//            })
//        {
//            _positionCalculator = positionCalculator ?? throw new NullException(() => positionCalculator);
//            _factorCalculator = factorCalculator ?? throw new NullException(() => factorCalculator);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double position = _positionCalculator.Calculate();
//            double factor = _factorCalculator.Calculate();

//            // IMPORTANT: To squash things in the output, you have to stretch things in the input.
//            double transformedPosition = position * factor;
//            return transformedPosition;
//        }
//    }

//    internal class Squash_OperatorCalculator_WithOrigin_WithPositionOutput : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly OperatorCalculatorBase _positionCalculator;
//        private readonly OperatorCalculatorBase _factorCalculator;
//        private readonly OperatorCalculatorBase _originCalculator;

//        public Squash_OperatorCalculator_WithOrigin_WithPositionOutput(
//            OperatorCalculatorBase positionCalculator,
//            OperatorCalculatorBase factorCalculator,
//            OperatorCalculatorBase originCalculator)
//            : base(new[]
//            {
//                positionCalculator,
//                factorCalculator,
//                originCalculator
//            })
//        {
//            _positionCalculator = positionCalculator ?? throw new NullException(() => positionCalculator);
//            _factorCalculator = factorCalculator ?? throw new NullException(() => factorCalculator);
//            _originCalculator = originCalculator ?? throw new NullException(() => originCalculator);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double position = _positionCalculator.Calculate();
//            double factor = _factorCalculator.Calculate();
//            double origin = _originCalculator.Calculate();

//            // IMPORTANT: To squash things in the output, you have to stretch things in the input.
//            double transformedPosition = (position - origin) * factor + origin;

//            return transformedPosition;
//        }
//    }

//    // For Time Dimension

//    internal class Squash_OperatorCalculator_VarFactor_WithPhaseTracking_WithPositionOutput : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly OperatorCalculatorBase _positionCalculator;
//        private readonly OperatorCalculatorBase _factorCalculator;

//        private double _phase;
//        private double _previousPosition;

//        public Squash_OperatorCalculator_VarFactor_WithPhaseTracking_WithPositionOutput(
//            OperatorCalculatorBase positionCalculator,
//            OperatorCalculatorBase factorCalculator)
//            : base(new[] { positionCalculator, factorCalculator })
//        {
//            _positionCalculator = positionCalculator ?? throw new NullException(() => positionCalculator);
//            _factorCalculator = factorCalculator ?? throw new NullException(() => factorCalculator);

//            ResetNonRecursive();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double factor = _factorCalculator.Calculate();
//            double position = _positionCalculator.Calculate();

//            double distance = position - _previousPosition;

//            // IMPORTANT: To squash things in the output, you have to stretch things in the input.
//            double phase = _phase + distance * factor;

//            _phase = phase;
//            _previousPosition = _positionCalculator.Calculate();

//            return _phase;
//        }

//        private void ResetNonRecursive()
//        {
//            // Phase Tracking
//            _previousPosition = _positionCalculator.Calculate();
//            _phase = 0.0;
//        }
//    }

//    internal class Squash_OperatorCalculator_ConstFactor_WithOriginShifting_WithPositionOutput : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly OperatorCalculatorBase _positionCalculator;
//        private readonly double _factor;

//        private double _origin;

//        public Squash_OperatorCalculator_ConstFactor_WithOriginShifting_WithPositionOutput(
//            OperatorCalculatorBase positionCalculator,
//            double factor)
//            : base(new[] { positionCalculator })
//        {
//            OperatorCalculatorHelper.AssertFactor(factor);

//            _positionCalculator = positionCalculator ?? throw new NullException(() => positionCalculator);
//            _factor = factor;

//            ResetNonRecursive();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double position = _positionCalculator.Calculate();

//            // IMPORTANT: To squash things in the output, you have to stretch things in the input.
//            double transformedPosition = (position - _origin) * _factor + _origin;

//            return transformedPosition;
//        }

//        public override void Reset()
//        {
//            base.Reset();

//            ResetNonRecursive();
//        }

//        private void ResetNonRecursive()
//        {
//            // Origin Shifting
//            _origin = _positionCalculator.Calculate();
//        }
//    }
//}
