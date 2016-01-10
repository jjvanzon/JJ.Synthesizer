using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SlowDown_WithVarTimeMultiplier_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _timeMultiplierCalculator;

        private double _phase;
        private double _previousTime;

        public SlowDown_WithVarTimeMultiplier_OperatorCalculator(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase timeMultiplierCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => signalCalculator);
            if (timeMultiplierCalculator == null) throw new NullException(() => timeMultiplierCalculator);
            if (timeMultiplierCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => timeMultiplierCalculator);

            _signalCalculator = signalCalculator;
            _timeMultiplierCalculator = timeMultiplierCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double timeMultiplier = _timeMultiplierCalculator.Calculate(time, channelIndex);
            if (timeMultiplier != 0)
            {
                // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
                double dt = time - _previousTime;
                _phase = _phase + dt / timeMultiplier;
            }

            double result = _signalCalculator.Calculate(_phase, channelIndex);

            _previousTime = time;

            return result;
        }
    }

    internal class SlowDown_WithConstTimeMultiplier_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private double _timeMultiplierValue;

        public SlowDown_WithConstTimeMultiplier_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            double timeMultiplierValue)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => signalCalculator);
            if (timeMultiplierValue == 0) throw new ZeroException(() => timeMultiplierValue);

            _signalCalculator = signalCalculator;
            _timeMultiplierValue = timeMultiplierValue;
        }

        public override double Calculate(double time, int channelIndex)
        {
            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double transformedTime = time / _timeMultiplierValue;
            double result = _signalCalculator.Calculate(transformedTime, channelIndex);
            return result;
        }
    }
}
