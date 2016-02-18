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

        protected double _minTime;
        protected double _maxTime;
        protected double _duration;
        protected double _rate;
        protected double _tickCount;

        /// <param name="extraTicksBefore">You can let this base class add extra ticks _array for interpolation purposes.</param>
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

            // The array copy actions here (to add extra ticks before and after),
            // are an unfortunate memory impact and initialization time sacrifice.
            // But it is done to prevent programming errors.
            // If you would do it outside of this base class,
            // chances are, you might program it wrong every now and then,
            // and cause the sound generation to crash in exceptional cases.
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
            double rate,
            double minTime,
            int extraTicksBefore,
            int extraTicksAfter,
            double valueBefore,
            double valueAfter)
            : this(array, rate, minTime, extraTicksBefore, extraTicksAfter)
        {
            _valueBefore = valueBefore;
            _valueAfter = valueAfter;
        }

        public abstract double CalculateValue(double time);
    }
}