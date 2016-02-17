using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Calculation.Arrays
{
    internal abstract class ArrayCalculatorBase_Line : ArrayCalculatorBase
    {
        private const int EXTRA_TICKS_BEFORE = 0;
        private const int EXTRA_TICKS_AFTER = 1;

        public ArrayCalculatorBase_Line(double[] array, double rate, double minTime)
            : base(array, rate, minTime, EXTRA_TICKS_BEFORE, EXTRA_TICKS_AFTER)
        { }

        public ArrayCalculatorBase_Line(
            double[] array, double rate, double minTime, double valueBefore, double valueAfter)
            : base(array, rate, minTime, EXTRA_TICKS_BEFORE, EXTRA_TICKS_AFTER, valueBefore, valueAfter)
        { }

        /// <summary> Base method does not check bounds of time or transform time from seconds to samples. </summary>
        public override double CalculateValue(double t)
        {
            int t0 = (int)t;
            int t1 = t0 + 1;

            double x0 = _array[t0];
            double x1 = _array[t1];

            double x = x0 + (x1 - x0) * (t - t0);
            return x;
        }
    }
}