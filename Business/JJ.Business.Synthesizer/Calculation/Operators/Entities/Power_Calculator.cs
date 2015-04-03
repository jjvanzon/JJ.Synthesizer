using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    internal class Power_Calculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _baseCalculator;
        private OperatorCalculatorBase _exponentCalculator;

        public Power_Calculator(OperatorCalculatorBase baseCalculator, OperatorCalculatorBase exponentCalculator)
        {
            if (baseCalculator == null) throw new NullException(() => baseCalculator);
            if (baseCalculator is Value_Calculator) throw new Exception("baseCalculator cannot be a Value_Calculator.");
            if (exponentCalculator == null) throw new NullException(() => exponentCalculator);
            if (exponentCalculator is Value_Calculator) throw new Exception("exponentCalculator cannot be a Value_Calculator.");

            _baseCalculator = baseCalculator;
            _exponentCalculator = exponentCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double @base = _baseCalculator.Calculate(time, channelIndex);
            double exponent = _exponentCalculator.Calculate(time, channelIndex);
            return Math.Pow(@base, exponent);
        }
    }
}
