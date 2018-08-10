namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal sealed class Interpolate_OperatorCalculator_Block 
		: OperatorCalculatorBase_FollowingSampler_2X1Y_LagBehind
	{
		public Interpolate_OperatorCalculator_Block(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionCalculator)
			: base(signalCalculator, samplingRateCalculator, positionCalculator)
		    => ResetNonRecursive();

	    protected override void ResetNonRecursive()
		{
		    base.ResetNonRecursive();

		    double x = _positionCalculator.Calculate();
			double y = _signalCalculator.Calculate();

			_x0 = x;
			_x1 = x + GetLargeDx();

			_y0 = y;
		}
	}
}