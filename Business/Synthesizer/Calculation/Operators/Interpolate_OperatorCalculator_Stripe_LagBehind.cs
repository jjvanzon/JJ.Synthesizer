namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal sealed class Interpolate_OperatorCalculator_Stripe_LagBehind
		: OperatorCalculatorBase_FollowingSampler_2X1Y_LagBehind
	{
		public Interpolate_OperatorCalculator_Stripe_LagBehind(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionCalculator)
			: base(signalCalculator, samplingRateCalculator, positionCalculator)
		    => ResetNonRecursive();

	    protected override void ResetNonRecursive()
	    {
	        base.ResetNonRecursive();

	        Interpolate_OperatorCalculator_Stripe_Helper.ResetNonRecursive(this);
	    }
	}
}