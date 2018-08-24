using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Collections;
using JJ.Framework.Mathematics;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal abstract class ClosestOverDimension_OperatorCalculator_Base
		: SortOverDimension_OperatorCalculator_Base
	{
		protected readonly OperatorCalculatorBase _inputCalculator;

		protected double _min;
		protected double _max;
		protected int _halfCount;

		public ClosestOverDimension_OperatorCalculator_Base(
			OperatorCalculatorBase inputCalculator,
			OperatorCalculatorBase collectionCalculator,
			OperatorCalculatorBase fromCalculator,
			OperatorCalculatorBase tillCalculator,
			OperatorCalculatorBase stepCalculator,
			OperatorCalculatorBase positionInputCalculator,
			VariableInput_OperatorCalculator positionOutputCalculator)
			: base(collectionCalculator, fromCalculator, tillCalculator, stepCalculator, positionInputCalculator, positionOutputCalculator)
		{
			_inputCalculator = inputCalculator;

			_childOperatorCalculators = _childOperatorCalculators.Concat(inputCalculator).ToArray();

			// ReSharper disable once VirtualMemberCallInConstructor
			ResetNonRecursive();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override double Calculate()
		{
			double input = _inputCalculator.Calculate();


			CollectionHelper.BinarySearchInexact(
				_samples,
				_halfCount,
				_min,
				_max,
				input,
				out double valueBefore,
				out double valueAfter);

			double distanceBefore = Geometry.AbsoluteDistance(input, valueBefore);
			double distanceAfter = Geometry.AbsoluteDistance(input, valueAfter);

			return distanceBefore <= distanceAfter ? valueBefore : valueAfter;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override void RecalculateCollection()
		{
			base.RecalculateCollection();

			if (_count != 0)
			{
				_min = _samples[0];
				_max = _samples[_count - 1];
				_halfCount = _count >> 1;
			}
			else
			{
				_min = 0;
				_max = 0;
				_halfCount = 0;
			}
		}
	}
}