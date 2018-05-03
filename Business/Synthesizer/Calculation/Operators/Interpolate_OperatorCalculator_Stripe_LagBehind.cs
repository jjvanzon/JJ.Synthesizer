using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal sealed class Interpolate_OperatorCalculator_Stripe_LagBehind : Interpolate_OperatorCalculator_Base
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

				_y0 = _signalCalculator.Calculate();
			}
			else if (x < _xAtMinusHalf)
			{
				double dx = Dx();

				_xAtMinusHalf -= dx;
				_xAtHalf -= dx;

				_y0 = _signalCalculator.Calculate();
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