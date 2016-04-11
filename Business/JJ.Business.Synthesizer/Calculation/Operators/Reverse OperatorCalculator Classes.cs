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

        private double _phase;
        private double _previousTime;

        public Reverse_WithVarSpeed_OperatorCalculator(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase speedCalculator)
            : base(new OperatorCalculatorBase[] { signalCalculator, speedCalculator })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => signalCalculator);
            if (speedCalculator == null) throw new NullException(() => speedCalculator);
            if (speedCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => speedCalculator);

            _signalCalculator = signalCalculator;
            _speedCalculator = speedCalculator;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double time = dimensionStack.Get(DimensionEnum.Time);

            _phase = TransformTime(dimensionStack);

            dimensionStack.Push(DimensionEnum.Time, _phase);
            double result = _signalCalculator.Calculate(dimensionStack);
            dimensionStack.Pop(DimensionEnum.Time);

            _previousTime = time;

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double TransformTime(DimensionStack dimensionStack)
        {
            double time = dimensionStack.Get(DimensionEnum.Time);

            double speed = _speedCalculator.Calculate(dimensionStack);

            double dt = time - _previousTime;
            double phase = _phase - dt * speed; // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.

            // Prevent phase from becoming a special number, rendering it unusable forever.
            if (Double.IsNaN(phase) || Double.IsInfinity(phase))
            {
                return Double.NaN;
            }

            return phase;
        }

        public override void Reset(DimensionStack dimensionStack)
        {
            double time = dimensionStack.Get(DimensionEnum.Time);

            _previousTime = time;
            _phase = 0.0;

            double transformedTime = TransformTime(dimensionStack);

            dimensionStack.Push(DimensionEnum.Time, transformedTime);
            base.Reset(dimensionStack);
            dimensionStack.Pop(DimensionEnum.Time);
        }
    }

    internal class Reverse_WithConstSpeed_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _timeSpeed;

        public Reverse_WithConstSpeed_OperatorCalculator(OperatorCalculatorBase signalCalculator, double speedValue)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => signalCalculator);
            if (speedValue == 0) throw new ZeroException(() => speedValue);
            if (Double.IsNaN(speedValue)) throw new NaNException(() => speedValue);
            if (Double.IsInfinity(speedValue)) throw new InfinityException(() => speedValue);

            _signalCalculator = signalCalculator;
            _timeSpeed = -speedValue;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double transformedTime = TransformTime(dimensionStack);

            dimensionStack.Push(DimensionEnum.Time, transformedTime);
            double result = _signalCalculator.Calculate(dimensionStack);
            dimensionStack.Pop(DimensionEnum.Time);

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double TransformTime(DimensionStack dimensionStack)
        {
            double time = dimensionStack.Get(DimensionEnum.Time);

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double transformedTime = time * _timeSpeed;

            return transformedTime;
        }

        public override void Reset(DimensionStack dimensionStack)
        {
            double tranformedTimed = TransformTime(dimensionStack);

            dimensionStack.Push(DimensionEnum.Time, tranformedTimed);
            base.Reset(dimensionStack);
            dimensionStack.Pop(DimensionEnum.Time);
        }
    }
}
