using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Arrays
{
    internal class ArrayCalculator_RotatePosition_Block_NoRate : ArrayCalculatorBase_Block, ICalculatorWithPosition
    {
        private const double DEFAULT_MIN_POSITION = 0.0;
        private const double DEFAULT_RATE = 1.0;

        public ArrayCalculator_RotatePosition_Block_NoRate(double[] array) 
            : base(array, DEFAULT_RATE, DEFAULT_MIN_POSITION, valueBefore: 0.0, valueAfter: 0.0)
        { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new double Calculate(double position)
        {
            if (double.IsNaN(position)) return 0.0;
            if (double.IsInfinity(position)) return 0.0;

            double transformedPosition = position % _length;

            // Account for negative positions.
            if (transformedPosition < 0.0)
            {
                transformedPosition += _length;
            }

            return base.Calculate(transformedPosition);
        }
    }
}
