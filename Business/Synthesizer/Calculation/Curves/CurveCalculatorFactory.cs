using JJ.Data.Synthesizer;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Curves
{
    internal static class CurveCalculatorFactory
    {
        public static ICalculatorWithPosition CreateCurveCalculator(Curve curve)
        {
            if (curve == null) throw new NullException(() => curve);

            var curveArrayInfo = CurveArrayHelper.ConvertToCurveArrayInfo(curve);

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (curveArrayInfo.MinX == 0.0)
            {
                return new CurveCalculator_MinXZero(curveArrayInfo);
            }
            else
            {
                return new CurveCalculator_MinX(curveArrayInfo);
            }
        }
    }
}
