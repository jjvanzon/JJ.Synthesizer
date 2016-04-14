using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Arrays
{
    internal class ArrayCalculator_RotatePosition_Hermite : ArrayCalculatorBase_Hermite
    {
        private const double DEFAULT_MIN_POSITION = 0;

        public ArrayCalculator_RotatePosition_Hermite(
            double[] array, double rate) 
            : base(array, rate, DEFAULT_MIN_POSITION)
        { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double CalculateValue(double position)
        {
            double transformedPosition = position % _length;

            // Account for negative positions.
            if (transformedPosition < 0.0)
            {
                transformedPosition += _length;
            }

            transformedPosition *= _rate;

            return base.CalculateValue(transformedPosition);
        }
    }
}
