using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Divide_WithConstOrigin_AndDenominator_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _numeratorCalculator;
        private double _denominatorValue;
        private double _originValue;

        public Divide_WithConstOrigin_AndDenominator_OperatorCalculator(
            OperatorCalculatorBase numeratorCalculator, 
            double denominatorValue, 
            double originValue)
        {
            if (numeratorCalculator == null) throw new NullException(() => numeratorCalculator);
            if (numeratorCalculator is Value_OperatorCalculator) throw new Exception("numeratorCalculator cannot be a Value_OperatorCalculator.");
            if (denominatorValue == 0) throw new Exception("denominatorValue cannot be 0.");

            _numeratorCalculator = numeratorCalculator;
            _denominatorValue = denominatorValue;
            _originValue = originValue;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double a = _numeratorCalculator.Calculate(time, channelIndex);
            return (a - _originValue) / _denominatorValue + _originValue;
        }
    }
}