using JJ.Framework.Exceptions;
using System.Runtime.CompilerServices;
// ReSharper disable CompareOfFloatsByEqualityOperator

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Stretch_OperatorCalculator_ZeroOrigin 
        : OperatorCalculatorBase_WithChildCalculators, IPositionTransformer
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _factorCalculator;
        private readonly DimensionStack _dimensionStack;

        public Stretch_OperatorCalculator_ZeroOrigin(
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
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double transformedPosition = GetTransformedPosition();

            _dimensionStack.Push(transformedPosition);

            double result = _signalCalculator.Calculate();

            _dimensionStack.Pop();
            return result;
        }

        public override void Reset()
        {
            double transformedPosition = GetTransformedPosition();

            _dimensionStack.Push(transformedPosition);

            base.Reset();

            _dimensionStack.Pop();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetTransformedPosition()
        {
            double position = _dimensionStack.Get();

            double factor = _factorCalculator.Calculate();

            double transformedPosition = StretchOperatorCalculatorHelper.TransformPosition(position, factor);

            return transformedPosition;
        }
    }

    internal class Stretch_OperatorCalculator_WithOrigin 
        : OperatorCalculatorBase_WithChildCalculators, IPositionTransformer
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _factorCalculator;
        private readonly OperatorCalculatorBase _originCalculator;
        private readonly DimensionStack _dimensionStack;

        public Stretch_OperatorCalculator_WithOrigin(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase factorCalculator,
            OperatorCalculatorBase originCalculator,
            DimensionStack dimensionStack)
            : base(new[]
            {
                signalCalculator,
                factorCalculator,
                originCalculator
            })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(factorCalculator, () => factorCalculator);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(originCalculator, () => originCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _signalCalculator = signalCalculator;
            _factorCalculator = factorCalculator;
            _originCalculator = originCalculator;
            _dimensionStack = dimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double transformedPosition = GetTransformedPosition();

            _dimensionStack.Push(transformedPosition);

            double result = _signalCalculator.Calculate();

            _dimensionStack.Pop();

            return result;
        }

        public override void Reset()
        {
            double transformedPosition = GetTransformedPosition();

            _dimensionStack.Push(transformedPosition);

            base.Reset();

            _dimensionStack.Pop();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetTransformedPosition()
        {
            double position = _dimensionStack.Get();

            double factor = _factorCalculator.Calculate();
            double origin = _originCalculator.Calculate();

            double transformedPosition = StretchOperatorCalculatorHelper.TransformPosition(position, factor, origin);

            return transformedPosition;
        }
    }

    // For Time Dimension

    internal class Stretch_OperatorCalculator_VarFactor_WithPhaseTracking 
        : OperatorCalculatorBase_WithChildCalculators, IPositionTransformer
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _factorCalculator;
        private readonly DimensionStack _dimensionStack;

        private double _phase;
        private double _previousPosition;

        public Stretch_OperatorCalculator_VarFactor_WithPhaseTracking(
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

            ResetNonRecursive();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = GetPosition();

            _phase = GetTransformedPosition(position);

            _dimensionStack.Push(_phase);

            double result = _signalCalculator.Calculate();

            _dimensionStack.Pop();

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
            double transformedPosition = GetTransformedPosition();

            _dimensionStack.Push(transformedPosition);

            base.Reset();

            _dimensionStack.Pop();
        }

        private void ResetNonRecursive()
        {
            // Phase Tracking
            _previousPosition = GetPosition();
            _phase = 0.0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetPosition()
        {
            return _dimensionStack.Get();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetTransformedPosition()
        {
            double position = GetPosition();
            return GetTransformedPosition(position);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetTransformedPosition(double position)
        {
            double factor = _factorCalculator.Calculate();

            double phase = StretchOperatorCalculatorHelper.TransformPosition(position, factor, _phase, _previousPosition);

            return phase;
        }
    }

    internal class Stretch_OperatorCalculator_ConstFactor_WithOriginShifting 
        : OperatorCalculatorBase_WithChildCalculators, IPositionTransformer
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _factor;
        private readonly DimensionStack _dimensionStack;

        private double _origin;

        public Stretch_OperatorCalculator_ConstFactor_WithOriginShifting(
            OperatorCalculatorBase signalCalculator,
            double factor,
            DimensionStack dimensionStack)
            : base(new[] { signalCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertFactor(factor);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _signalCalculator = signalCalculator;
            _factor = factor;
            _dimensionStack = dimensionStack;

            ResetNonRecursive();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double transformedPosition = GetTransformedPosition();

            _dimensionStack.Push(transformedPosition);

            double result = _signalCalculator.Calculate();

            _dimensionStack.Pop();
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
            double transformedPosition = GetTransformedPosition();

            _dimensionStack.Push(transformedPosition);

            base.Reset();

            _dimensionStack.Pop();
        }

        private void ResetNonRecursive()
        {
            // Origin Shifting
            _origin = GetPosition();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetPosition()
        {
            return _dimensionStack.Get();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetTransformedPosition()
        {
            double position = GetPosition();

            return StretchOperatorCalculatorHelper.TransformPosition(position, _factor, _origin);
        }
    }

    public static class StretchOperatorCalculatorHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double TransformPosition(double position, double factor, double origin)
        {
            // IMPORTANT: To stretch things in the output, you have to squash things in the input.
            double transformedPosition = (position - origin) / factor + origin;

            return transformedPosition;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double TransformPosition(double position, double factor, double phase, double previousPosition)
        {
            // IMPORTANT: To stretch things in the output, you have to squash things in the input.
            double positionChange = position - previousPosition;
            double transformedPosition = phase + positionChange / factor;
            return transformedPosition;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double TransformPosition(double position, double factor)
        {
            // IMPORTANT: To stretch things in the output, you have to squash things in the input.
            double transformedPosition = position / factor;
            return transformedPosition;
        }
    }
}
