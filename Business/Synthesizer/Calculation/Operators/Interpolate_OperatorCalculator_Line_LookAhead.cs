namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal sealed class Interpolate_OperatorCalculator_Line_LookAhead : OperatorCalculatorBase_FollowingSampler_2Point_LookAhead
	{
		private double _a;

		public Interpolate_OperatorCalculator_Line_LookAhead(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionCalculator,
			VariableInput_OperatorCalculator positionOutputCalculator)
			: base(signalCalculator, samplingRateCalculator, positionCalculator, positionOutputCalculator)
		    => ResetNonRecursive();

	    protected override void Precalculate()
		{
			double dy = _y1 - _y0;
			_a = dy / GetLargeDx();
		}

		protected override double Calculate(double x) => _y0 + _a * (x - _x0);
	}
}
