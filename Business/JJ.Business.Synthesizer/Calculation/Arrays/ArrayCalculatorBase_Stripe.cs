using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Calculation.Arrays
{
    internal class ArrayCalculatorBase_Stripe : ArrayCalculatorBase
    {
        private const int EXTRA_TICK_COUNT = 1;

        public ArrayCalculatorBase_Stripe(double[] array, double rate, double minTime)
            : base(array, rate, minTime, EXTRA_TICK_COUNT)
        { }

        public ArrayCalculatorBase_Stripe(
            double[] array, double valueBefore, double valueAfter, double rate, double minTime)
            : base(array, valueBefore, valueAfter, rate, minTime, EXTRA_TICK_COUNT)
        { }

        /// <summary> Base method does not check bounds of time or transform time from seconds to samples. </summary>
        public override double CalculateValue(double t)
        {
            t += 0.5;

            int t0 = (int)t;

            double value = _array[t0];
            return value;
        }
    }
}