namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal sealed class Interpolate_OperatorCalculator_Block : Interpolate_OperatorCalculator_Base
	{
		private double _x0;
		private double _x1;
		private double _y0;

		public Interpolate_OperatorCalculator_Block(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionInputCalculator)
			: base(signalCalculator, samplingRateCalculator, positionInputCalculator)
		{
			ResetNonRecursive();
		}

		protected override bool MustShiftForward(double x) => x > _x1;

		protected override void ShiftForward() => _x0 = _x1;

		protected override void SetNextSample()
		{
			_x1 += Dx();
			_y0 = _signalCalculator.Calculate();
		}

		protected override bool MustShiftBackward(double x) => x < _x0;

		protected override void ShiftBackward() => _x1 = _x0;

		protected override void SetPreviousSample()
		{
			_x0 -= Dx();
			_y0 = _signalCalculator.Calculate();
		}

		protected override void Precalculate() { }

		protected override double Calculate(double x) => _y0;

		protected override void ResetNonRecursive()
		{
			double x = _positionInputCalculator.Calculate();
			double y = _signalCalculator.Calculate();

			_x0 = x;
			_x1 = x + Dx();

			_y0 = y;
		}
	}
}