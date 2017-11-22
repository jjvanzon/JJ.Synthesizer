using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal class AverageFollower_OperatorCalculator : SumFollower_OperatorCalculator
	{
		public AverageFollower_OperatorCalculator(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase sliceLengthCalculator,
			OperatorCalculatorBase sampleCountCalculator,
			OperatorCalculatorBase positionCalculator)
			: base(signalCalculator, sliceLengthCalculator, sampleCountCalculator, positionCalculator)
		{ }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override double PostProcessAggregate() => _sum / _sampleCountDouble;
	}
}