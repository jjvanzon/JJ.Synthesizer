namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal abstract class OperatorCalculatorBase_FollowingSampler_4Point : OperatorCalculatorBase_FollowingSampler
	{
		protected OperatorCalculatorBase_FollowingSampler_4Point(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionCalculator)
			: base(signalCalculator, samplingRateCalculator, positionCalculator) { }

		protected double _xMinus1;
		protected double _x0;
		protected double _x1;
		protected double _x2;
		protected double _yMinus1;
		protected double _y0;
		protected double _y1;
		protected double _y2;

		protected sealed override void ShiftForward()
		{
			_xMinus1 = _x0;
			_x0 = _x1;
			_x1 = _x2;
			_yMinus1 = _y0;
			_y0 = _y1;
			_y1 = _y2;
		}

		protected sealed override void ShiftBackward()
		{
			_x0 = _xMinus1;
			_x1 = _x0;
			_x2 = _x1;
			_y0 = _yMinus1;
			_y1 = _y0;
			_y2 = _y1;
		}
	}
}