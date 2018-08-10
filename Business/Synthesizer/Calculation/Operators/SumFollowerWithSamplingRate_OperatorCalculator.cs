namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal class SumFollowerWithSamplingRate_OperatorCalculator : OperatorCalculatorBase_FollowingSampler_Aggregate
	{
	    public SumFollowerWithSamplingRate_OperatorCalculator(
	        OperatorCalculatorBase signalCalculator,
	        OperatorCalculatorBase sliceLengthCalculator,
	        OperatorCalculatorBase samplingRateCalculator,
	        OperatorCalculatorBase positionCalculator)
	        : base(signalCalculator, sliceLengthCalculator, samplingRateCalculator, positionCalculator)
	        // ReSharper disable once VirtualMemberCallInConstructor
	        => ResetNonRecursive();

		protected override void Precalculate()
		{
		    _aggregate -= _yLast;
		    _aggregate += _yFirst;
		}
	}
}