using JJ.Framework.Reflection.Exceptions;
using System;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SlowDown_WithVarFactor_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _factorCalculator;

        private double _phase;
        private double _previousTime;

        public SlowDown_WithVarFactor_OperatorCalculator(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase factorCalculator)
            : base(new OperatorCalculatorBase[] { signalCalculator, factorCalculator })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => signalCalculator);
            if (factorCalculator == null) throw new NullException(() => factorCalculator);
            if (factorCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => factorCalculator);

            _signalCalculator = signalCalculator;
            _factorCalculator = factorCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            _phase = TransformTime(time, channelIndex);

            double result = _signalCalculator.Calculate(_phase, channelIndex);

            _previousTime = time;

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double TransformTime(double time, int channelIndex)
        {
            double factor = _factorCalculator.Calculate(time, channelIndex);

            // IMPORTANT: To multiply the time in the output, you have to divide the time of the input.
            double dt = time - _previousTime;
            double phase = _phase + dt / factor;

            // Prevent phase from becoming a special number, rendering it unusable forever.
            if (Double.IsNaN(phase) || Double.IsInfinity(phase))
            {
                return Double.NaN;
            }

            return phase;
        }

        public override void Reset(double time, int channelIndex)
        {
            _previousTime = time;
            _phase = 0.0;

            double transformedTime = TransformTime(time, channelIndex);
            base.Reset(transformedTime, channelIndex);
        }
    }

    internal class SlowDown_WithConstFactor_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _factorValue;

        public SlowDown_WithConstFactor_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            double factorValue)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => signalCalculator);
            if (factorValue == 0) throw new ZeroException(() => factorValue);
            if (Double.IsNaN(factorValue)) throw new NaNException(() => factorValue);
            if (Double.IsInfinity(factorValue)) throw new InfinityException(() => factorValue);

            _signalCalculator = signalCalculator;
            _factorValue = factorValue;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double transformedTime = TransformTime(time, channelIndex);
            double result = _signalCalculator.Calculate(transformedTime, channelIndex);
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double TransformTime(double time, int channelIndex)
        {
            // IMPORTANT: To multiply the time in the output, you have to divide the time of the input.
            double transformedTime = time / _factorValue;
            return transformedTime;
        }

        public override void Reset(double time, int channelIndex)
        {
            double transformedTime = TransformTime(time, channelIndex);
            base.Reset(transformedTime, channelIndex);
        }
    }
}
