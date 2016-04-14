using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Curves
{
    internal class CurveCalculator_MinX : ICurveCalculator
    {
        private ArrayCalculator_MinPosition_Line _arrayCalculator;

        public CurveCalculator_MinX(CurveArrayInfo curveArrayInfo)
        {
            if (curveArrayInfo == null) throw new NullException(() => curveArrayInfo);

            _arrayCalculator = new ArrayCalculator_MinPosition_Line(
                curveArrayInfo.Array,
                curveArrayInfo.Rate,
                curveArrayInfo.MinX,
                curveArrayInfo.YBefore,
                curveArrayInfo.YAfter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double CalculateY(double x)
        {
            return _arrayCalculator.CalculateValue(x);
        }
    }
}
