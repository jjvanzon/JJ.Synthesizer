using JJ.Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class PowerCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _baseCalculator;
        private OperatorCalculatorBase _exponentCalculator;

        public PowerCalculator(OperatorCalculatorBase baseCalculator, OperatorCalculatorBase exponentCalculator)
        {
            if (baseCalculator == null) throw new NullException(() => baseCalculator);
            if (exponentCalculator == null) throw new NullException(() => exponentCalculator);

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
