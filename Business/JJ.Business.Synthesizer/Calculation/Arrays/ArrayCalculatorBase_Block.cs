using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Calculation.Arrays
{
    internal class ArrayCalculatorBase_Block : ArrayCalculatorBase
    {
        public ArrayCalculatorBase_Block(double[] array, double rate, double minTime)
            : base(array, rate, minTime)
        { }

        public ArrayCalculatorBase_Block(
            double[] array, double valueBefore, double valueAfter, double rate, double minTime)
            : base(array, valueBefore, valueAfter, rate, minTime)
        { }

        /// <summary> Base method does not check bounds of time or transform time from seconds to samples. </summary>
        public override double CalculateValue(double t)
        {
            int t0 = (int)t;

            double value = _array[t0];
            return value;
        }
    }
}