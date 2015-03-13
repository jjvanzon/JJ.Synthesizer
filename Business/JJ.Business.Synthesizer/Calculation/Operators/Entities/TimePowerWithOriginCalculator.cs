using JJ.Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    internal class TimePowerWithOriginCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _exponentCalculator;
        private OperatorCalculatorBase _originCalculator;

        public TimePowerWithOriginCalculator(
            OperatorCalculatorBase signalCalculator, 
            OperatorCalculatorBase exponentCalculator, 
            OperatorCalculatorBase originCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (exponentCalculator == null) throw new NullException(() => exponentCalculator);
            if (originCalculator == null) throw new NullException(() => originCalculator);

            _signalCalculator = signalCalculator;
            _exponentCalculator = exponentCalculator;
            _originCalculator = originCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            // IMPORTANT: 

            // To increase time in the output, you have to decrease time of the input. 
            // That is why the reciprocal of the exponent is used.

            // Furthermore, you can not use a fractional exponent on a negative number.
            // Time can be negative, that is why the sign is taken off the time 
            // before taking the power and then added to it again after taking the power.

            double origin = _originCalculator.Calculate(time, channelIndex);

            double timeAbs = Math.Abs(time - origin);
            double timeSign = Math.Sign(time - origin);

            double exponent = _exponentCalculator.Calculate(time, channelIndex);

            double transformedTime = timeSign * Math.Pow(timeAbs, 1 / exponent) + origin;

            double result = _signalCalculator.Calculate(transformedTime, channelIndex);
            return result;
        }
    }
}
