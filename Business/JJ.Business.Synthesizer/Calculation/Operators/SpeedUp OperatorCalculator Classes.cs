using JJ.Framework.Reflection.Exceptions;
using System;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SpeedUp_WithVarFactor_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _factorCalculator;
        private int _dimensionIndex;
        private readonly DimensionStack _dimensionStack;

        private double _phase;
        private double _previousPosition;

        public SpeedUp_WithVarFactor_OperatorCalculator(
            OperatorCalculatorBase signalCalculator, 
            OperatorCalculatorBase factorCalculator,
            DimensionEnum dimensionEnum,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { signalCalculator, factorCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(factorCalculator, () => factorCalculator);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _signalCalculator = signalCalculator;
            _factorCalculator = factorCalculator;
            _dimensionIndex = (int)dimensionEnum;
            _dimensionStack = dimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionIndex);

            _phase = TransformPosition(position);

            _dimensionStack.Push(_dimensionIndex, _phase);

            double result = _signalCalculator.Calculate();

            _dimensionStack.Pop(_dimensionIndex);

            _previousPosition = position;

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double TransformPosition(double position)
        {
            double factor = _factorCalculator.Calculate();

            double positionChange = position - _previousPosition;

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double phase = _phase + positionChange * factor; 

            return phase;
        }

        public override void Reset()
        {
            double position = _dimensionStack.Get(_dimensionIndex);

            _previousPosition = position;
            _phase = 0.0;

            double transformedPosition = TransformPosition(position);

            _dimensionStack.Push(_dimensionIndex, transformedPosition);

            base.Reset();

            _dimensionStack.Pop(_dimensionIndex);
        }
    }

    internal class SpeedUp_WithConstFactor_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _factor;
        private readonly int _dimensionIndex;
        private readonly DimensionStack _dimensionStack;

        public SpeedUp_WithConstFactor_OperatorCalculator(
            OperatorCalculatorBase signalCalculator, 
            double factor,
            DimensionEnum dimensionEnum,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            if (factor == 0) throw new ZeroException(() => factor);
            if (factor == 1) throw new EqualException(() => factor, 1);
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
            double transformedPosition = TransformPosition();

            _dimensionStack.Push(_dimensionIndex, transformedPosition);
            double result = _signalCalculator.Calculate();
            _dimensionStack.Pop(_dimensionIndex);

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double TransformPosition()
        {
            double position = _dimensionStack.Get(_dimensionIndex);

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double transformedPosition = position * _factor;
            return transformedPosition;
        }

        public override void Reset()
        {
            double transformedPosition = TransformPosition();

            _dimensionStack.Push(_dimensionIndex, transformedPosition);

            base.Reset();

            _dimensionStack.Pop(_dimensionIndex);
        }
    }
}
