using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Curves
{
    internal class CurveCalculator_MinXZero : ICurveCalculator
    {
        private readonly ArrayCalculator_MinPositionZero_Line _arrayCalculator;

        public CurveCalculator_MinXZero(CurveArrayInfo curveArrayInfo)
        {
            if (curveArrayInfo == null) throw new NullException(() => curveArrayInfo);

            _arrayCalculator = new ArrayCalculator_MinPositionZero_Line(
                curveArrayInfo.Array,
                curveArrayInfo.Rate,
                curveArrayInfo.YBefore,
                curveArrayInfo.YAfter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double CalculateY(double x)
        {
            return _arrayCalculator.Calculate(x);
        }
    }
}
