using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Arrays
{
    internal class ArrayCalculator_RotateTime_Stripe_NoRate : ArrayCalculatorBase_Stripe
    {
        private const double DEFAULT_RATE = 1.0;
        private const double DEFAULT_MIN_TIME = 0.0;

        public ArrayCalculator_RotateTime_Stripe_NoRate(double[] array)
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
    }
}
