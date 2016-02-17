using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Arrays
{
    internal class ArrayCalculator_MinTimeZero_Hermite : ArrayCalculatorBase_Hermite
    {
        private const double MIN_TIME = 0.0;

        public ArrayCalculator_MinTimeZero_Hermite(
            double[] array, double rate)
            : base(array, rate, MIN_TIME)
        { }

        public ArrayCalculator_MinTimeZero_Hermite(
            double[] array, double rate, double valueBefore, double valueAfter)
            : base(array, rate, MIN_TIME, valueBefore, valueAfter)
        { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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