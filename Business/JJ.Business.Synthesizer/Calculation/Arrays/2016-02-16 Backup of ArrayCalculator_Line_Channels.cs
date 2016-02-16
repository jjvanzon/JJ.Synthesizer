//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace JJ.Business.Synthesizer.Calculation.Arrays
//{
//    internal class ArrayCalculator_Line_Channels : ArrayCalculatorBase_Channels
//    {
//        private const int EXTRA_TIME_TICK_COUNT = 1;

//        public ArrayCalculator_Line_Channels(double[,] array, double rate, double minTime)
//            : base(array, rate, minTime, EXTRA_TIME_TICK_COUNT)
//        { }

//        public ArrayCalculator_Line_Channels(
//            double[,] array,
//            double valueBefore,
//            double valueAfter,
//            double rate)
//            : base(array, valueBefore, valueAfter, rate, EXTRA_TIME_TICK_COUNT)
//        { }

//        public override double CalculateValue(double time, int channelIndex)
//        {
//            // Return if sample not in range.
//            // Execute it on the doubles, to prevent integer overflow.
//            if (time < 0) return _valueBefore;
//            if (time > _maxTime) return _valueAfter;

//            double t = time * _rate;

//            int t0 = (int)t;
//            int t1 = t0 + 1; // Note that extra tick count = 1.

//            double x0 = _array[channelIndex, t0];
//            double x1 = _array[channelIndex, t1];

//            double x = x0 + (x1 - x0) * (t - t0);
//            return x;
//        }
//    }
//}