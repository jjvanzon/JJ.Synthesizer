using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal sealed class Interpolate_OperatorCalculator_Stripe_LookAhead : Interpolate_OperatorCalculator_Base_LookAhead
	{
		private double _xAtMinusHalf;
		private double _xAtHalf;
		private double _y0;

		public Interpolate_OperatorCalculator_Stripe_LookAhead(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionInputCalculator,
			VariableInput_OperatorCalculator positionOutputCalculator)
			: base(signalCalculator, samplingRateCalculator, positionInputCalculator, positionOutputCalculator)
		{
			ResetNonRecursive();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override double Calculate()
		{
			double x = _positionInputCalculator.Calculate();

			// TODO: What if _x1 is way off? How will it correct itself?
			if (x > _xAtHalf)
			{
				double dx = Dx();

				_xAtMinusHalf += dx;
				_xAtHalf += dx;

				double x0 = _xAtMinusHalf + dx / 2.0;

				double originalValue = _positionOutputCalculator._value;
				_positionOutputCalculator._value = x0;

				_y0 = _signalCalculator.Calculate();

				_positionOutputCalculator._value = originalValue;

			}
			else if (x < _xAtMinusHalf)
			{
				double dx = Dx();

				_xAtMinusHalf -= dx;
				_xAtHalf -= dx;


				double x0 = _xAtMinusHalf + dx / 2.0;

				double originalValue = _positionOutputCalculator._value;
				_positionOutputCalculator._value = x0;

				_y0 = _signalCalculator.Calculate();

				_positionOutputCalculator._value = originalValue;
			}

			return _y0;
		}

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