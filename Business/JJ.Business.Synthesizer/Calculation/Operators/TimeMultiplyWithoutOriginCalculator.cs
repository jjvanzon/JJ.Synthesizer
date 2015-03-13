using JJ.Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class TimeMultiplyWithoutOriginCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _timeMultiplierCalculator;

        public TimeMultiplyWithoutOriginCalculator(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase timeMultiplierCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (timeMultiplierCalculator == null) throw new NullException(() => timeMultiplierCalculator);

            _signalCalculator = signalCalculator;
            _timeMultiplierCalculator = timeMultiplierCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double timeMultiplier = _timeMultiplierCalculator.Calculate(time, channelIndex);

            // Time multiplier 0? See that as multiplier = 1 or rather: just pass through signal.
            if (timeMultiplier == 0)
            {
                double signal = _signalCalculator.Calculate(time, channelIndex);
                return signal;
            }

            // IMPORTANT: To multiply the time in the output, you have to divide the time of the input.
            double transformedTime = time / timeMultiplier;
            double result = _signalCalculator.Calculate(transformedTime, channelIndex);
            return result;
        }
    }
}
