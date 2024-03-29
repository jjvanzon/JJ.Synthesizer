﻿//using System;
//using JJ.Framework.Exceptions;

//namespace JJ.Business.Synthesizer.Calculation.Arrays
//{
//    internal class ArrayInfo
//    {
//        // Fields for performance
//        public double[] _array;
//        public double _valueBefore;
//        public double _valueAfter;

//        public double _minPosition;
//        public double _maxPosition;
//        public double _length;
//        public double _rate;
//        public double _tickCount;

//        /// <param name="extraTicksBefore">You can let this base class add extra ticks _array for interpolation purposes.</param>
//        public ArrayInfo(
//            double[] array, double rate, double minPosition, int extraTicksBefore, int extraTicksAfter)
//        {
//            if (array == null) throw new NullException(() => array);
//            if (extraTicksBefore < 0) throw new LessThanException(() => extraTicksBefore, 0);
//            if (extraTicksAfter < 0) throw new LessThanException(() => extraTicksAfter, 0);

//            _array = array;

//            int tickCountInt = _array.Length;
//            _tickCount = tickCountInt;

//            _minPosition = minPosition;
//            _rate = rate;
//            _maxPosition = _minPosition + (_tickCount - 1) / _rate; // 11 positions = 10 pieces of length.

//            _length = _maxPosition - _minPosition;

//            // The array copy actions here (to add extra ticks before and after),
//            // are an unfortunate memory impact and initialization time sacrifice.
//            // But it is done to prevent programming errors.
//            // If you would do it outside of this base class,
//            // chances are, you might program it wrong every now and then,
//            // and cause the sound generation to crash in exceptional cases.
//            int extraTickCount = extraTicksBefore + extraTicksAfter;
//            // ReSharper disable once InvertIf
//            if (extraTickCount > 0)
//            {
//                var array2 = new double[tickCountInt + extraTickCount];

//                for (int i = 0; i < extraTicksBefore; i++)
//                {
//                    array2[i] = _valueBefore;
//                }

//                Array.Copy(_array, 0, array2, extraTicksBefore, tickCountInt);

//                for (int i = tickCountInt + extraTicksBefore; i < tickCountInt + extraTickCount; i++)
//                {
//                    array2[i] = _valueAfter;
//                }

//                _array = array2;
//            }
//        }

//        public ArrayInfo(
//            double[] array,
//            double rate,
//            double minPosition,
//            int extraTicksBefore,
//            int extraTicksAfter,
//            double valueBefore,
//            double valueAfter)
//            : this(array, rate, minPosition, extraTicksBefore, extraTicksAfter)
//        {
//            _valueBefore = valueBefore;
//            _valueAfter = valueAfter;
//        }
//    }
//}
