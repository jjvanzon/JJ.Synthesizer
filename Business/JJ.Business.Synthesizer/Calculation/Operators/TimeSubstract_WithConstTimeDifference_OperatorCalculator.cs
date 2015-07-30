using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class TimeSubstract_WithConstTimeDifference_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private double _timeDifferenceValue;

        public TimeSubstract_WithConstTimeDifference_OperatorCalculator(
            OperatorCalculatorBase signalCalculator, 
            double timeDifferenceValue)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Value_OperatorCalculator) throw new Exception("signalCalculator cannot be a ValueCalculator.");

            _signalCalculator = signalCalculator;
            _timeDifferenceValue = timeDifferenceValue;
        }

        public override double Calculate(double time, int channelIndex)
        {
            // IMPORTANT: To substract time from the output, you have add time to the input.
            double transformedTime = time + _timeDifferenceValue;
            double result = _signalCalculator.Calculate(transformedTime, channelIndex);
            return result;
        }
    }
}
