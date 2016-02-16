//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace JJ.Business.Synthesizer.Calculation.Arrays
//{
//    internal abstract class ArrayCalculatorBase_CurveSmoothInterpolation : ArrayCalculatorBase
//    {
//        private const int EXTRA_TICK_COUNT = 2;

//        public ArrayCalculatorBase_CurveSmoothInterpolation(double[] array, double rate, double minTime)
//            : base(array, rate, minTime, EXTRA_TICK_COUNT)
//        { }

//        public ArrayCalculatorBase_CurveSmoothInterpolation(
//            double[] array, double valueBefore, double valueAfter, double rate, double minTime)
//            : base(array, valueBefore, valueAfter, rate, minTime, EXTRA_TICK_COUNT)
//        { }

//        /// <summary> Base method does not check bounds of time or transform time from seconds to samples. </summary>
//        public override double CalculateValue(double t)
//        {

//            int t0 = (int)t;
//            int t1 = t0 + 1; // Note that extra tick count = 1.

//            double x0 = _array[t0];
//            double x1 = _array[t1];

//            double x = x0 + (x1 - x0) * (t - t0);
//            return x;
//        }
//    }
//}