using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.SynthesizerPrototype.WithStructs.Calculation
{
	public struct Add_OperatorCalculator_VarArray : IOperatorCalculator_Vars
	{
		private IOperatorCalculator _firstChildCalculator;
		private IOperatorCalculator[] _remainingChildCalculators;
		private int _remainingChildCalculatorsCount;

		public Add_OperatorCalculator_VarArray(IList<IOperatorCalculator> childCalculators)
		{
			if (childCalculators == null) throw new NullException(() => childCalculators);
			if (childCalculators.Count == 0) throw new CollectionEmptyException(() => childCalculators);

			_firstChildCalculator = childCalculators.First();
			_remainingChildCalculators = childCalculators.Skip(1).ToArray();
			_remainingChildCalculatorsCount = _remainingChildCalculators.Length;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double Calculate()
		{
			double sum = _firstChildCalculator.Calculate();

			for (var i = 0; i < _remainingChildCalculatorsCount; i++)
			{
				double value = _remainingChildCalculators[i].Calculate();

				sum += value;
			}

			return sum;
		}

		public void SetVarCalculator(int i, IOperatorCalculator varOperatorCalculator)
		{
			if (i == 0)
			{
				_firstChildCalculator = varOperatorCalculator;
			}
			else if (i > 1)
			{
				int requiredSize = i;
				bool mustIncreaseSize = _remainingChildCalculators.Length < requiredSize;
				if (mustIncreaseSize)
				{

					var newRemainingChildCalculators = new IOperatorCalculator[requiredSize];
					Array.Copy(_remainingChildCalculators, newRemainingChildCalculators, _remainingChildCalculators.Length);
					_remainingChildCalculators = newRemainingChildCalculators;
					_remainingChildCalculatorsCount = _remainingChildCalculators.Length;
				}

				_remainingChildCalculators[i - 1] = varOperatorCalculator;
			}
			else
			{
				throw new Exception($"i {i} not valid.");
			}
		}
	}
}