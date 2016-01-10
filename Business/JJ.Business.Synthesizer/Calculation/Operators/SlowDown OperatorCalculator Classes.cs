using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SlowDown_WithVarFactor_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators_WithPhaseTracking
    {
        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _factorCalculator;

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
            double factor = _factorCalculator.Calculate(time, channelIndex);

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double dt = time - _previousTime;
            double phase = _phase + dt / factor;

            // Prevent phase from becoming a special number, rendering it unusable forever.
            if (Double.IsNaN(phase) || Double.IsInfinity(phase))
            {
                return Double.NaN;
            }

            double result = _signalCalculator.Calculate(_phase, channelIndex);

            _previousTime = time;
            _phase = phase;

            return result;
        }
    }

    internal class SlowDown_WithConstFactor_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private OperatorCalculatorBase _signalCalculator;
        private double _factorValue;

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
            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double transformedTime = time / _factorValue; 
            double result = _signalCalculator.Calculate(transformedTime, channelIndex);
            return result;
        }
    }
}
