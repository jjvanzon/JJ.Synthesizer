using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal class ClosestOverInlets_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
	{
		private readonly OperatorCalculatorBase _inputCalculator;
		private readonly OperatorCalculatorBase _firstItemCalculators;
		private readonly OperatorCalculatorBase[] _remainingItemCalculators;
		private readonly int _remainingItemCalculatorsCount;

		public ClosestOverInlets_OperatorCalculator(
			OperatorCalculatorBase inputCalculator,
			// ReSharper disable once SuggestBaseTypeForParameter
			IList<OperatorCalculatorBase> itemCalculators)
			: base(new List<OperatorCalculatorBase>(itemCalculators) { inputCalculator })
		{
			if (itemCalculators == null) throw new NullException(() => itemCalculators);
			if (itemCalculators.Count < 1) throw new LessThanException(() => itemCalculators.Count, 1);

			_inputCalculator = inputCalculator ?? throw new NullException(() => inputCalculator);
			_firstItemCalculators = itemCalculators.First();
			_remainingItemCalculators = itemCalculators.Skip(1).ToArray();
			_remainingItemCalculatorsCount = _remainingItemCalculators.Length;
		}

		public override double Calculate()
		{
			double input = _inputCalculator.Calculate();
			double firstItem = _firstItemCalculators.Calculate();

			double smallestDistance = Geometry.AbsoluteDistance(input, firstItem);
			double closestItem = firstItem;

			for (int i = 0; i < _remainingItemCalculatorsCount; i++)
			{
				OperatorCalculatorBase itemCalculator = _remainingItemCalculators[i];
				double item = itemCalculator.Calculate();

				double distance = Geometry.AbsoluteDistance(input, item);
				// ReSharper disable once InvertIf
				if (smallestDistance > distance)
				{
					smallestDistance = distance;
					closestItem = item;
				}
			}

			return closestItem;
		}
	}
}