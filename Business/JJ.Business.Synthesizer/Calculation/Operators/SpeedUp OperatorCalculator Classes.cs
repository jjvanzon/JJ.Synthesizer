using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SpeedUp_WithVarTimeDivider_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _timeDividerCalculator;

        private double _phase;
        private double _previousTime;

        public SpeedUp_WithVarTimeDivider_OperatorCalculator(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase timeDividerCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => signalCalculator);
            if (timeDividerCalculator == null) throw new NullException(() => timeDividerCalculator);
            if (timeDividerCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => timeDividerCalculator);

            _signalCalculator = signalCalculator;
            _timeDividerCalculator = timeDividerCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double timeDivider = _timeDividerCalculator.Calculate(time, channelIndex);

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double dt = time - _previousTime;
            _phase = _phase + dt * timeDivider;

            double result = _signalCalculator.Calculate(_phase, channelIndex);

            _previousTime = time;

            return result;
        }
    }

    internal class SpeedUp_WithConstTimeDivider_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private double _timeDividerValue;

        public SpeedUp_WithConstTimeDivider_OperatorCalculator(OperatorCalculatorBase signalCalculator, double timeDividerValue)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => signalCalculator);
            if (timeDividerValue == 0) throw new ZeroException(() => timeDividerValue);

            _signalCalculator = signalCalculator;
            _timeDividerValue = timeDividerValue;
        }

        public override double Calculate(double time, int channelIndex)
        {
            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double transformedTime = time * _timeDividerValue;
            double result = _signalCalculator.Calculate(transformedTime, channelIndex);
            return result;
        }
    }
}
