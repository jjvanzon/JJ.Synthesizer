using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Power_WithConstBase_OperatorCalculator : OperatorCalculatorBase
    {
        private double _baseValue;
        private OperatorCalculatorBase _exponentCalculator;

        public Power_WithConstBase_OperatorCalculator(double baseValue, OperatorCalculatorBase exponentCalculator)
        {
            if (exponentCalculator == null) throw new NullException(() => exponentCalculator);
            if (exponentCalculator is Number_OperatorCalculator) throw new Exception("exponentCalculator cannot be a Value_OperatorCalculator.");

            _baseValue = baseValue;
            _exponentCalculator = exponentCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double exponent = _exponentCalculator.Calculate(time, channelIndex);
            return Math.Pow(_baseValue, exponent);
        }
    }
}
