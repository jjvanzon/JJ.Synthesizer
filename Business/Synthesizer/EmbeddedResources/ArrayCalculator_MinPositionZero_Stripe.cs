using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Arrays
{
	internal class ArrayCalculator_MinPositionZero_Stripe : ArrayCalculatorBase_Stripe, ICalculatorWithPosition
	{
		private const double MIN_POSITION = 0.0;

		public ArrayCalculator_MinPositionZero_Stripe(
			double[] array, double rate, double valueBefore, double valueAfter)
			: base(array, rate, MIN_POSITION, valueBefore, valueAfter)
		{ }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public new double Calculate(double position)
		{
			// Return if sample not in range.
			// Execute it on the doubles, to prevent integer overflow later.
			if (position < 0) return _valueBefore;
			if (position > _maxPosition) return _valueAfter;
			if (double.IsNaN(position)) return 0.0;
			if (double.IsInfinity(position)) return 0.0;

			double t = position * _rate;

			return base.Calculate(t);
		}
	}
}
