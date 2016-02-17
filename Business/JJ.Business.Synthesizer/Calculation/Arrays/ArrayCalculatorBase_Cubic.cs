using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Mathematics;

namespace JJ.Business.Synthesizer.Calculation.Arrays
{
    internal abstract class ArrayCalculatorBase_Cubic : ArrayCalculatorBase
    {
        private const int EXTRA_TICKS_BEFORE = 1;
        private const int EXTRA_TICKS_AFTER = 2;

        public ArrayCalculatorBase_Cubic(double[] array, double rate, double minTime)
            : base(array, rate, minTime, EXTRA_TICKS_BEFORE, EXTRA_TICKS_AFTER)
        { }

        public ArrayCalculatorBase_Cubic(
            double[] array, double rate, double minTime, double valueBefore, double valueAfter)
            : base(array, rate, minTime, EXTRA_TICKS_BEFORE, EXTRA_TICKS_AFTER, valueBefore, valueAfter)
        { }

        /// <summary> Base method does not check bounds of time or transform time from seconds to samples. </summary>
        public override double CalculateValue(double t)
        {
            t += EXTRA_TICKS_BEFORE;

            int t0 = (int)t;
            int tMinus1 = t0 - 1; 
            int t1 = t0 + 1;
            int t2 = t1 + 1;

            double xMinus1 = _array[tMinus1];
            double x0 = _array[t0];
            double x1 = _array[t1];
            double x2 = _array[t1];

            double x = Interpolator.Interpolate_Cubic_Equidistant_SlightlyBetterThanLinear(
                xMinus1, x0, x1, x2, t);

            return x;
        }
    }
}