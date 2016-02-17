//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Framework.Reflection.Exceptions;

//namespace JJ.Business.Synthesizer.Calculation.Arrays
//{
//    internal abstract class ArrayCalculatorBase_MinTime
//    {
//        // Fields for performance
//        protected double[] _array;
//        protected double _valueBefore;
//        protected double _valueAfter;

//        protected double _tickCount;
//        protected double _rate;
//        protected double _minTime;
//        protected double _maxTime;
//        protected double _duration;

//        public ArrayCalculatorBase_MinTime(
//            double[] array,
//            double rate,
//            double minTime,
//            int extraTickCount = 0)
//        {
//            if (array == null) throw new NullException(() => array);
//            if (extraTickCount < 0) throw new LessThanException(() => extraTickCount, 0);

//            _array = array;

//            int tickCountInt = _array.Length;
//            _tickCount = tickCountInt;

//            _valueBefore = _array[0];
//            _valueAfter = _array.Last();

//            _minTime = minTime;
//            _rate = rate;
//            _maxTime = _minTime + (_tickCount - 1) / _rate; // 11 samples = 10 pieces of time.

//            _duration = _maxTime - _minTime;

//            if (extraTickCount > 0)
//            {
//                double[] array2 = new double[tickCountInt + extraTickCount];
//                Array.Copy(_array, array2, tickCountInt);
//                Array.Clear(array2, tickCountInt, extraTickCount);
//                _array = array2;
//            }
//        }

//        public ArrayCalculatorBase_MinTime(
//            double[] array,
//            double valueBefore,
//            double valueAfter,
//            double rate,
//            double minTime,
//            int extraTickCount = 0)
//            : this(array, rate, minTime, extraTickCount)
//        {
//            _valueBefore = valueBefore;
//            _valueAfter = valueAfter;
//        }

//        public abstract double CalculateValue(double time);
//    }
//}