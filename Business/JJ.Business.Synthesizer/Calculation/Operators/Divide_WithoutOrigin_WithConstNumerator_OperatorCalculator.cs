using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Divide_WithoutOrigin_WithConstNumerator_OperatorCalculator : OperatorCalculatorBase
    {
        private double _numeratorValue;
        private OperatorCalculatorBase _denominatorCalculator;

        public Divide_WithoutOrigin_WithConstNumerator_OperatorCalculator(double numeratorValue, OperatorCalculatorBase denominatorCalculator)
        {
            if (denominatorCalculator == null) throw new NullException(() => denominatorCalculator);
            if (denominatorCalculator is Value_OperatorCalculator) throw new Exception("denominatorCalculator cannot be a Value_OperatorCalculator.");

            _numeratorValue = numeratorValue;
            _denominatorCalculator = denominatorCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double denominator = _denominatorCalculator.Calculate(time, channelIndex);

            if (denominator == 0)
            {
                return _numeratorValue;
            }

            return _numeratorValue / denominator;
        }
    }
}
