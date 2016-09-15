using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;
using JJ.Framework.Common;

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
            DimensionStack dimensionStack)
            : base(
                collectionCalculator,
                fromCalculator,
                tillCalculator,
                stepCalculator,
                dimensionStack)
        {
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _inputCalculator = inputCalculator;

            _childOperatorCalculators = _childOperatorCalculators.Union(inputCalculator).ToArray();

            ResetNonRecursive();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double input = _inputCalculator.Calculate();

            double valueBefore;
            double valueAfter;

            CollectionHelper.BinarySearchInexact(
                _sortedItems,
                _halfCount,
                _min,
                _max,
                input,
                out valueBefore,
                out valueAfter);

            double distanceBefore = Geometry.AbsoluteDistance(input, valueBefore);
            double distanceAfter = Geometry.AbsoluteDistance(input, valueAfter);

            if (distanceBefore <= distanceAfter)
            {
                return valueBefore;
            }
            else
            {
                return valueAfter;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override bool RecalculateCollection()
        {
            bool success = base.RecalculateCollection();
            if (success)
            {
                _min = _sortedItems[0];
                _max = _sortedItems[_countInt - 1];
                _halfCount = _countInt >> 1;
            }
            else
            {
                _min = 0;
                _max = 0;
                _halfCount = 0;
            }

            return success;
        }
    }
}