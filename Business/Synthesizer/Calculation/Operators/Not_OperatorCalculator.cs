using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal class Not_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
	{
		private readonly OperatorCalculatorBase _numberCalculator;

		public Not_OperatorCalculator(OperatorCalculatorBase numberCalculator)
			: base(new[] { numberCalculator })
		{
			_numberCalculator = numberCalculator ?? throw new NullException(() => numberCalculator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override double Calculate()
		{
			double number = _numberCalculator.Calculate();

			// ReSharper disable once CompareOfFloatsByEqualityOperator
			bool isFalse = number == 0.0;

			if (isFalse) return 1.0;
			else return 0.0;
		}
	}
}