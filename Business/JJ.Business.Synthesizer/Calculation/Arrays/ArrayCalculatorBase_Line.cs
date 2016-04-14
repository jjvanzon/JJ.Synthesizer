using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Arrays
{
    internal abstract class ArrayCalculatorBase_Line : ArrayCalculatorBase
    {
        private const int EXTRA_TICKS_BEFORE = 0;
        private const int EXTRA_TICKS_AFTER = 1;

        public ArrayCalculatorBase_Line(double[] array, double rate, double minPosition)
            : base(array, rate, minPosition, EXTRA_TICKS_BEFORE, EXTRA_TICKS_AFTER)
        { }

        public ArrayCalculatorBase_Line(
            double[] array, double rate, double minPosition, double valueBefore, double valueAfter)
            : base(array, rate, minPosition, EXTRA_TICKS_BEFORE, EXTRA_TICKS_AFTER, valueBefore, valueAfter)
        { }

        /// <summary> Base method does not check bounds or transform position from 'seconds to samples'. </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double CalculateValue(double x)
        {
            int x0 = (int)x;
            int x1 = x0 + 1;

            double y0 = _array[x0];
            double y1 = _array[x1];

            double y = y0 + (y1 - y0) * (x - x0);
            return y;
        }
    }
}