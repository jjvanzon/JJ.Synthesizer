using JJ.Framework.Collections;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class MaxFollower_OperatorCalculator_AllVars : MaxOrMinFollower_OperatorCalculatorBase
    {
        public MaxFollower_OperatorCalculator_AllVars(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase sliceLengthCalculator,
            OperatorCalculatorBase sampleCountCalculator,
            DimensionStack dimensionStack)
            : base(signalCalculator, sliceLengthCalculator, sampleCountCalculator, dimensionStack)
        { }

        protected override double GetMaxOrMin(RedBlackTree<double, double> redBlackTree)
        {
            return redBlackTree.GetMaximum();
        }
    }
}
