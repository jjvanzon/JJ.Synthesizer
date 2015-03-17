using JJ.Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    internal class TimeMultiplyWithOriginCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _timeMultiplierCalculator;
        private OperatorCalculatorBase _originOutletCalculator;

        public TimeMultiplyWithOriginCalculator(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase timeMultiplierCalculator, OperatorCalculatorBase originOutletCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (timeMultiplierCalculator == null) throw new NullException(() => timeMultiplierCalculator);
            if (originOutletCalculator == null) throw new NullException(() => originOutletCalculator);
            if (signalCalculator is ValueCalculator) throw new Exception("signalCalculator cannot be a ValueCalculator.");

            _signalCalculator = signalCalculator;
            _timeMultiplierCalculator = timeMultiplierCalculator;
            _originOutletCalculator = originOutletCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double timeMultiplier = _timeMultiplierCalculator.Calculate(time, channelIndex);

            // IMPORTANT: To multiply the time in the output, you have to divide the time of the input.
            double origin = _originOutletCalculator.Calculate(time, channelIndex);
            double transformedTime = (time - origin) / timeMultiplier + origin;
            double result2 = _signalCalculator.Calculate(transformedTime, channelIndex);
            return result2;
        }
    }
}
