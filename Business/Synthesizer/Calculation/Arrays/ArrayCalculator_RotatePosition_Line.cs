using System;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Arrays
{
    internal class ArrayCalculator_RotatePosition_Line : ArrayCalculatorBase_Line
    {
        private const double DEFAULT_MIN_POSITION = 0;

        public ArrayCalculator_RotatePosition_Line(
            double[] array, double rate) 
            : base(array, rate, DEFAULT_MIN_POSITION)
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

            transformedPosition *= _rate;

            return base.CalculateValue(transformedPosition);
        }
    }
}
