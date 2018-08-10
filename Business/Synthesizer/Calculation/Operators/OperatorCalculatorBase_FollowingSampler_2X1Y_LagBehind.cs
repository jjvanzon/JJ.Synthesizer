namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal abstract class OperatorCalculatorBase_FollowingSampler_2X1Y_LagBehind : OperatorCalculatorBase_FollowingSampler_2X1Y
	{
		protected OperatorCalculatorBase_FollowingSampler_2X1Y_LagBehind(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionCalculator)
			: base(signalCalculator, samplingRateCalculator, positionCalculator) { }

		protected sealed override void SetNextSample()
		{
			_x1 += GetLargeDx();
			_y0 = _signalCalculator.Calculate();
		}

		protected sealed override void SetPreviousSample()
		{
			_x0 -= GetLargeDx();
			_y0 = _signalCalculator.Calculate();
		}
	}
}
