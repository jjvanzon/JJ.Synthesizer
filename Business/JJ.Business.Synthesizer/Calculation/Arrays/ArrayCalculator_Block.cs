using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Calculation.Arrays
{
    internal class ArrayCalculator_Block : ArrayCalculatorBase
    {
        public ArrayCalculator_Block(double[,] array, double rate, double minTime = 0) 
            : base(array, rate, minTime)
        { }

        public ArrayCalculator_Block(
            double[,] array, 
            double valueBefore, 
            double valueAfter,
            double rate,
            double minTime = 0) 
            : base(array, valueBefore, valueAfter, rate, minTime)
        { }

        public override double CalculateValue(double time, int channelIndex)
        {
            // Return if sample not in range.
            // Execute it on the doubles, to prevent integer overflow.
            if (time < _minTime) return _valueBefore;
            if (time > _maxTime) return _valueAfter;

            double t = (time - _minTime) * _rate;
            int t0 = (int)t;

            double value = _array[channelIndex, t0];
            return value;
        }
    }
}
