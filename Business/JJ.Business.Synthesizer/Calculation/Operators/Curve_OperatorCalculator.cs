using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Calculation.Curves;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    // TODO: If you want to get rid of the interface indirection,
    // that comes with using ICurveCalculator,
    // you would have to make several subclasses,
    // that each handle a different type of CurveCalculator.

    internal class Curve_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly ICurveCalculator _curveCalculator;

        public Curve_OperatorCalculator(ICurveCalculator curveCalculator)
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
