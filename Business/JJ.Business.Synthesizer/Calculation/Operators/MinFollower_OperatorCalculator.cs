using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Collections;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class MinFollower_OperatorCalculator : MaxOrMinFollower_OperatorCalculatorBase
    {
        public MinFollower_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase timeSliceDurationCalculator,
            OperatorCalculatorBase sampleCountCalculator,
            DimensionStack dimensionStack)
            : base(signalCalculator, timeSliceDurationCalculator, sampleCountCalculator, dimensionStack)
        { }

        protected override double GetMaxOrMin(RedBlackTree<double, double> redBlackTree)
        {
            return redBlackTree.GetMinimum();
        }
    }
}