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

        private double _phase;
        private double _previousPosition;

        public SlowDown_WithVarFactor_OperatorCalculator(
            OperatorCalculatorBase signalCalculator, 
            OperatorCalculatorBase factorCalculator,
            DimensionEnum dimensionEnum)
            : base(new OperatorCalculatorBase[] 
            {
                signalCalculator,
                factorCalculator
            })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(factorCalculator, () => factorCalculator);

            _signalCalculator = signalCalculator;
            _factorCalculator = factorCalculator;
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

            double factor = _factorCalculator.Calculate(dimensionStack);

            // IMPORTANT: To multiply the time in the output, you have to divide the time of the input.
            double positionChange = position - _previousPosition;
            double phase = _phase + positionChange / factor;

            // Prevent phase from becoming a special number, rendering it unusable forever.
            if (Double.IsNaN(phase) || Double.IsInfinity(phase))
            {
                return Double.NaN;
            }

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

    internal class SlowDown_WithConstFactor_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _factor;
        private readonly int _dimensionIndex;

        public SlowDown_WithConstFactor_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            double factor,
            DimensionEnum dimensionEnum)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            if (factor == 0) throw new ZeroException(() => factor);
            if (Double.IsNaN(factor)) throw new NaNException(() => factor);
            if (Double.IsInfinity(factor)) throw new InfinityException(() => factor);

            _signalCalculator = signalCalculator;
            _factor = factor;
            _dimensionIndex = (int)dimensionEnum;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double transformedPosition = TransformPosition(dimensionStack);

            dimensionStack.Push(_dimensionIndex, transformedPosition);
            double result = _signalCalculator.Calculate(dimensionStack);
            dimensionStack.Pop(_dimensionIndex);

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double TransformPosition(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            // IMPORTANT: To multiply the time in the output, you have to divide the time of the input.
            double transformedPosition = position / _factor;
            return transformedPosition;
        }

        public override void Reset(DimensionStack dimensionStack)
        {
            double transformedPosition = TransformPosition(dimensionStack);

            dimensionStack.Push(_dimensionIndex, transformedPosition);
            base.Reset(dimensionStack);
            dimensionStack.Pop(_dimensionIndex);
        }
    }
}
