using JJ.Framework.Reflection.Exceptions;
using System;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Reverse_WithVarSpeed_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _speedCalculator;
        private readonly int _dimensionIndex;

        private double _phase;
        private double _previousPosition;

        public Reverse_WithVarSpeed_OperatorCalculator(
            OperatorCalculatorBase signalCalculator, 
            OperatorCalculatorBase speedCalculator,
            DimensionEnum dimensionEnum)
            : base(new OperatorCalculatorBase[] 
            {
                signalCalculator,
                speedCalculator
            })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(speedCalculator, () => speedCalculator);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);

            _signalCalculator = signalCalculator;
            _speedCalculator = speedCalculator;
            _dimensionIndex = (int)dimensionEnum;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            _phase = TransformPosition(dimensionStack);

            dimensionStack.Push(_dimensionIndex, _phase);
            double result = _signalCalculator.Calculate(dimensionStack);
            dimensionStack.Pop(_dimensionIndex);

            _previousPosition = position;

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double TransformPosition(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            double speed = _speedCalculator.Calculate(dimensionStack);

            double positionChange = position - _previousPosition;

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double phase = _phase - positionChange * speed;

            // Prevent phase from becoming a special number, rendering it unusable forever.
            //if (Double.IsNaN(phase) || Double.IsInfinity(phase))
            //{
            //    return Double.NaN;
            //}

            return phase;
        }

        public override void Reset(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            _previousPosition = position;
            _phase = 0.0;

            double transformedPosition = TransformPosition(dimensionStack);

            dimensionStack.Push(_dimensionIndex, transformedPosition);
            base.Reset(dimensionStack);
            dimensionStack.Pop(_dimensionIndex);
        }
    }

    internal class Reverse_WithConstSpeed_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _speed;
        private readonly int _dimensionIndex;

        public Reverse_WithConstSpeed_OperatorCalculator(
            OperatorCalculatorBase signalCalculator, 
            double speed,
            DimensionEnum dimensionEnum)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            if (speed == 0) throw new ZeroException(() => speed);
            if (Double.IsNaN(speed)) throw new NaNException(() => speed);
            if (Double.IsInfinity(speed)) throw new InfinityException(() => speed);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);

            _signalCalculator = signalCalculator;
            _speed = -speed;
            _dimensionIndex = (int)dimensionEnum;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double transformedPosition = TransformPosition(dimensionStack);

            dimensionStack.Push(_dimensionIndex, transformedPosition);
            double value = _signalCalculator.Calculate(dimensionStack);
            dimensionStack.Pop(_dimensionIndex);

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double TransformPosition(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double transformedPosition = position * _speed;

            return transformedPosition;
        }

        public override void Reset(DimensionStack dimensionStack)
        {
            double tranformedPosition = TransformPosition(dimensionStack);

            dimensionStack.Push(_dimensionIndex, tranformedPosition);
            base.Reset(dimensionStack);
            dimensionStack.Pop(_dimensionIndex);
        }
    }
}
