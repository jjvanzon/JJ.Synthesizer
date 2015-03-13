using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    internal class CurveInCalculator : OperatorCalculatorBase
    {
        private CurveCalculator _curveCalculator;

        public CurveInCalculator(Curve curve)
        {
            if (curve == null) throw new NullException(() => curve);

            _curveCalculator = new CurveCalculator(curve);
        }

        public override double Calculate(double time, int channelIndex)
        {
            double result = _curveCalculator.CalculateValue(time);
            return result;
        }
    }
}
