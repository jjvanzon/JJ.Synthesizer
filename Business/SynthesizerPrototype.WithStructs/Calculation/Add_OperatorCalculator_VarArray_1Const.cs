using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions;

namespace JJ.Business.SynthesizerPrototype.WithStructs.Calculation
{
	public struct Add_OperatorCalculator_VarArray_1Const : IOperatorCalculator_Vars
	{
		private IOperatorCalculator[] _varCalculators;
		private int _varCalculatorsCount;
		private readonly double _constValue;

		public Add_OperatorCalculator_VarArray_1Const(IList<IOperatorCalculator> operandCalculators, double constValue)
		{
			if (operandCalculators == null) throw new NullException(() => operandCalculators);
			if (operandCalculators.Count == 0) throw new CollectionEmptyException(() => operandCalculators);

			_varCalculators = operandCalculators.ToArray();
			_varCalculatorsCount = _varCalculators.Length;
			_constValue = constValue;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double Calculate()
		{
			double sum = _constValue;

			for (int i = 0; i < _varCalculatorsCount; i++)
			{
				double value = _varCalculators[i].Calculate();

				sum += value;
			}

			return sum;
		}

		public void SetVarCalculator(int i, IOperatorCalculator varOperatorCalculator)
		{
			if (i >= 0)
			{
				int requiredSize = i + 1;
				bool mustIncreaseSize = _varCalculators.Length < requiredSize;
				if (mustIncreaseSize)
				{
					var newVarCalculators = new IOperatorCalculator[requiredSize];
					Array.Copy(_varCalculators, newVarCalculators, _varCalculators.Length);
					_varCalculators = newVarCalculators;
					_varCalculatorsCount = _varCalculators.Length;
				}

				_varCalculators[i - 1] = varOperatorCalculator;
			}
			else
			{
				throw new Exception($"i {i} not valid.");
			}
		}
	}
}