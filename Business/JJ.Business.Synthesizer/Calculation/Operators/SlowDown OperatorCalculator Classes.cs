using JJ.Framework.Reflection.Exceptions;
using System;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SlowDown_OperatorCalculator_VarFactor_WithPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _factorCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _currentDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        private double _phase;
        private double _previousPosition;

        public SlowDown_OperatorCalculator_VarFactor_WithPhaseTracking(
            OperatorCalculatorBase signalCalculator, 
            OperatorCalculatorBase factorCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] 
            {
                signalCalculator,
                factorCalculator
            })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(factorCalculator, () => factorCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

            _signalCalculator = signalCalculator;
            _factorCalculator = factorCalculator;
            _dimensionStack = dimensionStack;
            _currentDimensionStackIndex = dimensionStack.CurrentIndex;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_previousDimensionStackIndex);

            _phase = GetTransformedPosition(position);

            _dimensionStack.Set(_currentDimensionStackIndex, _phase);

            double result = _signalCalculator.Calculate();

            _previousPosition = position;

            return result;
        }

        public override void Reset()
        {
            double position = _dimensionStack.Get(_previousDimensionStackIndex);

            // Phase Tracking
            _previousPosition = position;
            _phase = 0.0;

            // Dimension Transformation
            double transformedPosition = GetTransformedPosition(position);

            _dimensionStack.Set(_currentDimensionStackIndex,transformedPosition);

            base.Reset();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetTransformedPosition(double position)
        {
            double factor = _factorCalculator.Calculate();

            // IMPORTANT: To stretch things in the output, you have to squash things in the input.
            double positionChange = position - _previousPosition;
            double phase = _phase + positionChange / factor;

            return phase;
        }
    }

    internal class SlowDown_OperatorCalculator_VarFactor_NoPhaseTracking : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _factorCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _currentDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        public SlowDown_OperatorCalculator_VarFactor_NoPhaseTracking(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase factorCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator,
                factorCalculator
            })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(factorCalculator, () => factorCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

            _signalCalculator = signalCalculator;
            _factorCalculator = factorCalculator;
            _dimensionStack = dimensionStack;
            _currentDimensionStackIndex = dimensionStack.CurrentIndex;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_previousDimensionStackIndex);

            double transformedPosition = GetTransformedPosition(position);

            _dimensionStack.Set(_currentDimensionStackIndex, transformedPosition);

            double result = _signalCalculator.Calculate();

            return result;
        }

        public override void Reset()
        {
            double position = _dimensionStack.Get(_previousDimensionStackIndex);

            double transformedPosition = GetTransformedPosition(position);

            _dimensionStack.Set(_currentDimensionStackIndex, transformedPosition);

            base.Reset();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetTransformedPosition(double position)
        {
            double factor = _factorCalculator.Calculate();

            // IMPORTANT: To stretch things in the output, you have to squash things in the input.
            double phase = position / factor;

            return phase;
        }
    }

    internal class SlowDown_OperatorCalculator_ConstFactor_WithOriginShifting : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _factor;
        private readonly DimensionStack _dimensionStack;
        private readonly int _currentDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        private double _origin;

        public SlowDown_OperatorCalculator_ConstFactor_WithOriginShifting(
            OperatorCalculatorBase signalCalculator,
            double factor,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            if (factor == 0) throw new ZeroException(() => factor);
            if (factor == 1) throw new ZeroException(() => factor);
            if (Double.IsNaN(factor)) throw new NaNException(() => factor);
            if (Double.IsInfinity(factor)) throw new InfinityException(() => factor);
            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

            _signalCalculator = signalCalculator;
            _factor = factor;
            _dimensionStack = dimensionStack;
            _currentDimensionStackIndex = dimensionStack.CurrentIndex;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_previousDimensionStackIndex);

            double transformedPosition = GetTransformedPosition(position);

            _dimensionStack.Set(_currentDimensionStackIndex, transformedPosition);

            double result = _signalCalculator.Calculate();

            return result;
        }

        public override void Reset()
        {
            double position = _dimensionStack.Get(_previousDimensionStackIndex);

            // Origin Shifting
            _origin = position;

            // Dimension Transformation
            double transformedPosition = GetTransformedPosition(position);

            _dimensionStack.Set(_currentDimensionStackIndex, transformedPosition);

            base.Reset();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetTransformedPosition(double position)
        {
            // IMPORTANT: To stretch things in the output, you have to squash things in the input.
            double transformedPosition = (position - _origin) / _factor;

            return transformedPosition;
        }
    }

    internal class SlowDown_OperatorCalculator_ConstFactor_NoOriginShifting : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _factor;
        private readonly DimensionStack _dimensionStack;
        private readonly int _currentDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        public SlowDown_OperatorCalculator_ConstFactor_NoOriginShifting(
            OperatorCalculatorBase signalCalculator,
            double factor,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            if (factor == 0) throw new ZeroException(() => factor);
            if (factor == 1) throw new ZeroException(() => factor);
            if (Double.IsNaN(factor)) throw new NaNException(() => factor);
            if (Double.IsInfinity(factor)) throw new InfinityException(() => factor);
            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

            _signalCalculator = signalCalculator;
            _factor = factor;
            _dimensionStack = dimensionStack;
            _currentDimensionStackIndex = dimensionStack.CurrentIndex;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double transformedPosition = GetTransformedPosition();

            _dimensionStack.Set(_currentDimensionStackIndex, transformedPosition);

            double result = _signalCalculator.Calculate();

            return result;
        }

        public override void Reset()
        {
            double transformedPosition = GetTransformedPosition();

            _dimensionStack.Set(_currentDimensionStackIndex, transformedPosition);

            base.Reset();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetTransformedPosition()
        {
            double position = _dimensionStack.Get(_previousDimensionStackIndex);

            // IMPORTANT: To stretch things in the output, you have to squash things in the input.
            double transformedPosition = position / _factor;
            return transformedPosition;
        }
    }
}
