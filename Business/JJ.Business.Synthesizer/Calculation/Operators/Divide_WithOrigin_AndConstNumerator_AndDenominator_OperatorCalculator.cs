using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Divide_WithOrigin_AndConstNumerator_AndDenominator_OperatorCalculator : OperatorCalculatorBase
    {
        private double _numeratorValue;
        private double _denominatorValue;
        private OperatorCalculatorBase _originCalculator;

        public Divide_WithOrigin_AndConstNumerator_AndDenominator_OperatorCalculator(
            double numeratorValue, 
            double denominatorValue, 
            OperatorCalculatorBase originCalculator)
        {
            if (denominatorValue == 0) throw new Exception("denominatorValue cannot be 0.");
            if (originCalculator == null) throw new NullException(() => originCalculator);
            if (originCalculator is Number_OperatorCalculator) throw new Exception("originCalculator cannot be a Value_OperatorCalculator.");

            _numeratorValue = numeratorValue;
            _denominatorValue = denominatorValue;
            _originCalculator = originCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double origin = _originCalculator.Calculate(time, channelIndex);
            return (_numeratorValue - origin) / _denominatorValue + origin;
        }
    }
}