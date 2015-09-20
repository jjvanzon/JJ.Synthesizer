using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SlowDown_WithConstOrigin_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _timeMultiplierCalculator;
        private double _originValue;

        public SlowDown_WithConstOrigin_OperatorCalculator(
            OperatorCalculatorBase signalCalculator, 
            OperatorCalculatorBase timeMultiplierCalculator, 
            double originValue)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Value_OperatorCalculator) throw new Exception("signalCalculator cannot be a Value_OperatorCalculator.");
            if (timeMultiplierCalculator == null) throw new NullException(() => timeMultiplierCalculator);
            if (timeMultiplierCalculator is Value_OperatorCalculator) throw new Exception("timeMultiplierCalculator cannot be a Value_OperatorCalculator.");

            _signalCalculator = signalCalculator;
            _timeMultiplierCalculator = timeMultiplierCalculator;
            _originValue = originValue;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double timeMultiplier = _timeMultiplierCalculator.Calculate(time, channelIndex);

            if (timeMultiplier == 0)
            {
                double result = _signalCalculator.Calculate(time, channelIndex);
                return result;
            }
            else
            {
                // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
                double transformedTime = (time - _originValue) / timeMultiplier + _originValue;
                double result = _signalCalculator.Calculate(transformedTime, channelIndex);
                return result;
            }
        }
    }
}
