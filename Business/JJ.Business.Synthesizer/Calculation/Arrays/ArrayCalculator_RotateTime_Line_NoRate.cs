using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Arrays
{
    internal class ArrayCalculator_RotateTime_Line_NoRate : ArrayCalculatorBase_Line
    {
        private const double DEFAULT_RATE = 1.0;
        private const double DEFAULT_MIN_TIME = 0.0;

        public ArrayCalculator_RotateTime_Line_NoRate(double[] array) 
            : base(array, DEFAULT_RATE, DEFAULT_MIN_TIME)
        { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double CalculateValue(double time)
        {
            double transformedTime = time % _duration;

            // Account for negative times.
            if (transformedTime < 0.0)
            {
                transformedTime += _duration;
            }

            return base.CalculateValue(transformedTime);
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
