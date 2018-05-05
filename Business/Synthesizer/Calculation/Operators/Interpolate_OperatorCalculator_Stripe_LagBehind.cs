namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal sealed class Interpolate_OperatorCalculator_Stripe_LagBehind
		: Interpolate_OperatorCalculator_Base
	{
		private double _xAtMinusHalf;
		private double _xAtHalf;
		private double _y0;

		public Interpolate_OperatorCalculator_Stripe_LagBehind(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionInputCalculator)
			: base(signalCalculator, samplingRateCalculator, positionInputCalculator)
		{
			ResetNonRecursive();
		}

		protected override bool MustShiftForward(double x) => x > _xAtHalf;

		protected override void ShiftForward()
		{
			double dx = Dx();
			_xAtMinusHalf += dx;
			_xAtHalf += dx;
		}

		protected override void SetNextSample() => SetSample();

		protected override bool MustShiftBackward(double x) => x < _xAtMinusHalf;

		protected override void ShiftBackward()
		{
			double dx = Dx();
			_xAtMinusHalf -= dx;
			_xAtHalf -= dx;
		}

		protected override void SetPreviousSample() => SetSample();

		protected override void Precalculate() { }

		protected override double Calculate(double x) => _y0;

		private void SetSample() => _y0 = _signalCalculator.Calculate();

		protected override void ResetNonRecursive()
		{
			double x = _positionInputCalculator.Calculate();
			double y = _signalCalculator.Calculate();

			double halfDx = Dx() / 2.0;

			_xAtMinusHalf = x - halfDx;
			_xAtHalf = x + halfDx;
			_y0 = y;
		}
	}
}