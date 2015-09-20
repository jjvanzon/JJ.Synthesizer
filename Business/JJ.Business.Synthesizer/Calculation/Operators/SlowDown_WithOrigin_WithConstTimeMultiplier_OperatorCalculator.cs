using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SlowDown_WithOrigin_WithConstTimeMultiplier_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private double _timeMultiplierValue;
        private OperatorCalculatorBase _originCalculator;

        public SlowDown_WithOrigin_WithConstTimeMultiplier_OperatorCalculator(
            OperatorCalculatorBase signalCalculator, 
            double timeMultiplierValue, 
            OperatorCalculatorBase originCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new Exception("signal cannot be a ValueCalculator.");
            if (timeMultiplierValue == 0) throw new Exception("timeMultiplierValue cannot be 0.");
            if (originCalculator == null) throw new NullException(() => originCalculator);
            if (originCalculator is Number_OperatorCalculator) throw new Exception("originCalculator cannot be a ValueCalculator.");

            _signalCalculator = signalCalculator;
            _timeMultiplierValue = timeMultiplierValue;
            _originCalculator = originCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double origin = _originCalculator.Calculate(time, channelIndex);
            double transformedTime = (time - origin) / _timeMultiplierValue + origin;
            double result2 = _signalCalculator.Calculate(transformedTime, channelIndex);
            return result2;
        }
    }
}
