using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal class Divide_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
	{
		private readonly OperatorCalculatorBase _aCalculator;
		private readonly OperatorCalculatorBase _bCalculator;

		public Divide_OperatorCalculator(
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

			return a / b;
		}
	}
}
