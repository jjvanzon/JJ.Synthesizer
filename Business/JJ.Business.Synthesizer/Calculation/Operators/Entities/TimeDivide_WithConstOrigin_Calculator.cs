using JJ.Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    internal class TimeDivide_WithConstOrigin_Calculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _timeDividerCalculator;
        private double _originValue;

        public TimeDivide_WithConstOrigin_Calculator(
            OperatorCalculatorBase signalCalculator, 
            OperatorCalculatorBase timeDividerCalculator, 
            double originValue)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Value_Calculator) throw new Exception("signalCalculator cannot be a Value_Calculator.");
            if (timeDividerCalculator == null) throw new NullException(() => timeDividerCalculator);
            if (timeDividerCalculator is Value_Calculator) throw new Exception("timeDividerCalculator cannot be a Value_Calculator.");

            _signalCalculator = signalCalculator;
            _timeDividerCalculator = timeDividerCalculator;
            _originValue = originValue;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double timeDivider = _timeDividerCalculator.Calculate(time, channelIndex);

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double transformedTime = (time - _originValue) * timeDivider + _originValue;
            double result2 = _signalCalculator.Calculate(transformedTime, channelIndex);
            return result2;
        }
    }
}
