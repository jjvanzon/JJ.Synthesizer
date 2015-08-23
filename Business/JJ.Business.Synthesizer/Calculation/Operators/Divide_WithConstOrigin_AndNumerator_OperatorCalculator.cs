using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Divide_WithConstOrigin_AndNumerator_OperatorCalculator : OperatorCalculatorBase
    {
        private double _numeratorValue;
        private OperatorCalculatorBase _denominatorCalculator;
        private double _originValue;

        public Divide_WithConstOrigin_AndNumerator_OperatorCalculator(
            double numeratorValue, 
            OperatorCalculatorBase denominatorCalculator, 
            double originValue)
        {
            if (denominatorCalculator == null) throw new NullException(() => denominatorCalculator);
            if (denominatorCalculator is Value_OperatorCalculator) throw new Exception("denominatorCalculator cannot be a Value_OperatorCalculator.");

            _numeratorValue = numeratorValue;
            _denominatorCalculator = denominatorCalculator;
            _originValue = originValue;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double denominator = _denominatorCalculator.Calculate(time, channelIndex);

            if (denominator == 0)
            {
                return _numeratorValue;
            }

            return (_numeratorValue - _originValue) / denominator + _originValue;
        }
    }
}