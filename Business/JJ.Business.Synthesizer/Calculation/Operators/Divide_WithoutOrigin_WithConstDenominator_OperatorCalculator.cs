using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Divide_WithoutOrigin_WithConstDenominator_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _numeratorCalculator;
        private double _denominatorValue;

        public Divide_WithoutOrigin_WithConstDenominator_OperatorCalculator(OperatorCalculatorBase numeratorCalculator, double denominatorValue)
        {
            if (numeratorCalculator == null) throw new NullException(() => numeratorCalculator);
            if (numeratorCalculator is Number_OperatorCalculator) throw new Exception("numeratorCalculator cannot be a Value_OperatorCalculator.");
            if (denominatorValue == 0) throw new Exception("denominatorValue cannot be 0.");

            _numeratorCalculator = numeratorCalculator;
            _denominatorValue = denominatorValue;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double numerator = _numeratorCalculator.Calculate(time, channelIndex);
            return numerator / _denominatorValue;
        }
    }
}
