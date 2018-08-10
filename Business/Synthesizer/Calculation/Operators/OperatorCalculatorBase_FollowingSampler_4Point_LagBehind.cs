namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal abstract class OperatorCalculatorBase_FollowingSampler_4Point_LagBehind : OperatorCalculatorBase_FollowingSampler_4Point
	{
		protected OperatorCalculatorBase_FollowingSampler_4Point_LagBehind(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionCalculator)
			: base(signalCalculator, samplingRateCalculator, positionCalculator) { }

		protected sealed override void SetNextSample()
		{
			_x2 += GetLargeDx();
			_y2 = _signalCalculator.Calculate();
		}

		protected sealed override void SetPreviousSample()
		{
			_xMinus1 -= GetLargeDx();
			_yMinus1 = _signalCalculator.Calculate();
		}

		protected sealed override void ResetNonRecursive()
		{
		    base.ResetNonRecursive();

		    double x = _positionCalculator.Calculate();
			double y = _signalCalculator.Calculate();
			double dx = GetLargeDx();

			_xMinus1 = x - dx - dx;
			_x0 = x - dx;
			_x1 = x;
			_x2 = x + dx;

			_yMinus1 = y;
			_y0 = y;
			_y1 = y;
			_y2 = y;

			Precalculate();
		}
	}
}