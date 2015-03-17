using JJ.Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    internal class TimeMultiply_WithoutOrigin_Calculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _timeMultiplierCalculator;

        public TimeMultiply_WithoutOrigin_Calculator(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase timeMultiplierCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Value_Calculator) throw new Exception("signalCalculator cannot be a ValueCalculator.");
            if (timeMultiplierCalculator == null) throw new NullException(() => timeMultiplierCalculator);
            if (timeMultiplierCalculator is Value_Calculator) throw new Exception("timeMultiplierCalculator cannot be a ValueCalculator.");

            _signalCalculator = signalCalculator;
            _timeMultiplierCalculator = timeMultiplierCalculator;
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
                double transformedTime = time / timeMultiplier;
                double result = _signalCalculator.Calculate(transformedTime, channelIndex);
                return result;
            }
        }
    }
}