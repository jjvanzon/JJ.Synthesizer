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
            OperatorCalculatorBase sampleCountCalculator,
            DimensionStack dimensionStack)
            : base(signalCalculator, timeSliceDurationCalculator, sampleCountCalculator, dimensionStack)
        { }

        protected override double GetMaximumOrMinimum(RedBlackTree<double, double> redBlackTree)
        {
            return redBlackTree.GetMaximum();
        }
    }
}
