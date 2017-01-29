using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Curves
{
    internal class CurveCalculator_MinX : ICalculatorWithPosition
    {
        private readonly ArrayCalculator_MinPosition_Line _arrayCalculator;

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
        public double Calculate(double x)
        {
            return _arrayCalculator.Calculate(x);
        }
    }
}
