//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//	internal class SumFollower_OperatorCalculator_Backup : OperatorCalculatorBase_FollowingSampler_Aggregate
//	{
//	    public SumFollower_OperatorCalculator_Backup(
//	        OperatorCalculatorBase signalCalculator,
//	        OperatorCalculatorBase sliceLengthCalculator,
//	        OperatorCalculatorBase samplingRateCalculator,
//	        OperatorCalculatorBase positionCalculator)
//	        : base(signalCalculator, sliceLengthCalculator, samplingRateCalculator, positionCalculator)
//	        // ReSharper disable once VirtualMemberCallInConstructor
//	        => ResetNonRecursive();

//		protected override void Precalculate()
//		{
//		    _aggregate -= _yMinus1;
//		    _aggregate += _y0;
//		}
//	}
//}