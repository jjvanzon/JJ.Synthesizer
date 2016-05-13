using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Collections;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Maximum_OperatorCalculator : MaximumOrMinimum_OperatorCalculatorBase
    {
        public Maximum_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase timeSliceDurationCalculator,
            OperatorCalculatorBase sampleCountCalculator,
            DimensionEnum dimensionEnum,
            DimensionStack dimensionStack)
            : base(signalCalculator, timeSliceDurationCalculator, sampleCountCalculator, dimensionEnum, dimensionStack)
        { }

        protected override double GetMaximumOrMinimum(RedBlackTree<double, double> redBlackTree)
        {
            return redBlackTree.GetMaximum();
        }
    }
}
