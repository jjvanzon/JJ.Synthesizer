using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Arrays
{
    internal class ArrayCalculator_RotateTime_Line : ArrayCalculatorBase_Line
    {
        private const double DEFAULT_MIN_TIME = 0;

        public ArrayCalculator_RotateTime_Line(
            double[] array, double rate) 
            : base(array, rate, DEFAULT_MIN_TIME)
        { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double CalculateValue(double time)
        {
            time = time % _duration;

            double t = time * _rate;

            return base.CalculateValue(t);
        }
    }
}
