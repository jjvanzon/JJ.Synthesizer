using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Power_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _baseCalculator;
        private OperatorCalculatorBase _exponentCalculator;

        public Power_OperatorCalculator(OperatorCalculatorBase baseCalculator, OperatorCalculatorBase exponentCalculator)
        {
            if (baseCalculator == null) throw new NullException(() => baseCalculator);
            if (baseCalculator is Value_OperatorCalculator) throw new Exception("baseCalculator cannot be a Value_OperatorCalculator.");
            if (exponentCalculator == null) throw new NullException(() => exponentCalculator);
            if (exponentCalculator is Value_OperatorCalculator) throw new Exception("exponentCalculator cannot be a Value_OperatorCalculator.");

            _baseCalculator = baseCalculator;
            _exponentCalculator = exponentCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double @base = _baseCalculator.Calculate(time, channelIndex);
            double exponent = _exponentCalculator.Calculate(time, channelIndex);
            return Math.Pow(@base, exponent);
        }
    }
}
