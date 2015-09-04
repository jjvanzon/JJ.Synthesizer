using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class TimeAdd_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _timeDifferenceCalculator;

        public TimeAdd_OperatorCalculator(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase timeDifferenceCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Value_OperatorCalculator) throw new Exception("signalCalculator cannot be a Value_OperatorCalculator.");
            if (timeDifferenceCalculator == null) throw new NullException(() => timeDifferenceCalculator);
            if (timeDifferenceCalculator is Value_OperatorCalculator) throw new Exception("timeDifferenceCalculator cannot be a Value_OperatorCalculator.");

            _signalCalculator = signalCalculator;
            _timeDifferenceCalculator = timeDifferenceCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double timeDifference = _timeDifferenceCalculator.Calculate(time, channelIndex);
            // IMPORTANT: To add time to the output, you have substract time from the input.
            double transformedTime = time - timeDifference;
            double result = _signalCalculator.Calculate(transformedTime, channelIndex);
            return result;
        }
    }
}
