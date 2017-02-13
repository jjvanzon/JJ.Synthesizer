using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Arrays
{
    internal class ArrayCalculator_RotatePosition_Line_NoRate : ArrayCalculatorBase_Line, ICalculatorWithPosition
    {
        private const double DEFAULT_RATE = 1.0;
        private const double DEFAULT_MIN_POSITION = 0.0;

        public override bool IsRotatingPosition => true;

        public ArrayCalculator_RotatePosition_Line_NoRate(double[] array) 
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

        // Brainstorm to check if t0 + 1 could cause index out of range:
        //
        // sample count = 10
        // sample count - 1 = 9
        //     0 % 9 = 0
        //     8 % 9 = 8
        //     9 % 9 = 0
        //    10 % 9 = 1
        // 9.001 % 9 = 0.001
        // 8.999 % 9 = 8.999
        //
        // It will never become 9.
        // So t0 + 1 is max 9, the maximum array index.
    }
}
