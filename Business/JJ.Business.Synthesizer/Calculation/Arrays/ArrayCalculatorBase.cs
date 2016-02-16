using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Arrays
{
    internal abstract class ArrayCalculatorBase
    {
        // I fear performance overhead if I would make these properties,
        // even though by making them fields, they are not encapsulated.

        /// <summary> First index is channel, second index time. </summary>
        protected double[,] _array;
        protected double _valueBefore;
        protected double _valueAfter;

        protected int _channelCount;
        protected double _timeTickCount;
        protected double _minTime;
        protected double _rate;
        protected double _maxTime;
        protected double _duration;

        public ArrayCalculatorBase(
            double[,] array,
            double rate,
            double minTime = 0,
            int extraTimeTickCount = 0)
        {
            if (array == null) throw new NullException(() => array);
            if (array.GetLength(0) < 1) throw new LessThanException(() => array.GetLength(0), 1);
            if (extraTimeTickCount < 0) throw new LessThanException(() => extraTimeTickCount, 0);

            _array = array;

            _channelCount = _array.GetLength(0);
            int timeTickCountInt = _array.GetLength(1);
            _timeTickCount = timeTickCountInt;

            _valueBefore = _array[0, 0];
            _valueAfter = _array[0, _array.GetLength(1)];

            _minTime = minTime;
            _rate = rate;
            // TODO: Not sure if it will harm interpolation if I subtract 1. (11 samples = 10 pieces of time?)
            _maxTime = _minTime + (_timeTickCount - 1) / _rate;

            _duration = _maxTime - _minTime;

            // Apply extra sample count (for interpolation purposes).
            // It does not seem every very efficient, but it does do the trick.
            double[,] array2 = new double[_channelCount, timeTickCountInt + extraTimeTickCount];
            Array.Copy(_array, array2, timeTickCountInt * 2);
            Array.Clear(array2, timeTickCountInt * 2, extraTimeTickCount);
            _array = array2;
        }

        public ArrayCalculatorBase(
            double[,] array,
            double valueBefore,
            double valueAfter,
            double rate,
            double minTime = 0,
            int extraTimeTickCount = 0)
            : this(array, rate, minTime, extraTimeTickCount)
        {
            _valueBefore = valueBefore;
            _valueAfter = valueAfter;
        }

        public abstract double CalculateValue(double time, int channelIndex);
    }
}