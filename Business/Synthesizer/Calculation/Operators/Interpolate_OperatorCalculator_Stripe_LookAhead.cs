using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal sealed class Interpolate_OperatorCalculator_Stripe_LookAhead 
		: Interpolate_OperatorCalculator_Base

	{
		private readonly VariableInput_OperatorCalculator _positionOutputCalculator;

		private double _xAtMinusHalf;
		private double _xAtHalf;
		private double _y0;

		public Interpolate_OperatorCalculator_Stripe_LookAhead(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionInputCalculator,
			VariableInput_OperatorCalculator positionOutputCalculator)
			: base(signalCalculator, samplingRateCalculator, positionInputCalculator)
		{
			_positionOutputCalculator = positionOutputCalculator ?? throw new ArgumentNullException(nameof(positionOutputCalculator));
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

		private void SetSample()
		{
			double dx = Dx();

			double x0 = _xAtMinusHalf + dx / 2.0;

			double originalValue = _positionOutputCalculator._value;
			_positionOutputCalculator._value = x0;

			_y0 = _signalCalculator.Calculate();

			_positionOutputCalculator._value = originalValue;
		}

		protected override void Precalculate() { }

		protected override double Calculate(double x) => _y0;

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