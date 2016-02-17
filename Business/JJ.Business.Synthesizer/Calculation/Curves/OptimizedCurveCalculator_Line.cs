using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Calculation.Curves
{
    internal class OptimizedCurveCalculator_Line : ICurveCalculator
    {
        private ArrayCalculator_MinTime_Line _arrayCalculator;

        public OptimizedCurveCalculator_Line(Curve curve)
        {
            CurveArrayInfo arrayInfo = CurveArrayHelper.GetArrayInfo(curve);

            _arrayCalculator = new ArrayCalculator_MinTime_Line(
                arrayInfo.Array, 
                arrayInfo.Rate,
                arrayInfo.MinTime,
                arrayInfo.ValueBefore,
                arrayInfo.ValueAfter);
        }

        public double CalculateValue(double time)
        {
            return _arrayCalculator.CalculateValue(time);
        }
    }
}
