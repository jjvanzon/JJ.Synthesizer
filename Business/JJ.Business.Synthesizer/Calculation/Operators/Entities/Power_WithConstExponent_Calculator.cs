using JJ.Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    internal class Power_WithConstExponent_Calculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _baseCalculator;
        private double _exponentValue;

        public Power_WithConstExponent_Calculator(OperatorCalculatorBase baseCalculator, double exponentValue)
        {
            if (baseCalculator == null) throw new NullException(() => baseCalculator);
            if (baseCalculator is Value_Calculator) throw new Exception("baseCalculator cannot be a Value_Calculator.");

            _baseCalculator = baseCalculator;
            _exponentValue = exponentValue;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double @base = _baseCalculator.Calculate(time, channelIndex);
            return Math.Pow(@base, _exponentValue);
        }
    }
}
