using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal class ChangeTrigger_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
	{
		private readonly OperatorCalculatorBase _calculationCalculator;
		private readonly OperatorCalculatorBase _resetCalculator;

		private double _previousTriggerValue;

		public ChangeTrigger_OperatorCalculator(
			OperatorCalculatorBase calculationCalculator,
			OperatorCalculatorBase resetCalculator)
			: base(new[] { calculationCalculator, resetCalculator })
		{
			_calculationCalculator = calculationCalculator ?? throw new NullException(() => calculationCalculator);
			_resetCalculator = resetCalculator ?? throw new NullException(() => resetCalculator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override double Calculate()
		{
			double newTriggerValue = _resetCalculator.Calculate();

			// ReSharper disable once CompareOfFloatsByEqualityOperator
			if (_previousTriggerValue != newTriggerValue)
			{
				_calculationCalculator.Reset();

				_previousTriggerValue = newTriggerValue;
			}

			return _calculationCalculator.Calculate();
		}
	}
}