using JJ.Framework.Reflection.Exceptions;
using System;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SlowDown_WithVarFactor_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _factorCalculator;
        private readonly int _dimensionIndex;
        private readonly DimensionStacks _dimensionStack;

        private double _phase;
        private double _previousPosition;

        public SlowDown_WithVarFactor_OperatorCalculator(
            OperatorCalculatorBase signalCalculator, 
            OperatorCalculatorBase factorCalculator,
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack)
            : base(new OperatorCalculatorBase[] 
            {
                signalCalculator,
                factorCalculator
            })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(factorCalculator, () => factorCalculator);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);

            _signalCalculator = signalCalculator;
            _factorCalculator = factorCalculator;
            _dimensionIndex = (int)dimensionEnum;
            _dimensionStack = dimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionIndex);

            _phase = GetTransformedPosition(position);

            _dimensionStack.Push(_dimensionIndex, _phase);

            double result = _signalCalculator.Calculate();

            _dimensionStack.Pop(_dimensionIndex);

            _previousPosition = position;

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetTransformedPosition(double position)
        {
            double factor = _factorCalculator.Calculate();

            // IMPORTANT: To multiply the time in the output, you have to divide the time of the input.
            double positionChange = position - _previousPosition;
            double phase = _phase + positionChange / factor;

            return phase;
        }

        public override void Reset()
        {
            double position = _dimensionStack.Get(_dimensionIndex);

            _previousPosition = position;
            _phase = 0.0;

            double transformedPosition = GetTransformedPosition(position);

            _dimensionStack.Push(_dimensionIndex, transformedPosition);

            base.Reset();

            _dimensionStack.Pop(_dimensionIndex);
        }
    }

    internal class SlowDown_WithConstFactor_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _factor;
        private readonly int _dimensionIndex;
        private readonly DimensionStacks _dimensionStack;

        public SlowDown_WithConstFactor_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            double factor,
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            if (factor == 0) throw new ZeroException(() => factor);
            if (Double.IsNaN(factor)) throw new NaNException(() => factor);
            if (Double.IsInfinity(factor)) throw new InfinityException(() => factor);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _signalCalculator = signalCalculator;
            _factor = factor;
            _dimensionIndex = (int)dimensionEnum;
            _dimensionStack = dimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double transformedPosition = GetTransformedPosition();

            _dimensionStack.Push(_dimensionIndex, transformedPosition);

            double result = _signalCalculator.Calculate();

            _dimensionStack.Pop(_dimensionIndex);

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetTransformedPosition()
        {
            double position = _dimensionStack.Get(_dimensionIndex);

            // IMPORTANT: To multiply the time in the output, you have to divide the time of the input.
            double transformedPosition = position / _factor;
            return transformedPosition;
        }

        public override void Reset()
        {
            double transformedPosition = GetTransformedPosition();

            _dimensionStack.Push(_dimensionIndex, transformedPosition);

            base.Reset();

            _dimensionStack.Pop(_dimensionIndex);
        }
    }
}
