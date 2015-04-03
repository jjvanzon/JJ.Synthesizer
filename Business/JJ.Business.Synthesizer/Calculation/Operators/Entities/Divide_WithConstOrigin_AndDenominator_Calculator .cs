using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    internal class Divide_WithConstOrigin_AndDenominator_Calculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _numeratorCalculator;
        private double _denominatorValue;
        private double _originValue;

        public Divide_WithConstOrigin_AndDenominator_Calculator(
            OperatorCalculatorBase numeratorCalculator, 
            double denominatorValue, 
            double originValue)
        {
            if (numeratorCalculator == null) throw new NullException(() => numeratorCalculator);
            if (numeratorCalculator is Value_Calculator) throw new Exception("numeratorCalculator cannot be a Value_Calculator.");
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