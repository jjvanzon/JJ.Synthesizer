using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class TimeAdd_WithConstTimeDifference_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private double _timeDifferenceValue;

        public TimeAdd_WithConstTimeDifference_OperatorCalculator(
            OperatorCalculatorBase signalCalculator, 
            double timeDifferenceValue)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Value_OperatorCalculator) throw new Exception("signalCalculator cannot be a Value_OperatorCalculator.");

            _signalCalculator = signalCalculator;
            _timeDifferenceValue = timeDifferenceValue;
        }

        public override double Calculate(double time, int channelIndex)
        {
            // IMPORTANT: To add time to the output, you have substract time from the input.
            double transformedTime = time - _timeDifferenceValue;
            double result = _signalCalculator.Calculate(transformedTime, channelIndex);
            return result;
        }
    }
}
