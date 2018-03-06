using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal class If_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
	{
		private readonly OperatorCalculatorBase _conditionCalculator;
		private readonly OperatorCalculatorBase _thenCalculator;
		private readonly OperatorCalculatorBase _elseCalculator;

		public If_OperatorCalculator(
			OperatorCalculatorBase conditionCalculator,
			OperatorCalculatorBase thenCalculator,
			OperatorCalculatorBase elseCalculator)
			: base(new[] { conditionCalculator, thenCalculator, elseCalculator })
		{
			_conditionCalculator = conditionCalculator ?? throw new NullException(() => conditionCalculator);
			_thenCalculator = thenCalculator ?? throw new NullException(() => thenCalculator);
			_elseCalculator = elseCalculator ?? throw new NullException(() => elseCalculator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override double Calculate()
		{
			double condition = _conditionCalculator.Calculate();

			// ReSharper disable once CompareOfFloatsByEqualityOperator
			bool isTrue = condition != 0.0;

			return isTrue ? _thenCalculator.Calculate() : _elseCalculator.Calculate();
		}
	}
}
