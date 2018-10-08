namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal abstract class Interpolate_OperatorCalculator_Base_2X1Y_LagBehind 
	    : Interpolate_OperatorCalculator_Base_2X1Y
	{
		protected Interpolate_OperatorCalculator_Base_2X1Y_LagBehind(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionInputCalculator)
			: base(signalCalculator, samplingRateCalculator, positionInputCalculator) { }

		protected sealed override void SetNextSample()
		{
			_x1 += Dx();
			_y0 = _signalCalculator.Calculate();
		}

		protected sealed override void SetPreviousSample()
		{
			_x0 -= Dx();
			_y0 = _signalCalculator.Calculate();
		}
	}
}
