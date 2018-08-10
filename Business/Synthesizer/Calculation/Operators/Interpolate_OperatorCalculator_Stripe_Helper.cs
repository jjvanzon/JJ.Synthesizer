using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal static class Interpolate_OperatorCalculator_Stripe_Helper
	{
		public static void ResetNonRecursive(OperatorCalculatorBase_FollowingSampler_2X1Y calculator)
		{
			if (calculator == null) throw new ArgumentNullException(nameof(calculator));

			double x = calculator._positionCalculator.Calculate();
			double y = calculator._signalCalculator.Calculate();

			double halfDx = calculator.GetLargeDx() / 2.0;

			calculator._x0 = x - halfDx;
			calculator._x1 = x + halfDx;
			calculator._y0 = y;
		}
	}
}
