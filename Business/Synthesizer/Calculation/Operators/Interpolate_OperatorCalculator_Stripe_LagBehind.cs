using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal class Interpolate_OperatorCalculator_Stripe_LagBehind : Interpolate_OperatorCalculator_Base
	{
		private double _xAtMinusHalf;
		private double _x0;
		private double _xAtHalf;
		private double _y0;

		public Interpolate_OperatorCalculator_Stripe_LagBehind(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionInputCalculator,
			VariableInput_OperatorCalculator positionOutputCalculator)
			: base(signalCalculator, samplingRateCalculator, positionInputCalculator, positionOutputCalculator)
		{ }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override double Calculate()
		{
			double x = _positionInputCalculator.Calculate();
	 
			// TODO: What if _x1 is way off? How will it correct itself?
			if (x > _xAtHalf)
			{
				double originalValue = _positionOutputCalculator._value;
				_positionOutputCalculator._value = _x0;
				double samplingRate0 = GetSamplingRate();
				_positionOutputCalculator._value = originalValue;

				double dx0 = 1.0 / samplingRate0;
				_xAtMinusHalf += dx0;
				_x0 += dx0;
				_xAtHalf += dx0;

				_y0 = _signalCalculator.Calculate();
			}
			else if (x < _xAtMinusHalf)
			{
				double originalValue = _positionOutputCalculator._value;
				_positionOutputCalculator._value = _x0;
				double samplingRate0 = GetSamplingRate();
				_positionOutputCalculator._value = originalValue;

				double dx0 = 1.0 / samplingRate0;
				_xAtMinusHalf -= dx0;
				_x0 -= dx0;
				_xAtHalf -= dx0;

				_y0 = _signalCalculator.Calculate();
			}

			return _y0;
		}

		protected override void ResetNonRecursive()
		{
			double x = _positionInputCalculator.Calculate();
			double y = _signalCalculator.Calculate();
			double samplingRate = GetSamplingRate();

			double dx = 1.0 / samplingRate;
			double halfDx = dx / 2.0;

			_x0 = x;
			_xAtMinusHalf = x - halfDx;
			_xAtHalf = x + halfDx;
			_y0 = y;
		}
	}
}