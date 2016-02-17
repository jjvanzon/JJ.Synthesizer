using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Calculation.Arrays
{
    internal class ArrayCalculator_RotateTime_Stripe : ArrayCalculatorBase_Stripe
    {
        private const double DEFAULT_MIN_TIME = 0;

        public ArrayCalculator_RotateTime_Stripe(
            double[] array, double rate) 
            : base(array, rate, DEFAULT_MIN_TIME)
        { }

        public ArrayCalculator_RotateTime_Stripe(
            double[] array, double rate, double valueBefore, double valueAfter)
            : base(array, rate, DEFAULT_MIN_TIME, valueBefore, valueAfter)
        { }

        public override double CalculateValue(double time)
        {
            time = time % _duration;

            double t = time * _rate;

            return base.CalculateValue(t);
        }
    }
}
