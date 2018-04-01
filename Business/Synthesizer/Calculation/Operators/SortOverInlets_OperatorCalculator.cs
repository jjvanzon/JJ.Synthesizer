using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Collections;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal class SortOverInlets_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
	{
		private readonly OperatorCalculatorBase[] _itemCalculators;
		private readonly double[] _items;
		private readonly double _maxIndexDouble;
		private readonly int _itemCount;

		private readonly OperatorCalculatorBase _positionCalculator;

		public SortOverInlets_OperatorCalculator(
			IList<OperatorCalculatorBase> itemCalculators,
			OperatorCalculatorBase positionCalculator) 
			: base(itemCalculators.Union(positionCalculator).ToArray())
		{
			_positionCalculator = positionCalculator;

			_itemCalculators = itemCalculators.ToArray();
			_itemCount = itemCalculators.Count;
			_maxIndexDouble = _itemCount - 1;
			_items = new double[_itemCount];
		}

		public override double Calculate()
		{
			double position = _positionCalculator.Calculate();

			if (!CalculationHelper.CanCastToNonNegativeInt32WithMax(position, _maxIndexDouble))
			{
				return 0.0;
			}

			for (int i = 0; i < _itemCount; i++)
			{
				_items[i] = _itemCalculators[i].Calculate();
			}

			Array.Sort(_items);
			
			double item = _items[(int)position];

			return item;
		}
	}
}
