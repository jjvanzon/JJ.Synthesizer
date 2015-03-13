using JJ.Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class TimeDivideWithoutOriginCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _timeDividerCalculator;

        public TimeDivideWithoutOriginCalculator(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase timeDividerCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (timeDividerCalculator == null) throw new NullException(() => timeDividerCalculator);

            _signalCalculator = signalCalculator;
            _timeDividerCalculator = timeDividerCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double timeDivider = _timeDividerCalculator.Calculate(time, channelIndex);

            // Time multiplier 0? See that as multiplier = 1 or rather: just pass through signal.
            if (timeDivider == 0)
            {
                double signal = _signalCalculator.Calculate(time, channelIndex);
                return signal;
            }

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double transformedTime = time * timeDivider;
            double result = _signalCalculator.Calculate(transformedTime, channelIndex);
            return result;
        }
    }
}
