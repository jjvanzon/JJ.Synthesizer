using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SpeedUp_WithOrigin_WithConstTimeDivider_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private double _timeDividerValue;
        private OperatorCalculatorBase _originCalculator;

        public SpeedUp_WithOrigin_WithConstTimeDivider_OperatorCalculator(
            OperatorCalculatorBase signalCalculator, 
            double timeDividerValue, 
            OperatorCalculatorBase originCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Value_OperatorCalculator) throw new Exception("signal cannot be a ValueCalculator.");
            if (originCalculator == null) throw new NullException(() => originCalculator);
            if (originCalculator is Value_OperatorCalculator) throw new Exception("originCalculator cannot be a Value_OperatorCalculator.");

            _signalCalculator = signalCalculator;
            _timeDividerValue = timeDividerValue;
            _originCalculator = originCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double origin = _originCalculator.Calculate(time, channelIndex);
            double transformedTime = (time - origin) * _timeDividerValue + origin;
            double result2 = _signalCalculator.Calculate(transformedTime, channelIndex);
            return result2;
        }
    }
}
