using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Calculation.Curves;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Curve_OperatorCalculator : OperatorCalculatorBase
    {
        private ICurveCalculator _curveCalculator;

        public Curve_OperatorCalculator(Curve curve)
        {
            if (curve == null) throw new NullException(() => curve);

            _curveCalculator = new OptimizedCurveCalculator(curve);
        }

        public override double Calculate(double time, int channelIndex)
        {
            double result = _curveCalculator.CalculateValue(time);
            return result;
        }
    }
}
