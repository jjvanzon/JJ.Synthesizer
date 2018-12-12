using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Arrays
{
	internal abstract class ArrayCalculatorBase_Stripe : ArrayCalculatorBase
	{
		private const int EXTRA_TICKS_BEFORE = 0;
		private const int EXTRA_TICKS_AFTER = 1;

		public ArrayCalculatorBase_Stripe(
			double[] array, double rate, double minPosition, double valueBefore, double valueAfter)
			: base(array, rate, minPosition, EXTRA_TICKS_BEFORE, EXTRA_TICKS_AFTER, valueBefore, valueAfter)
		{ }

		/// <summary> Base method does not check bounds or transform position from 'seconds to samples'. </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected double Calculate(double x)
		{
			x += 0.5;

			var x0 = (int)x;

			double value = _array[x0];
			return value;
		}
	}
}