using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal class SumOverDimension_OperatorCalculator_CollectionRecalculationUponReset : SumOverDimension_OperatorCalculator_Base
	{
		public SumOverDimension_OperatorCalculator_CollectionRecalculationUponReset(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase fromCalculator,
			OperatorCalculatorBase tillCalculator,
			OperatorCalculatorBase stepCalculator,
			OperatorCalculatorBase positionInputCalculator,
			VariableInput_OperatorCalculator positionOutputCalculator)
			: base(signalCalculator, fromCalculator, tillCalculator, stepCalculator, positionInputCalculator, positionOutputCalculator)
		{ }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override void ResetNonRecursive() => RecalculateCollection();
	}
}