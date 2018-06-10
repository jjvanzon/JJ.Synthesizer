namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal sealed class Interpolate_OperatorCalculator_Block 
		: Interpolate_OperatorCalculator_Base_2X1Y_LagBehind
	{
		public Interpolate_OperatorCalculator_Block(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionInputCalculator)
			: base(signalCalculator, samplingRateCalculator, positionInputCalculator)
		    => ResetNonRecursive();

	    protected override void ResetNonRecursive()
		{
			double x = _positionInputCalculator.Calculate();
			double y = _signalCalculator.Calculate();

			_x0 = x;
			_x1 = x + Dx();

			_y0 = y;
		}
	}
}