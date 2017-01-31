using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Data.Synthesizer;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation
{
    internal static class CurveArrayCalculatorFactory
    {
        public static ICalculatorWithPosition CreateCurveArrayCalculator(Curve curve)
        {
            if (curve == null) throw new NullException(() => curve);

            var curveArrayInfo = CurveArrayHelper.ConvertToCurveArrayInfo(curve);

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (curveArrayInfo.MinX == 0.0)
            {
                var arrayCalculator = new ArrayCalculator_MinPositionZero_Line(
                    curveArrayInfo.Array,
                    curveArrayInfo.Rate,
                    curveArrayInfo.YBefore,
                    curveArrayInfo.YAfter);

                return arrayCalculator;
            }
            else
            {
                var arrayCalculator = new ArrayCalculator_MinPosition_Line(
                    curveArrayInfo.Array,
                    curveArrayInfo.Rate,
                    curveArrayInfo.MinX,
                    curveArrayInfo.YBefore,
                    curveArrayInfo.YAfter);

                return arrayCalculator;
            }
        }
    }
}
