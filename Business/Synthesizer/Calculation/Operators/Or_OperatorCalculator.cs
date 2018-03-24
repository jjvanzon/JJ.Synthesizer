using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions.Basic;

// ReSharper disable CompareOfFloatsByEqualityOperator
// ReSharper disable once RedundantIfElseBlock

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal class Or_OperatorCalculator: OperatorCalculatorBase_WithChildCalculators
	{
		private readonly OperatorCalculatorBase _aCalculator;
		private readonly OperatorCalculatorBase _bCalculator;

		public Or_OperatorCalculator(
			OperatorCalculatorBase aCalculator,
			OperatorCalculatorBase bCalculator)
			: base(new[] { aCalculator, bCalculator })
		{
			_aCalculator = aCalculator ?? throw new NullException(() => aCalculator);
			_bCalculator = bCalculator ?? throw new NullException(() => bCalculator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override double Calculate()
		{
			double a = _aCalculator.Calculate();
			double b = _bCalculator.Calculate();

			bool aIsTrue = a != 0.0;
			bool bIsTrue = b != 0.0;

			if (aIsTrue || bIsTrue) return 1.0;
			else return 0.0;
		}
	}
}