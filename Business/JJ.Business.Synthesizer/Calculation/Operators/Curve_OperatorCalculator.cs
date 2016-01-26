using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Calculation.Curves;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Curve_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly OptimizedCurveCalculator _curveCalculator;

        public Curve_OperatorCalculator(ICurveCalculator curveCalculator)
        {
            if (curveCalculator == null) throw new NullException(() => curveCalculator);

            OptimizedCurveCalculator optimizedCurveCalculator = curveCalculator as OptimizedCurveCalculator;

            if (optimizedCurveCalculator == null)
            {
                throw new IsNotTypeException<OptimizedCurveCalculator>(() => optimizedCurveCalculator);
            }

            _curveCalculator = optimizedCurveCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double result = _curveCalculator.CalculateValue(time);
            return result;
        }
    }
}
