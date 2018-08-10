namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal sealed class MaxFollowerWithSamplingRate_OperatorCalculator : MaxOrMinFollower_OperatorCalculatorBase
	{
		public MaxFollowerWithSamplingRate_OperatorCalculator(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase sliceLengthCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionInputCalculator)
			: base(signalCalculator, sliceLengthCalculator, samplingRateCalculator, positionInputCalculator)
	        => ResetNonRecursive();

        protected override void Precalculate()
		{
		    base.Precalculate();

	        _aggregate = _redBlackTree.GetMaximum();
	    }
	}
}