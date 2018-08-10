namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal sealed class Interpolate_OperatorCalculator_Line_LagBehind : OperatorCalculatorBase_FollowingSampler_2Point_LagBehind
	{
		private double _a;

		public Interpolate_OperatorCalculator_Line_LagBehind(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionCalculator)
			: base(signalCalculator, samplingRateCalculator, positionCalculator)
		    => ResetNonRecursive();

	    protected override void Precalculate()
		{
			double dy = _y1 - _y0;
			_a = dy / GetLargeDx();
		}

		protected override double Calculate(double x) => _y0 + _a * (x - _x0);
	}
}