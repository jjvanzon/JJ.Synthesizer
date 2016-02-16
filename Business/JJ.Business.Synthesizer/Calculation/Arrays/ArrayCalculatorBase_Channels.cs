using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Arrays
{
    internal abstract class ArrayCalculatorBase_Channels
    {
        // Fields for performance

        /// <summary> First index is channel, second index time. </summary>
        protected double[,] _array;
        protected double _valueBefore;
        protected double _valueAfter;

        protected int _channelCount;
        protected double _tickCount;
        protected double _rate;
        protected double _maxTime;
        protected double _duration;

        public ArrayCalculatorBase_Channels(
            double[,] array,
            double rate,
            int extraTickCount = 0)
        {
            if (array == null) throw new NullException(() => array);
            if (array.GetLength(0) < 1) throw new LessThanException(() => array.GetLength(0), 1);
            if (extraTickCount < 0) throw new LessThanException(() => extraTickCount, 0);

            _array = array;

            _channelCount = _array.GetLength(0);
            int tickCountInt = _array.GetLength(1);
            _tickCount = tickCountInt;

            _valueBefore = _array[0, 0];
            _valueAfter = _array[0, _array.GetLength(1)];

            _rate = rate;
            _maxTime = (_tickCount - 1) / _rate;

            _duration = _maxTime;

            if (extraTickCount > 0)
            {
                double[,] array2 = new double[_channelCount, tickCountInt + extraTickCount];
                Array.Copy(_array, array2, tickCountInt * 2);
                Array.Clear(array2, tickCountInt * 2, extraTickCount);
                _array = array2;
            }
        }

        public ArrayCalculatorBase_Channels(
            double[,] array,
            double valueBefore,
            double valueAfter,
            double rate,
            int extraTickCount = 0)
            : this(array, rate, extraTickCount)
        {
            _valueBefore = valueBefore;
            _valueAfter = valueAfter;
        }

        public abstract double CalculateValue(double time, int channelIndex);
    }
}