using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Calculation.Arrays
{
    internal class ArrayCalculator_MinTime_Block : ArrayCalculatorBase_Block
    {
        public ArrayCalculator_MinTime_Block(
            double[] array, double rate, double minTime) 
            : base(array, rate, minTime)
        { }

        public ArrayCalculator_MinTime_Block(
            double[] array, double rate, double minTime, double valueBefore, double valueAfter)
            : base(array, rate, minTime, valueBefore, valueAfter)
        { }

        public override double CalculateValue(double time)
        {
            // Return if sample not in range.
            // Execute it on the doubles, to prevent integer overflow.
            if (time < _minTime) return _valueBefore;
            if (time > _maxTime) return _valueAfter;

            double t = (time - _minTime) * _rate;

            return base.CalculateValue(t);
        }
    }
}
