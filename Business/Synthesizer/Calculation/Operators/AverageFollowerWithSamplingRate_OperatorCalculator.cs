namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal class AverageFollowerWithSamplingRate_OperatorCalculator : SumFollowerWithSamplingRate_OperatorCalculator
	{
		public AverageFollowerWithSamplingRate_OperatorCalculator(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase sliceLengthCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionCalculator)
			: base(signalCalculator, sliceLengthCalculator, samplingRateCalculator, positionCalculator)
		{ }

		protected override void Precalculate()
		{
		    base.Precalculate();

		    _aggregate = _aggregate / GetSampleCount();
		}
	}
}