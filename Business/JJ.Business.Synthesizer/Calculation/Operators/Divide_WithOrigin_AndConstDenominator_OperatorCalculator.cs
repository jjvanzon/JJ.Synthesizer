using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Divide_WithOrigin_AndConstDenominator_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _numeratorCalculator;
        private double _denominatorValue;
        private OperatorCalculatorBase _originCalculator;

        public Divide_WithOrigin_AndConstDenominator_OperatorCalculator(
            OperatorCalculatorBase numeratorCalculator, 
            double denominatorValue, 
            OperatorCalculatorBase originCalculator)
        {
            if (numeratorCalculator == null) throw new NullException(() => numeratorCalculator);
            if (numeratorCalculator is Value_OperatorCalculator) throw new Exception("numeratorCalculator cannot be a Value_OperatorCalculator.");
            if (denominatorValue == 0) throw new Exception("denominatorValue cannot be 0.");
            if (originCalculator == null) throw new NullException(() => originCalculator);
            if (originCalculator is Value_OperatorCalculator) throw new Exception("originCalculator cannot be a Value_OperatorCalculator.");

            _numeratorCalculator = numeratorCalculator;
            _denominatorValue = denominatorValue;
            _originCalculator = originCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double origin = _originCalculator.Calculate(time, channelIndex);
            double a = _numeratorCalculator.Calculate(time, channelIndex);
            return (a - origin) / _denominatorValue + origin;
        }
    }
}