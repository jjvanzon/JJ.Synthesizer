using JJ.Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    internal class Divide_WithOrigin_AndConstNumerator_Calculator : OperatorCalculatorBase
    {
        private double _numeratorValue;
        private OperatorCalculatorBase _denominatorCalculator;
        private OperatorCalculatorBase _originCalculator;

        public Divide_WithOrigin_AndConstNumerator_Calculator(
            double numeratorValue, 
            OperatorCalculatorBase denominatorCalculator, 
            OperatorCalculatorBase originCalculator)
        {
            if (denominatorCalculator == null) throw new NullException(() => denominatorCalculator);
            if (denominatorCalculator is Value_Calculator) throw new Exception("denominatorCalculator cannot be a Value_Calculator.");
            if (originCalculator == null) throw new NullException(() => originCalculator);
            if (originCalculator is Value_Calculator) throw new Exception("originCalculator cannot be a Value_Calculator.");

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