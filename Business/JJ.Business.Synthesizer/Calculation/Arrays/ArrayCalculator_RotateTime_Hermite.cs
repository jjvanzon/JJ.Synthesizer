using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Arrays
{
    internal class ArrayCalculator_RotateTime_Hermite : ArrayCalculatorBase_Hermite
    {
        private const double DEFAULT_MIN_TIME = 0;

        public ArrayCalculator_RotateTime_Hermite(
            double[] array, double rate) 
            : base(array, rate, DEFAULT_MIN_TIME)
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

            transformedTime *= _rate;

            return base.CalculateValue(transformedTime);
        }
    }
}
