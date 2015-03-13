using JJ.Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    internal class TimeSubstractCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _timeDifferenceCalculator;

        public TimeSubstractCalculator(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase timeDifferenceCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (timeDifferenceCalculator == null) throw new NullException(() => timeDifferenceCalculator);

            _signalCalculator = signalCalculator;
            _timeDifferenceCalculator = timeDifferenceCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double timeDifference = _timeDifferenceCalculator.Calculate(time, channelIndex);
            // IMPORTANT: To substract time from the output, you have add time to the input.
            double transformedTime = time + timeDifference;
            double result = _signalCalculator.Calculate(transformedTime, channelIndex);
            return result;
        }
    }
}
