using JJ.Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class DivideWithoutOriginCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _numeratorCalculator;
        private OperatorCalculatorBase _denominatorCalculator;

        public DivideWithoutOriginCalculator(OperatorCalculatorBase numeratorCalculator, OperatorCalculatorBase denominatorCalculator)
        {
            if (numeratorCalculator == null) throw new NullException(() => numeratorCalculator);
            if (denominatorCalculator == null) throw new NullException(() => denominatorCalculator);

            _numeratorCalculator = numeratorCalculator;
            _denominatorCalculator = denominatorCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double d = _denominatorCalculator.Calculate(time, channelIndex);

            // Do not divide by 0.
            if (d == 0) return 0;

            double n = _numeratorCalculator.Calculate(time, channelIndex);

            return n / d;
        }
    }
}
