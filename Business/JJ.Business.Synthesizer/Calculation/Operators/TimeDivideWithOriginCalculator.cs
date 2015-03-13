using JJ.Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class TimeDivideWithOriginCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _timeDividerCalculator;
        private OperatorCalculatorBase _originOutletCalculator;

        public TimeDivideWithOriginCalculator(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase timeDividerCalculator, OperatorCalculatorBase originOutletCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (timeDividerCalculator == null) throw new NullException(() => timeDividerCalculator);
            if (originOutletCalculator == null) throw new NullException(() => originOutletCalculator);

            _signalCalculator = signalCalculator;
            _timeDividerCalculator = timeDividerCalculator;
            _originOutletCalculator = originOutletCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double timeDivider = _timeDividerCalculator.Calculate(time, channelIndex);

            // Time divider 0? See that as divider = 1 or rather: just pass through signal.
            if (timeDivider == 0)
            {
                double signal = _signalCalculator.Calculate(time, channelIndex);
                return signal;
            }

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double origin = _originOutletCalculator.Calculate(time, channelIndex);
            double transformedTime = (time - origin) * timeDivider + origin;
            double result2 = _signalCalculator.Calculate(transformedTime, channelIndex);
            return result2;
        }
    }
}
