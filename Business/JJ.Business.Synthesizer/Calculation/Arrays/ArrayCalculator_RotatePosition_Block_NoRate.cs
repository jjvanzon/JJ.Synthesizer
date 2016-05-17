using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Arrays
{
    internal class ArrayCalculator_RotatePosition_Block_NoRate : ArrayCalculatorBase_Block
    {
        private const double DEFAULT_MIN_POSITION = 0.0;
        private const double DEFAULT_RATE = 1.0;

        public ArrayCalculator_RotatePosition_Block_NoRate(double[] array) 
            : base(array, DEFAULT_RATE, DEFAULT_MIN_POSITION)
        { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double CalculateValue(double position)
        {
            if (Double.IsNaN(position)) return 0.0;
            if (Double.IsInfinity(position)) return 0.0;

            double transformedPosition = position % _length;

            // Account for negative positions.
            if (transformedPosition < 0.0)
            {
                transformedPosition += _length;
            }

            return base.CalculateValue(transformedPosition);
        }
    }
}
