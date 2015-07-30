using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class TimeDivide_WithoutOrigin_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _timeDividerCalculator;

        public TimeDivide_WithoutOrigin_OperatorCalculator(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase timeDividerCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Value_OperatorCalculator) throw new Exception("signalCalculator cannot be a Value_Calculator.");
            if (timeDividerCalculator == null) throw new NullException(() => timeDividerCalculator);
            if (timeDividerCalculator is Value_OperatorCalculator) throw new Exception("timeDividerCalculator cannot be a Value_Calculator.");

            _signalCalculator = signalCalculator;
            _timeDividerCalculator = timeDividerCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double timeDivider = _timeDividerCalculator.Calculate(time, channelIndex);

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double transformedTime = time * timeDivider;
            double result = _signalCalculator.Calculate(transformedTime, channelIndex);
            return result;
        }
    }
}
