using System;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal class Interpolate_OperatorCalculator_Stripe_LookAhead : Interpolate_OperatorCalculator_Base_LookAhead
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
		{ }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override double Calculate()
		{
			throw new NotImplementedException();

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
			throw new NotImplementedException();

			double x = _positionInputCalculator.Calculate();
			double y = _signalCalculator.Calculate();

			double halfDx = Dx() / 2.0;

			_xAtMinusHalf = x - halfDx;
			_xAtHalf = x + halfDx;
			_y0 = y;
		}
	}
}