using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Curves
{
    internal static class CurveCalculatorFactory
    {
        public static ICurveCalculator CreateCurveCalculator(Curve curve)
        {
            if (curve == null) throw new NullException(() => curve);

            var curveArrayInfo = CurveArrayHelper.ConvertToCurveArrayInfo(curve);

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
