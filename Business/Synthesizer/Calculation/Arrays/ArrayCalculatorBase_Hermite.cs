using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;

namespace JJ.Business.Synthesizer.Calculation.Arrays
{
    internal abstract class ArrayCalculatorBase_Hermite : ArrayCalculatorBase, ICalculatorWithPosition
    {
        private const int EXTRA_TICKS_BEFORE = 1;
        private const int EXTRA_TICKS_AFTER = 2;

        public ArrayCalculatorBase_Hermite(double[] array, double rate, double minPosition)
            : base(array, rate, minPosition, EXTRA_TICKS_BEFORE, EXTRA_TICKS_AFTER)
        { }

        public ArrayCalculatorBase_Hermite(
            double[] array, double rate, double minPosition, double valueBefore, double valueAfter)
            : base(array, rate, minPosition, EXTRA_TICKS_BEFORE, EXTRA_TICKS_AFTER, valueBefore, valueAfter)
        { }

        /// <summary> Base method does not check bounds or transform position from 'seconds to samples'. </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate(double x)
        {
            x += EXTRA_TICKS_BEFORE;

            int x0 = (int)x;
            int xMinus1 = x0 - 1; 
            int x1 = x0 + 1;
            int x2 = x1 + 1;

            double yMinus1 = _array[xMinus1];
            double y0 = _array[x0];
            double y1 = _array[x1];
            double y2 = _array[x2];

            double offset = x - x0;

            double y = Interpolator.Interpolate_Hermite_4pt3oX(yMinus1, y0, y1, y2, offset);
            return y;
        }
    }
}