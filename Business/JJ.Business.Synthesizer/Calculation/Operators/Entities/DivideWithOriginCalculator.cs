using JJ.Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    internal class DivideWithOriginCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _numeratorCalculator;
        private OperatorCalculatorBase _denominatorCalculator;
        private OperatorCalculatorBase _originCalculator;

        public DivideWithOriginCalculator(
            OperatorCalculatorBase numeratorCalculator, 
            OperatorCalculatorBase denominatorCalculator, 
            OperatorCalculatorBase originCalculator)
        {
            if (numeratorCalculator == null) throw new NullException(() => numeratorCalculator);
            if (denominatorCalculator == null) throw new NullException(() => denominatorCalculator);
            if (originCalculator == null) throw new NullException(() => originCalculator);

            _numeratorCalculator = numeratorCalculator;
            _denominatorCalculator = denominatorCalculator;
            _originCalculator = originCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double origin = _denominatorCalculator.Calculate(time, channelIndex);

            double denominator = _denominatorCalculator.Calculate(time, channelIndex);

            // Do not divide by 0.
            if (denominator == 0) return 0;

            double numerator = _numeratorCalculator.Calculate(time, channelIndex);

            return (numerator - origin) / denominator + origin;
        }
    }
}
