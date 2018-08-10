using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal sealed class Interpolate_OperatorCalculator_Stripe_LookAhead
		: OperatorCalculatorBase_FollowingSampler_2X1Y
	{
		private readonly VariableInput_OperatorCalculator _positionOutputCalculator;

		public Interpolate_OperatorCalculator_Stripe_LookAhead(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionCalculator,
			VariableInput_OperatorCalculator positionOutputCalculator)
			: base(signalCalculator, samplingRateCalculator, positionCalculator)
		{
			_positionOutputCalculator = positionOutputCalculator ?? throw new ArgumentNullException(nameof(positionOutputCalculator));
			ResetNonRecursive();
		}

		protected override void SetNextSample()
		{
			_x1 += GetLargeDx();
			SetY0();
		}

		protected override void SetPreviousSample()
		{
			_x0 -= GetLargeDx();
			SetY0();
		}

		private void SetY0()
		{
			// For Stripe interpolation x0 is really xMinusHalf,
			// but for LookAhead we need the actual x0.
			double xMinusHalf = _x0;
			double x0 = xMinusHalf + GetLargeDx() / 2.0;

			double originalValue = _positionOutputCalculator._value;
			_positionOutputCalculator._value = x0;
			_y0 = _signalCalculator.Calculate();
			_positionOutputCalculator._value = originalValue;
		}

		protected override void ResetNonRecursive()
	    {
	        base.ResetNonRecursive();

	        Interpolate_OperatorCalculator_Stripe_Helper.ResetNonRecursive(this);
	    }
	}
}