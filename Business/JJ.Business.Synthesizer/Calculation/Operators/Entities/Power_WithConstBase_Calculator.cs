using JJ.Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    internal class Power_WithConstBase_Calculator : OperatorCalculatorBase
    {
        private double _baseValue;
        private OperatorCalculatorBase _exponentCalculator;

        public Power_WithConstBase_Calculator(double baseValue, OperatorCalculatorBase exponentCalculator)
        {
            if (exponentCalculator == null) throw new NullException(() => exponentCalculator);
            if (exponentCalculator is Value_Calculator) throw new Exception("exponentCalculator cannot be a Value_Calculator.");

            _baseValue = baseValue;
            _exponentCalculator = exponentCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double exponent = _exponentCalculator.Calculate(time, channelIndex);
            return Math.Pow(_baseValue, exponent);
        }
    }
}
