namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SumFollower_OperatorCalculator : OperatorCalculatorBase_Follower
    {
        private double _sum;

        public SumFollower_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase sliceLengthCalculator,
            OperatorCalculatorBase sampleCountCalculator,
            OperatorCalculatorBase positionCalculator)
            : base(signalCalculator, sliceLengthCalculator, sampleCountCalculator, positionCalculator) { }

        protected override double Aggregate(double sample)
        {
            // Use a queueing trick to update the average without traversing a whole list.
            // This also makes the average update more continually.
            double oldSample = _queue.Dequeue();
            _sum -= oldSample;

            _sum += sample;

            return _sum;
        }

        protected override void ResetNonRecursive()
        {
            base.ResetNonRecursive();

            _sum = 0.0;
        }
    }
}