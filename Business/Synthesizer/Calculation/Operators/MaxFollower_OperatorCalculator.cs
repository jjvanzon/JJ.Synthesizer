namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal class MaxFollower_OperatorCalculator : MaxOrMinFollower_OperatorCalculatorBase
	{
		public MaxFollower_OperatorCalculator(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase sliceLengthCalculator,
			OperatorCalculatorBase sampleCountCalculator,
			OperatorCalculatorBase positionInputCalculator)
			: base(signalCalculator, sliceLengthCalculator, sampleCountCalculator, positionInputCalculator) { }

		protected override double Aggregate(double sample)
		{
			base.Aggregate(sample);

			return _redBlackTree.GetMaximum();
		}
	}
}