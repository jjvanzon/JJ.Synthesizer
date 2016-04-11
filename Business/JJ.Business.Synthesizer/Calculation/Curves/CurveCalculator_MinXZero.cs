using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Curves
{
    internal class CurveCalculator_MinXZero : ICurveCalculator
    {
        private ArrayCalculator_MinTimeZero_Line _arrayCalculator;

        public CurveCalculator_MinXZero(CurveArrayInfo curveArrayInfo)
        {
            if (curveArrayInfo == null) throw new NullException(() => curveArrayInfo);

            _arrayCalculator = new ArrayCalculator_MinTimeZero_Line(
                curveArrayInfo.Array,
                curveArrayInfo.Rate,
                curveArrayInfo.YBefore,
                curveArrayInfo.YAfter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double CalculateY(double time)
        {
            return _arrayCalculator.CalculateValue(time);
        }
    }
}
