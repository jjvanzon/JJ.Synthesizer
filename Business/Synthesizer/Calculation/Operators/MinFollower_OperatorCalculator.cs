namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class MinFollower_OperatorCalculator : MaxOrMinFollower_OperatorCalculatorBase
    {
        public MinFollower_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase sliceLengthCalculator,
            OperatorCalculatorBase sampleCountCalculator,
            OperatorCalculatorBase positionInputCalculator)
            : base(signalCalculator, sliceLengthCalculator, sampleCountCalculator, positionInputCalculator) { }

        protected override double Aggregate(double sample)
        {
            base.Aggregate(sample);

            return _redBlackTree.GetMinimum();
        }
    }
}