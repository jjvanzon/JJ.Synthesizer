using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions.Basic;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal class And_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
	{
		private readonly OperatorCalculatorBase _calculatorA;
		private readonly OperatorCalculatorBase _calculatorB;

		public And_OperatorCalculator(
			OperatorCalculatorBase calculatorA,
			OperatorCalculatorBase calculatorB)
			: base(new[] { calculatorA, calculatorB })
		{
			_calculatorA = calculatorA ?? throw new NullException(() => calculatorA);
			_calculatorB = calculatorB ?? throw new NullException(() => calculatorB);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override double Calculate()
		{
			double a = _calculatorA.Calculate();
			double b = _calculatorB.Calculate();

			bool aIsTrue = a != 0.0;
			bool bIsTrue = b != 0.0;

			if (aIsTrue && bIsTrue) return 1.0;
			else return 0.0;
		}
	}
}