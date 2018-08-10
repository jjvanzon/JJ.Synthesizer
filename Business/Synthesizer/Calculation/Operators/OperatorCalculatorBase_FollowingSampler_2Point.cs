namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal abstract class OperatorCalculatorBase_FollowingSampler_2Point : OperatorCalculatorBase_FollowingSampler
	{
		protected OperatorCalculatorBase_FollowingSampler_2Point(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionCalculator)
			: base(signalCalculator, samplingRateCalculator, positionCalculator) { }

		protected double _x0;
		protected double _x1;
		protected double _y0;
		protected double _y1;

		protected sealed override void ShiftForward()
		{
			_x0 = _x1;
			_y0 = _y1;
		}

		protected sealed override void ShiftBackward()
		{
			_x1 = _x0;
			_y1 = _y0;
		}
	}
}