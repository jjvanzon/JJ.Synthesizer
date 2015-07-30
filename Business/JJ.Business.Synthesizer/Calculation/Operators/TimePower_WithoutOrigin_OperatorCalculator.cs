using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class TimePower_WithoutOrigin_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _exponentCalculator;

        public TimePower_WithoutOrigin_OperatorCalculator(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase exponentCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            //if (signalCalculator is Value_Calculator) throw new Exception("signalCalculator cannot be a ValueCalculator.");
            if (exponentCalculator == null) throw new NullException(() => exponentCalculator);
            //if (exponentCalculator is Value_Calculator) throw new Exception("exponentCalculator cannot be a ValueCalculator.");

            _signalCalculator = signalCalculator;
            _exponentCalculator = exponentCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            // IMPORTANT: 

            // To increase time in the output, you have to decrease time of the input. 
            // That is why the reciprocal of the exponent is used.

            // Furthermore, you can not use a fractional exponent on a negative number.
            // Time can be negative, that is why the sign is taken off the time 
            // before taking the power and then added to it again after taking the power.

            // (time: -4, exponent: 2) => -1 * Pow(4, 1/2)
            double timeAbs = Math.Abs(time);
            double timeSign = Math.Sign(time);

            double exponent = _exponentCalculator.Calculate(time, channelIndex);

            double transformedTime = timeSign * Math.Pow(timeAbs, 1 / exponent);

            double result = _signalCalculator.Calculate(transformedTime, channelIndex);
            return result;
        }
    }
}
