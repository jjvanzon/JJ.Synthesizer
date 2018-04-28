using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal class LessThan_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
	{
		private readonly OperatorCalculatorBase _calculatorA;
		private readonly OperatorCalculatorBase _calculatorB;

		public LessThan_OperatorCalculator(
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

			return a < b ? 1.0 : 0.0;
		}
	}
}