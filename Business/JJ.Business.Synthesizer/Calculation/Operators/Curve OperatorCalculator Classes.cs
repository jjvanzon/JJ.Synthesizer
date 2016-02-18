using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Calculation.Curves;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Curve_MinTime_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly CurveCalculator_MinTime _curveCalculator;

        public Curve_MinTime_OperatorCalculator(CurveCalculator_MinTime curveCalculator)
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

    internal class Curve_MinTimeZero_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly CurveCalculator_MinTimeZero _curveCalculator;

        public Curve_MinTimeZero_OperatorCalculator(CurveCalculator_MinTimeZero curveCalculator)
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
