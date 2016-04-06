using JJ.Framework.Reflection.Exceptions;
using System;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SpeedUp_WithVarFactor_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _factorCalculator;

        private double _phase;
        private double _previousTime;

        private double _origin;

        public SpeedUp_WithVarFactor_OperatorCalculator(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase factorCalculator)
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
            double phase = TransformTime(time, channelIndex);

            _phase = phase;

            double result = _signalCalculator.Calculate(_phase, channelIndex);

            _previousTime = time;

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double TransformTime(double time, int channelIndex)
        {
            double factor = _factorCalculator.Calculate(time, channelIndex);

            double dt = time - _previousTime;
            double phase = _phase + dt * factor; // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.

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

    internal class SpeedUp_WithConstFactor_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _factor;

        private double _origin;

        public SpeedUp_WithConstFactor_OperatorCalculator(OperatorCalculatorBase signalCalculator, double factor)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => signalCalculator);
            if (factor == 0) throw new ZeroException(() => factor);
            if (factor == 1) throw new EqualException(() => factor, 1);
            if (Double.IsNaN(factor)) throw new NaNException(() => factor);
            if (Double.IsInfinity(factor)) throw new InfinityException(() => factor);

            _signalCalculator = signalCalculator;
            _factor = factor;
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
            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double transformedTime = time * _factor;
            return transformedTime;
        }

        public override void Reset(double time, int channelIndex)
        {
            double transformedTime = TransformTime(time, channelIndex);

            base.Reset(transformedTime, channelIndex);
        }
    }
}
