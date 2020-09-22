using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Arrays
{
	internal class ArrayCalculator_RotatePosition_Hermite : ArrayCalculatorBase_Hermite, ICalculatorWithPosition
	{
		private const double DEFAULT_MIN_POSITION = 0;

		public ArrayCalculator_RotatePosition_Hermite(
			double[] array, double rate) 
			: base(array, rate, DEFAULT_MIN_POSITION, valueBefore: 0.0, valueAfter: 0.0)
		{ }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public new double Calculate(double position)
		{
			if (double.IsNaN(position)) return 0.0;
			if (double.IsInfinity(position)) return 0.0;

			double transformedPosition = position % _length;

			// Account for negative positions.
			if (transformedPosition < 0.0)
			{
				transformedPosition += _length;
			}

			transformedPosition *= _rate;

			return base.Calculate(transformedPosition);
		}
	}
}
