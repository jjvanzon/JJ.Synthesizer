using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Arrays
{
    internal abstract class ArrayCalculatorBase
    {
        // Fields for performance
        protected double[] _array;
        protected double _valueBefore;
        protected double _valueAfter;

        protected double _tickCount;
        protected double _rate;
        protected double _minTime;
        protected double _maxTime;
        protected double _duration;

        public ArrayCalculatorBase(
            double[] array, double rate, double minTime, int extraTicksBefore, int extraTicksAfter)
        {
            if (array == null) throw new NullException(() => array);
            if (extraTicksBefore < 0) throw new LessThanException(() => extraTicksBefore, 0);
            if (extraTicksAfter < 0) throw new LessThanException(() => extraTicksAfter, 0);

            _array = array;

            int tickCountInt = _array.Length;
            _tickCount = tickCountInt;

            _valueBefore = _array[0];
            _valueAfter = _array.Last();

            _minTime = minTime;
            _rate = rate;
            _maxTime = _minTime + (_tickCount - 1) / _rate; // 11 samples = 10 pieces of time.

            _duration = _maxTime - _minTime;

            int extraTickCount = extraTicksBefore + extraTicksAfter;
            if (extraTickCount > 0)
            {
                double[] array2 = new double[tickCountInt + extraTickCount];

                for (int i = 0; i < extraTicksBefore; i++)
                {
                    array2[i] = _valueBefore;
                }

                Array.Copy(_array, 0, array2, extraTicksBefore, tickCountInt);

                for (int i = tickCountInt + extraTicksBefore; i < tickCountInt + extraTickCount; i++)
                {
                    array2[i] = _valueAfter;
                }

                _array = array2;
            }
        }

        public ArrayCalculatorBase(
            double[] array, 
            double valueBefore, double valueAfter, 
            double rate, double minTime, 
            int extraTicksBefore, int extraTicksAfter)
            : this(array, rate, minTime, extraTicksBefore, extraTicksAfter)
        {
            _valueBefore = valueBefore;
            _valueAfter = valueAfter;
        }

        public abstract double CalculateValue(double time);
    }
}