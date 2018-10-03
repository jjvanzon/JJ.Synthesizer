namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal abstract class Interpolate_OperatorCalculator_Base_4Point_LagBehind : Interpolate_OperatorCalculator_Base_4Point
	{
		protected Interpolate_OperatorCalculator_Base_4Point_LagBehind(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionInputCalculator)
			: base(signalCalculator, samplingRateCalculator, positionInputCalculator) { }

		protected sealed override void SetNextSample()
		{
			_x2 += Dx();
			_y2 = _signalCalculator.Calculate();
		}

		protected sealed override void SetPreviousSample()
		{
			_xMinus1 -= Dx();
			_yMinus1 = _signalCalculator.Calculate();
		}

		protected sealed override void ResetNonRecursive()
		{
			double x = _positionInputCalculator.Calculate();
			double y = _signalCalculator.Calculate();
			double dx = Dx();

			_xMinus1 = x - dx;
			_x0 = x;
			_x1 = x + dx;
			_x2 = x + dx + dx;

			_yMinus1 = y;
			_y0 = y;
			_y1 = y;
			_y2 = y;

			Precalculate();
		}
	}
}