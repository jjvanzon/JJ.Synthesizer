using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Curves
{
    internal class CurveCalculator_MinTime : ICurveCalculator
    {
        private ArrayCalculator_MinTime_Line _arrayCalculator;

        public CurveCalculator_MinTime(CurveArrayInfo curveArrayInfo)
        {
            if (curveArrayInfo == null) throw new NullException(() => curveArrayInfo);

            _arrayCalculator = new ArrayCalculator_MinTime_Line(
                curveArrayInfo.Array,
                curveArrayInfo.Rate,
                curveArrayInfo.MinTime,
                curveArrayInfo.ValueBefore,
                curveArrayInfo.ValueAfter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double CalculateValue(double time)
        {
            return _arrayCalculator.CalculateValue(time);
        }
    }
}
