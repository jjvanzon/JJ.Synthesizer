namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal abstract class OperatorCalculatorBase_FollowingSampler_2Point_LagBehind : OperatorCalculatorBase_FollowingSampler_2Point
	{
		protected OperatorCalculatorBase_FollowingSampler_2Point_LagBehind(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionCalculator)
			: base(signalCalculator, samplingRateCalculator, positionCalculator) { }

		protected sealed override void SetNextSample()
		{
			_x1 += GetLargeDx();
			_y1 = _signalCalculator.Calculate();
		}

		protected sealed override void SetPreviousSample()
		{
			_x0 -= GetLargeDx();
			_y0 = _signalCalculator.Calculate();
		}

		protected sealed override void ResetNonRecursive()
		{
		    base.ResetNonRecursive();

			double x = _positionCalculator.Calculate();
			double y = _signalCalculator.Calculate();

			double dx = GetLargeDx();

			_x0 = x - dx;
			_x1 = x;

			_y0 = y;
			_y1 = y;

			Precalculate();
		}
	}
}