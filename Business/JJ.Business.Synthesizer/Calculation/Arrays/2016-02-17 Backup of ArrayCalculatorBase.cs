//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Framework.Reflection.Exceptions;

//namespace JJ.Business.Synthesizer.Calculation.Arrays
//{
//    internal abstract class ArrayCalculatorBase
//    {
//        // Fields for performance
//        protected double[] _array;
//        protected double _valueBefore;
//        protected double _valueAfter;

//        protected double _tickCount;
//        protected double _rate;

//        protected double _maxTime;
//        protected double _duration;

//        public ArrayCalculatorBase(
//            double[] array,
//            double rate,

//            int extraTickCount = 0)
//        {
//            if (array == null) throw new NullException(() => array);
//            if (extraTickCount < 0) throw new LessThanException(() => extraTickCount, 0);

//            _array = array;

//            int tickCountInt = _array.Length;
//            _tickCount = tickCountInt;

//            _valueBefore = _array[0];
//            _valueAfter = _array.Last();


//            _rate = rate;
//            _maxTime = (_tickCount - 1) / _rate; // 11 samples = 10 pieces of time.

//            _duration = _maxTime;

//            if (extraTickCount > 0)
//            {
//                double[] array2 = new double[tickCountInt + extraTickCount];
//                Array.Copy(_array, array2, tickCountInt);
//                Array.Clear(array2, tickCountInt, extraTickCount);
//                _array = array2;
//            }
//        }

//        public ArrayCalculatorBase(
//            double[] array,
//            double valueBefore,
//            double valueAfter,
//            double rate,

//            int extraTickCount = 0)
//            : this(array, rate, extraTickCount)
//        {
//            _valueBefore = valueBefore;
//            _valueAfter = valueAfter;
//        }

//        public abstract double CalculateValue(double time);
//    }
//}