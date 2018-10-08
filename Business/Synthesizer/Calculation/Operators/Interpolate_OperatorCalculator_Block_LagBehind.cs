namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal sealed class Interpolate_OperatorCalculator_Block_LagBehind 
		: Interpolate_OperatorCalculator_Base_2X1Y_LagBehind
	{
		public Interpolate_OperatorCalculator_Block_LagBehind(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionInputCalculator)
			: base(signalCalculator, samplingRateCalculator, positionInputCalculator)
		    => ResetNonRecursive();

	    protected override void ResetNonRecursive() => Interpolate_OperatorCalculator_Block_Helper.ResetNonRecursive(this);
	}
}