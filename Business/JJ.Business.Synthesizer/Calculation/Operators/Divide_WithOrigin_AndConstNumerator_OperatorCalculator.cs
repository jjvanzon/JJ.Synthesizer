using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Divide_WithOrigin_AndConstNumerator_OperatorCalculator : OperatorCalculatorBase
    {
        private double _numeratorValue;
        private OperatorCalculatorBase _denominatorCalculator;
        private OperatorCalculatorBase _originCalculator;

        public Divide_WithOrigin_AndConstNumerator_OperatorCalculator(
            double numeratorValue, 
            OperatorCalculatorBase denominatorCalculator, 
            OperatorCalculatorBase originCalculator)
        {
            if (denominatorCalculator == null) throw new NullException(() => denominatorCalculator);
            if (denominatorCalculator is Number_OperatorCalculator) throw new Exception("denominatorCalculator cannot be a Value_OperatorCalculator.");
            if (originCalculator == null) throw new NullException(() => originCalculator);
            if (originCalculator is Number_OperatorCalculator) throw new Exception("originCalculator cannot be a Value_OperatorCalculator.");

            _numeratorValue = numeratorValue;
            _denominatorCalculator = denominatorCalculator;
            _originCalculator = originCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double denominator = _denominatorCalculator.Calculate(time, channelIndex);

            if (denominator == 0)
            {
                return _numeratorValue;
            }

            double origin = _originCalculator.Calculate(time, channelIndex);

            return (_numeratorValue - origin) / denominator + origin;
        }
    }
}