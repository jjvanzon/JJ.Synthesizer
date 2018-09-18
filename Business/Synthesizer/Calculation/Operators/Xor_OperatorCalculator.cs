using System.Runtime.CompilerServices;

// ReSharper disable CompareOfFloatsByEqualityOperator
// ReSharper disable once RedundantIfElseBlock

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal class Xor_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
	{
		private readonly OperatorCalculatorBase _aCalculator;
		private readonly OperatorCalculatorBase _bCalculator;

		public Xor_OperatorCalculator(
			OperatorCalculatorBase aCalculator,
			OperatorCalculatorBase bCalculator)
			: base(new[] { aCalculator, bCalculator })
		{
			_aCalculator = aCalculator;
			_bCalculator = bCalculator;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override double Calculate()
		{
			double a = _aCalculator.Calculate();
			double b = _bCalculator.Calculate();

			bool aIsTrue = a != 0.0;
			bool bIsTrue = b != 0.0;

			if (aIsTrue ^ bIsTrue) return 1.0;
			else return 0.0;
		}
	}
}