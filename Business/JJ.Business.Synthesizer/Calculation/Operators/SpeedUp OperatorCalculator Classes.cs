using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SpeedUp_WithVarFactor_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _factorCalculator;

        private double _phase;
        private double _previousTime;

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
            double factor = _factorCalculator.Calculate(time, channelIndex);

            double dt = time - _previousTime;
            double phase = _phase + dt * factor; // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.

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

        public override void ResetState()
        {
            _phase = 0.0;
            _previousTime = 0.0;

            base.ResetState();
        }
    }

    internal class SpeedUp_WithConstFactor_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _factorValue;

        public SpeedUp_WithConstFactor_OperatorCalculator(OperatorCalculatorBase signalCalculator, double factorValue)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => signalCalculator);
            if (factorValue == 0) throw new ZeroException(() => factorValue);
            if (factorValue == 1) throw new EqualException(() => factorValue, 1);
            if (Double.IsNaN(factorValue)) throw new NaNException(() => factorValue);
            if (Double.IsInfinity(factorValue)) throw new InfinityException(() => factorValue);

            _signalCalculator = signalCalculator;
            _factorValue = factorValue;
        }

        public override double Calculate(double time, int channelIndex)
        {
            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double transformedTime = time * _factorValue; 
            double result = _signalCalculator.Calculate(transformedTime, channelIndex);
            return result;
        }
    }
}
