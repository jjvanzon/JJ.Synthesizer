using JJ.Framework.Exceptions;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Squash_OperatorCalculator_ZeroOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _factorCalculator;
        private readonly DimensionStack _dimensionStack;

        public Squash_OperatorCalculator_ZeroOrigin(
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
        private double GetTransformedPosition()
        {
            double position = _dimensionStack.Get();

            double factor = _factorCalculator.Calculate();

            // IMPORTANT: To squash things in the output, you have to stretch things in the input.
            double transformedPosition = position * factor;

            return transformedPosition;
        }
    }

    internal class Squash_OperatorCalculator_WithOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _factorCalculator;
        private readonly OperatorCalculatorBase _originCalculator;
        private readonly DimensionStack _dimensionStack;

        public Squash_OperatorCalculator_WithOrigin(
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
        private double GetTransformedPosition()
        {
            double position = _dimensionStack.Get();

            double factor = _factorCalculator.Calculate();
            double origin = _originCalculator.Calculate();

            // IMPORTANT: To squash things in the output, you have to stretch things in the input.
            double transformedPosition = (position - origin) * factor + origin;

            return transformedPosition;
        }
    }

    // For Time Dimension

    internal class Squash_OperatorCalculator_VarFactor_WithPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _factorCalculator;
        private readonly DimensionStack _dimensionStack;

        private double _phase;
        private double _previousPosition;

        public Squash_OperatorCalculator_VarFactor_WithPhaseTracking(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase factorCalculator,
            DimensionStack dimensionStack)
            : base(new[] { signalCalculator, factorCalculator })
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
            double position = _dimensionStack.Get();

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
            double position = GetPosition();
            double transformedPosition = GetTransformedPosition(position);

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

        private double GetPosition()
        {
            return _dimensionStack.Get();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetTransformedPosition(double position)
        {
            double factor = _factorCalculator.Calculate();

            double distance = position - _previousPosition;

            // IMPORTANT: To squash things in the output, you have to stretch things in the input.
            double phase = _phase + distance * factor;

            return phase;
        }
    }

    internal class Squash_OperatorCalculator_ConstFactor_WithOriginShifting : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _factor;
        private readonly DimensionStack _dimensionStack;

        private double _origin;

        public Squash_OperatorCalculator_ConstFactor_WithOriginShifting(
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
            double position = _dimensionStack.Get();

            double transformedPosition = GetTransformedPosition(position);

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
            double position = GetPosition();
            double transformedPosition = GetTransformedPosition(position);

            _dimensionStack.Push(transformedPosition);

            base.Reset();

            _dimensionStack.Pop();
        }

        private void ResetNonRecursive()
        {
            // Origin Shifting
            _origin = GetPosition();
        }

        private double GetPosition()
        {
            return _dimensionStack.Get();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetTransformedPosition(double position)
        {
            // IMPORTANT: To squash things in the output, you have to stretch things in the input.
            double transformedPosition = (position - _origin) * _factor + _origin;
            return transformedPosition;
        }
    }
}
