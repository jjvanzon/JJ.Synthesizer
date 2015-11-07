using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Calculation.Curves;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Curve_OperatorCalculator : OperatorCalculatorBase
    {
        private OptimizedCurveCalculator _curveCalculator;

        public Curve_OperatorCalculator(OptimizedCurveCalculator curveCalculator)
        {
            if (curveCalculator == null) throw new NullException(() => curveCalculator);

            _curveCalculator = curveCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double result = _curveCalculator.CalculateValue(time);
            return result;
        }
    }
}
