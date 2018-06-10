namespace JJ.Business.Synthesizer.Calculation.Operators
{
	/// <summary>
	/// It seems to work, except for the artifacts that linear interpolation gives us.
	/// A weakness though is, that the sampling rate is remembered until the next sample,
	/// which may work poorly when a very low sampling rate is provided.
	/// </summary>
	internal sealed class Interpolate_OperatorCalculator_Line_LookAhead : Interpolate_OperatorCalculator_Base_2Point_LookAhead
	{
		private double _a;

		public Interpolate_OperatorCalculator_Line_LookAhead(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionInputCalculator,
			VariableInput_OperatorCalculator positionOutputCalculator)
			: base(signalCalculator, samplingRateCalculator, positionInputCalculator, positionOutputCalculator)
		    => ResetNonRecursive();

	    protected override void Precalculate()
		{
			double dy = _y1 - _y0;
			_a = dy / Dx();
		}

		protected override double Calculate(double x) => _y0 + _a * (x - _x0);
	}
}
