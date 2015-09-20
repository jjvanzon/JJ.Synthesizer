using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SpeedUp_WithOrigin_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _timeDividerCalculator;
        private OperatorCalculatorBase _originCalculator;

        public SpeedUp_WithOrigin_OperatorCalculator(
            OperatorCalculatorBase signalCalculator, 
            OperatorCalculatorBase timeDividerCalculator, 
            OperatorCalculatorBase originCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Value_OperatorCalculator) throw new Exception("signalCalculator cannot be a Value_OperatorCalculator.");
            if (timeDividerCalculator == null) throw new NullException(() => timeDividerCalculator);
            if (timeDividerCalculator is Value_OperatorCalculator) throw new Exception("timeDividerCalculator cannot be a Value_OperatorCalculator.");
            if (originCalculator == null) throw new NullException(() => originCalculator);
            if (originCalculator is Value_OperatorCalculator) throw new Exception("originCalculator cannot be a ValueCalculator.");

            _signalCalculator = signalCalculator;
            _timeDividerCalculator = timeDividerCalculator;
            _originCalculator = originCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double timeDivider = _timeDividerCalculator.Calculate(time, channelIndex);

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double origin = _originCalculator.Calculate(time, channelIndex);
            double transformedTime = (time - origin) * timeDivider + origin;
            double result2 = _signalCalculator.Calculate(transformedTime, channelIndex);
            return result2;
        }
    }
}
