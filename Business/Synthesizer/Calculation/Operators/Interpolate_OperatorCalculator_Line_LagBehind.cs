namespace JJ.Business.Synthesizer.Calculation.Operators
{
	/// <summary>
	/// It seems to work, except for the artifacts that linear interpolation gives us.
	/// A weakness though is, that the sampling rate is remembered until the next sample,
	/// which may work poorly when a very low sampling rate is provided.
	/// </summary>
	internal sealed class Interpolate_OperatorCalculator_Line_LagBehind : Interpolate_OperatorCalculator_Base_2Point_LagBehind
	{
		private double _a;

		public Interpolate_OperatorCalculator_Line_LagBehind(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionInputCalculator)
			: base(signalCalculator, samplingRateCalculator, positionInputCalculator)
		    => ResetNonRecursive();

	    protected override void Precalculate()
		{
			double dy = _y1 - _y0;
			_a = dy / Dx();
		}

		protected override double Calculate(double x) => _y0 + _a * (x - _x0);
	}
}