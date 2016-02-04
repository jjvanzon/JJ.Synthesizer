using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Framework.Collections;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Maximum_OperatorCalculator : MaximumOrMinimum_OperatorCalculatorBase
    {
        public Maximum_OperatorCalculator(OperatorCalculatorBase signalCalculator, double timeSliceDuration, int sampleCount)
            : base(signalCalculator, timeSliceDuration, sampleCount)
        { }

        protected override double GetMaximumOrMinimum(RedBlackTree<double, double> redBlackTree)
        {
            return redBlackTree.GetMaximum();
        }
    }
}
