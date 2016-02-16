using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Calculation.Arrays
{
    internal class ArrayCalculator_MinTimeZero_Block : ArrayCalculator_Block_Base
    {
        private const double MIN_TIME = 0.0;

        public ArrayCalculator_MinTimeZero_Block(double[] array, double rate) 
            : base(array, rate, MIN_TIME)
        { }

        public ArrayCalculator_MinTimeZero_Block(
            double[] array, double valueBefore, double valueAfter, double rate)
            : base(array, valueBefore, valueAfter, rate, MIN_TIME)
        { }

        public override double CalculateValue(double time)
        {
            // Return if sample not in range.
            // Execute it on the doubles, to prevent integer overflow later.
            if (time < 0) return _valueBefore;
            if (time > _maxTime) return _valueAfter;

            double t = time * _rate;

            return base.CalculateValue(t);
        }
    }
}
