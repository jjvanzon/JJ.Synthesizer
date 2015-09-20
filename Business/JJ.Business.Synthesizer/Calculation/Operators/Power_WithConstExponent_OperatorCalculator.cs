using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Power_WithConstExponent_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _baseCalculator;
        private double _exponentValue;

        public Power_WithConstExponent_OperatorCalculator(OperatorCalculatorBase baseCalculator, double exponentValue)
        {
            if (baseCalculator == null) throw new NullException(() => baseCalculator);
            if (baseCalculator is Number_OperatorCalculator) throw new Exception("baseCalculator cannot be a Value_OperatorCalculator.");

            _baseCalculator = baseCalculator;
            _exponentValue = exponentValue;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double @base = _baseCalculator.Calculate(time, channelIndex);
            return Math.Pow(@base, _exponentValue);
        }
    }
}
