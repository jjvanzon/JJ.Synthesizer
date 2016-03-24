using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Collections;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Maximum_OperatorCalculator : MaximumOrMinimum_OperatorCalculatorBase
    {
        public Maximum_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase timeSliceDurationCalculator,
            OperatorCalculatorBase sampleCountCalculator)
            : base(signalCalculator, timeSliceDurationCalculator, sampleCountCalculator)
        { }

        protected override double GetMaximumOrMinimum(RedBlackTree<double, double> redBlackTree)
        {
            return redBlackTree.GetMaximum();
        }
    }
}
