using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal class Multiply_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
	{
		private readonly OperatorCalculatorBase _firstOperandCalculator;
		private readonly OperatorCalculatorBase[] _remainingOperandCalculators;
		private readonly int _remainingOperandCalculatorsCount;

		public Multiply_OperatorCalculator(IList<OperatorCalculatorBase> operandCalculators)
			: base(operandCalculators)
		{
			if (operandCalculators == null) throw new NullException(() => operandCalculators);
			if (operandCalculators.Count == 0) throw new CollectionEmptyException(() => operandCalculators);

			_firstOperandCalculator = operandCalculators.First();
			_remainingOperandCalculators = operandCalculators.Skip(1).ToArray();
			_remainingOperandCalculatorsCount = _remainingOperandCalculators.Length;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override double Calculate()
		{
			double product = _firstOperandCalculator.Calculate();

			for (int i = 0; i < _remainingOperandCalculatorsCount; i++)
			{
				double value = _remainingOperandCalculators[i].Calculate();

				product *= value;
			}

			return product;
		}
	}
}