namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class AverageFollower_OperatorCalculator : SumFollower_OperatorCalculator
    {
        public AverageFollower_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase sliceLengthCalculator,
            OperatorCalculatorBase sampleCountCalculator,
            OperatorCalculatorBase positionCalculator)
            : base(signalCalculator, sliceLengthCalculator, sampleCountCalculator, positionCalculator)
        { }

        protected override double Aggregate(double sample)
        {
            double sum = base.Aggregate(sample);
            double average = sum / _sampleCountDouble;
            return average;
        }
    }
}