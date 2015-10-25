using System;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Exponent_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _lowCalculator;
        private OperatorCalculatorBase _highCalculator;
        private OperatorCalculatorBase _ratioCalculator;

        public Exponent_OperatorCalculator(OperatorCalculatorBase lowCalculator, OperatorCalculatorBase highCalculator, OperatorCalculatorBase ratioCalculator)
        {
            if (lowCalculator == null) throw new NullException(() => lowCalculator);
            if (highCalculator == null) throw new NullException(() => highCalculator);
            if (ratioCalculator == null) throw new NullException(() => ratioCalculator);
            // TODO: Enable this code again.
            //if (lowCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => lowCalculator);
            //if (highCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => highCalculator);
            //if (ratioCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => ratioCalculator);

            _lowCalculator = lowCalculator;
            _highCalculator = highCalculator;
            _ratioCalculator = ratioCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double low = _lowCalculator.Calculate(time, channelIndex);
            double high = _highCalculator.Calculate(time, channelIndex);
            double ratio = _ratioCalculator.Calculate(time, channelIndex);
            
            double result = low * Math.Pow(high / low, ratio);
            return result;
        }
    }
}
