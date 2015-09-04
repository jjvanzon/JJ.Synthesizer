using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class CurveIn_OperatorCalculator : OperatorCalculatorBase
    {
        private CurveCalculator _curveCalculator;

        public CurveIn_OperatorCalculator(Curve curve)
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
