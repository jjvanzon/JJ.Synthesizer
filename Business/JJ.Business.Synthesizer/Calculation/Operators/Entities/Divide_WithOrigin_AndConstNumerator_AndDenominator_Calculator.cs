using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    internal class Divide_WithOrigin_AndConstNumerator_AndDenominator_Calculator : OperatorCalculatorBase
    {
        private double _numeratorValue;
        private double _denominatorValue;
        private OperatorCalculatorBase _originCalculator;

        public Divide_WithOrigin_AndConstNumerator_AndDenominator_Calculator(
            double numeratorValue, 
            double denominatorValue, 
            OperatorCalculatorBase originCalculator)
        {
            if (denominatorValue == 0) throw new Exception("denominatorValue cannot be 0.");
            if (originCalculator == null) throw new NullException(() => originCalculator);
            if (originCalculator is Value_Calculator) throw new Exception("originCalculator cannot be a Value_Calculator.");

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