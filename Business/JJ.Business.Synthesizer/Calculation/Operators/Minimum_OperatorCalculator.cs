using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Framework.Collections;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Minimum_OperatorCalculator : MaximumOrMinimum_OperatorCalculatorBase
    {
        public Minimum_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase timeSliceDurationCalculator,
            OperatorCalculatorBase sampleCountCalculator)
            : base(signalCalculator, timeSliceDurationCalculator, sampleCountCalculator)
        { }

        protected override double GetMaximumOrMinimum(RedBlackTree<double, double> redBlackTree)
        {
            return redBlackTree.GetMinimum();
        }
    }
}
