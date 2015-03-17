using JJ.Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    internal class TimeDivide_WithoutOrigin_WithConstTimeDivider_Calculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private double _timeDividerValue;

        public TimeDivide_WithoutOrigin_WithConstTimeDivider_Calculator(OperatorCalculatorBase signalCalculator, double timeDividerValue)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Value_Calculator) throw new Exception("signalCalculator cannot be a Value_Calculator.");
            if (timeDividerValue == 0) throw new Exception("timeDividerValue cannot be 0.");

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
