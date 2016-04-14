using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Arrays
{
    internal class ArrayCalculator_MinPosition_Hermite : ArrayCalculatorBase_Hermite
    {
        public ArrayCalculator_MinPosition_Hermite(
            double[] array, double rate, double minPosition)
            : base(array, rate, minPosition)
        { }

        public ArrayCalculator_MinPosition_Hermite(
            double[] array, double rate, double minPosition, double valueBefore, double valueAfter)
            : base(array, rate, minPosition, valueBefore, valueAfter)
        { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double CalculateValue(double position)
        {
            // Return if sample not in range.
            // Execute it on the doubles, to prevent integer overflow.
            if (position < _minPosition) return _valueBefore;
            if (position > _maxPosition) return _valueAfter;

            double t = (position - _minPosition) * _rate;

            return base.CalculateValue(t);
        }
    }
}