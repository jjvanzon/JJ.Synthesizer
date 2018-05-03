namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal abstract class Interpolate_OperatorCalculator_Base_4Point_LagBehind : Interpolate_OperatorCalculator_Base_4Point
	{
		protected Interpolate_OperatorCalculator_Base_4Point_LagBehind(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionInputCalculator)
			: base(signalCalculator, samplingRateCalculator, positionInputCalculator) { }

		protected override void SetNextSample()
		{
			_x2 += Dx();
			_y2 = _signalCalculator.Calculate();
		}

		protected override void SetPreviousSample()
		{
			_xMinus1 -= Dx();
			_yMinus1 = _signalCalculator.Calculate();
		}
	}
}