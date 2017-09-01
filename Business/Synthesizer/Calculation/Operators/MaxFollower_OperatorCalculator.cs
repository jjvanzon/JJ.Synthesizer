using JJ.Framework.Collections;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class MaxFollower_OperatorCalculator : MaxOrMinFollower_OperatorCalculatorBase
    {
        public MaxFollower_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase sliceLengthCalculator,
            OperatorCalculatorBase sampleCountCalculator,
            OperatorCalculatorBase positionInputCalculator,
            VariableInput_OperatorCalculator positionOutputCalculator)
            : base(
                signalCalculator,
                sliceLengthCalculator,
                sampleCountCalculator,
                positionInputCalculator,
                positionOutputCalculator)
        { }

        protected override double GetMaxOrMin(RedBlackTree<double, double> redBlackTree)
        {
            return redBlackTree.GetMaximum();
        }
    }
}