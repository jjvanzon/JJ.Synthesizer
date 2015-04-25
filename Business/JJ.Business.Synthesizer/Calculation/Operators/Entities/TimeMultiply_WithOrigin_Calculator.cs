using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    internal class TimeMultiply_WithOrigin_Calculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _timeMultiplierCalculator;
        private OperatorCalculatorBase _originOutletCalculator;

        public TimeMultiply_WithOrigin_Calculator(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase timeMultiplierCalculator, OperatorCalculatorBase originOutletCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Value_Calculator) throw new Exception("signalCalculator cannot be a Value_Calculator.");
            if (timeMultiplierCalculator == null) throw new NullException(() => timeMultiplierCalculator);
            if (timeMultiplierCalculator is Value_Calculator) throw new Exception("timeMultiplierCalculator cannot be a Value_Calculator.");
            if (originOutletCalculator == null) throw new NullException(() => originOutletCalculator);
            if (originOutletCalculator is Value_Calculator) throw new Exception("originOutletCalculator cannot be a ValueCalculator.");

            _signalCalculator = signalCalculator;
            _timeMultiplierCalculator = timeMultiplierCalculator;
            _originOutletCalculator = originOutletCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double timeMultiplier = _timeMultiplierCalculator.Calculate(time, channelIndex);
            if (timeMultiplier == 0)
            {
                double result = _signalCalculator.Calculate(time, channelIndex);
                return result;
            }
            else
            {
                // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
                double origin = _originOutletCalculator.Calculate(time, channelIndex);
                double transformedTime = (time - origin) / timeMultiplier + origin;
                double result = _signalCalculator.Calculate(transformedTime, channelIndex);
                return result;
            }
        }
    }
}
