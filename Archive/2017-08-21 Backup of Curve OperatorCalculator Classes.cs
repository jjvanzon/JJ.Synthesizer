//using System;
//using JJ.Framework.Exceptions;
//using System.Runtime.CompilerServices;
//using JJ.Business.Synthesizer.Calculation.Arrays;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    internal class Curve_OperatorCalculator_MinX_NoOriginShifting : OperatorCalculatorBase
//    {
//        private readonly ArrayCalculator_MinPosition_Line _underlyingCalculator;
//        private readonly DimensionStack _dimensionStack;

//        public Curve_OperatorCalculator_MinX_NoOriginShifting(ArrayCalculator_MinPosition_Line underlyingCalculator, DimensionStack dimensionStack)
//        {
//            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

//            _underlyingCalculator = underlyingCalculator ?? throw new NullException(() => underlyingCalculator);
//            _dimensionStack = dimensionStack;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double position = _dimensionStack.Get();
//            double value = _underlyingCalculator.Calculate(position);
//            return value;
//        }
//    }

//    internal class Curve_OperatorCalculator_MinX_NoOriginShifting_WithPositionInput : OperatorCalculatorBase
//    {
//        private readonly OperatorCalculatorBase _positionCalculator;
//        private readonly ArrayCalculator_MinPosition_Line _underlyingArrayCalculator;

//        public Curve_OperatorCalculator_MinX_NoOriginShifting_WithPositionInput(
//            OperatorCalculatorBase positionCalculator,
//            ArrayCalculator_MinPosition_Line underlyingArrayCalculator)
//        {
//            _positionCalculator = positionCalculator ?? throw new ArgumentNullException(nameof(positionCalculator));

//            _underlyingArrayCalculator = underlyingArrayCalculator ?? throw new NullException(() => underlyingArrayCalculator);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double position = _positionCalculator.Calculate();
//            double value = _underlyingArrayCalculator.Calculate(position);
//            return value;
//        }
//    }

//    internal class Curve_OperatorCalculator_MinX_WithOriginShifting : OperatorCalculatorBase
//    {
//        private readonly ArrayCalculator_MinPosition_Line _underlyingCalculator;
//        private readonly DimensionStack _dimensionStack;

//        private double _origin;

//        public Curve_OperatorCalculator_MinX_WithOriginShifting(ArrayCalculator_MinPosition_Line underlyingCalculator, DimensionStack dimensionStack)
//        {
//            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

//            _underlyingCalculator = underlyingCalculator ?? throw new NullException(() => underlyingCalculator);
//            _dimensionStack = dimensionStack;

//            ResetPrivate();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double position = _dimensionStack.Get();
//            double phase = position - _origin;
//            double value = _underlyingCalculator.Calculate(phase);
//            return value;
//        }

//        public override void Reset() => ResetPrivate();
//        private void ResetPrivate() => _origin = _dimensionStack.Get();
//    }

//    internal class Curve_OperatorCalculator_MinX_WithOriginShifting_WithPositionInput : OperatorCalculatorBase
//    {
//        private readonly OperatorCalculatorBase _positionCalculator;
//        private readonly ArrayCalculator_MinPosition_Line _underlyingArrayCalculator;

//        private double _origin;

//        public Curve_OperatorCalculator_MinX_WithOriginShifting_WithPositionInput(
//            OperatorCalculatorBase positionCalculator,
//            ArrayCalculator_MinPosition_Line underlyingCalculator)
//        {
//            _positionCalculator = positionCalculator ?? throw new ArgumentNullException(nameof(positionCalculator));
//            _underlyingArrayCalculator = underlyingCalculator ?? throw new NullException(() => underlyingCalculator);

//            ResetPrivate();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double position = _positionCalculator.Calculate();
//            double phase = position - _origin;
//            double value = _underlyingArrayCalculator.Calculate(phase);
//            return value;
//        }

//        public override void Reset() => ResetPrivate();
//        private void ResetPrivate() => _origin = _positionCalculator.Calculate();
//    }

//    internal class Curve_OperatorCalculator_MinXZero_NoOriginShifting : OperatorCalculatorBase
//    {
//        private readonly ArrayCalculator_MinPositionZero_Line _underlyingCalculator;
//        private readonly DimensionStack _dimensionStack;

//        public Curve_OperatorCalculator_MinXZero_NoOriginShifting(ArrayCalculator_MinPositionZero_Line underlyingCalculator, DimensionStack dimensionStack)
//        {
//            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

//            _underlyingCalculator = underlyingCalculator ?? throw new NullException(() => underlyingCalculator);
//            _dimensionStack = dimensionStack;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double position = _dimensionStack.Get();
//            double value = _underlyingCalculator.Calculate(position);
//            return value;
//        }
//    }

//    internal class Curve_OperatorCalculator_MinXZero_NoOriginShifting_WithPositionInput : OperatorCalculatorBase
//    {
//        private readonly OperatorCalculatorBase _positionCalculator;
//        private readonly ArrayCalculator_MinPositionZero_Line _underlyingArrayCalculator;

//        public Curve_OperatorCalculator_MinXZero_NoOriginShifting_WithPositionInput(
//            OperatorCalculatorBase positionCalculator,
//            ArrayCalculator_MinPositionZero_Line underlyingArrayCalculator)
//        {
//            _positionCalculator = positionCalculator ?? throw new ArgumentNullException(nameof(positionCalculator));
//            _underlyingArrayCalculator = underlyingArrayCalculator ?? throw new NullException(() => underlyingArrayCalculator);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double position = _positionCalculator.Calculate();
//            double value = _underlyingArrayCalculator.Calculate(position);
//            return value;
//        }
//    }

//    internal class Curve_OperatorCalculator_MinXZero_WithOriginShifting : OperatorCalculatorBase
//    {
//        private readonly ArrayCalculator_MinPositionZero_Line _underlyingCalculator;
//        private readonly DimensionStack _dimensionStack;

//        private double _origin;

//        public Curve_OperatorCalculator_MinXZero_WithOriginShifting(ArrayCalculator_MinPositionZero_Line underlyingCalculator, DimensionStack dimensionStack)
//        {
//            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

//            _underlyingCalculator = underlyingCalculator ?? throw new NullException(() => underlyingCalculator);
//            _dimensionStack = dimensionStack;

//            ResetPrivate();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double position = _dimensionStack.Get();
//            double phase = position - _origin;
//            double value = _underlyingCalculator.Calculate(phase);
//            return value;
//        }

//        public override void Reset() => ResetPrivate();
//        private void ResetPrivate() => _origin = _dimensionStack.Get();
//    }

//    internal class Curve_OperatorCalculator_MinXZero_WithOriginShifting_WithPositionCalculator : OperatorCalculatorBase
//    {
//        private readonly OperatorCalculatorBase _positionCalculator;
//        private readonly ArrayCalculator_MinPositionZero_Line _underlyingArrayCalculator;

//        private double _origin;

//        public Curve_OperatorCalculator_MinXZero_WithOriginShifting_WithPositionCalculator(
//            OperatorCalculatorBase positionCalculator,
//            ArrayCalculator_MinPositionZero_Line underlyingCalculator)
//        {
//            _positionCalculator = positionCalculator ?? throw new ArgumentNullException(nameof(positionCalculator));
//            _underlyingArrayCalculator = underlyingCalculator ?? throw new NullException(() => underlyingCalculator);

//            ResetPrivate();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double position = _positionCalculator.Calculate();
//            double phase = position - _origin;
//            double value = _underlyingArrayCalculator.Calculate(phase);
//            return value;
//        }

//        public override void Reset() => ResetPrivate();
//        private void ResetPrivate() => _origin = _positionCalculator.Calculate();
//    }
//}
