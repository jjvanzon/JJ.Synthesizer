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

            var curveArrayInfo = CurveArrayHelper.GetCurveArrayInfo(curve);

            if (curveArrayInfo.MinTime == 0.0)
            {
                return new CurveCalculator_MinTimeZero(curveArrayInfo);
            }
            else
            {
                return new CurveCalculator_MinTime(curveArrayInfo);
            }
        }
    }
}
