using System;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal static class Interpolate_OperatorCalculator_Stripe_Helper
	{
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ResetNonRecursive(Interpolate_OperatorCalculator_Base_2X1Y calculator)
		{
			if (calculator == null) throw new ArgumentNullException(nameof(calculator));

			double x = calculator._positionInputCalculator.Calculate();
			double y = calculator._signalCalculator.Calculate();

			double halfDx = calculator.Dx() / 2.0;

			calculator._x0 = x - halfDx;
			calculator._x1 = x + halfDx;
			calculator._y0 = y;
		}
	}
}
