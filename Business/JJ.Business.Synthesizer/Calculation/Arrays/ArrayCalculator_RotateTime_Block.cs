using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Calculation.Arrays
{
    internal class ArrayCalculator_RotateTime_Block : ArrayCalculatorBase_Block
    {
        public ArrayCalculator_RotateTime_Block(double[] array, double rate, double minTime) 
            : base(array, rate, minTime)
        { }

        public ArrayCalculator_RotateTime_Block(
            double[] array, double valueBefore, double valueAfter, double rate, double minTime)
            : base(array, valueBefore, valueAfter, rate, minTime)
        { }

        public override double CalculateValue(double time)
        {
            time = time % _duration;

            double t = time * _rate;

            return base.CalculateValue(t);
        }
    }
}
