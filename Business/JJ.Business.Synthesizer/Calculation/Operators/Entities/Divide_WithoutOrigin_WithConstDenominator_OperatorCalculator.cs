using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    internal class Divide_WithoutOrigin_WithConstDenominator_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _numeratorCalculator;
        private double _denominatorValue;

        public Divide_WithoutOrigin_WithConstDenominator_OperatorCalculator(OperatorCalculatorBase numeratorCalculator, double denominatorValue)
        {
            if (numeratorCalculator == null) throw new NullException(() => numeratorCalculator);
            if (numeratorCalculator is Value_OperatorCalculator) throw new Exception("numeratorCalculator cannot be a Value_Calculator.");
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
