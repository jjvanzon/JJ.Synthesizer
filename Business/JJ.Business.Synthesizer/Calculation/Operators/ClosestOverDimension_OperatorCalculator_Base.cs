using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Copies.FromFramework;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal abstract class ClosestOverDimension_OperatorCalculator_Base : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _inputCalculator;
        private readonly OperatorCalculatorBase _collectionCalculator;
        private readonly OperatorCalculatorBase _fromCalculator;
        private readonly OperatorCalculatorBase _tillCalculator;
        private readonly OperatorCalculatorBase _stepCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double[] _sortedItems;

        public ClosestOverDimension_OperatorCalculator_Base(
            OperatorCalculatorBase inputCalculator,
            OperatorCalculatorBase collectionCalculator,
            OperatorCalculatorBase fromCalculator,
            OperatorCalculatorBase tillCalculator,
            OperatorCalculatorBase stepCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] 
            {
                inputCalculator,
                collectionCalculator,
                fromCalculator,
                tillCalculator,
                stepCalculator
            })
        {
            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

            _inputCalculator = inputCalculator;
            _collectionCalculator = collectionCalculator;
            _fromCalculator = fromCalculator;
            _tillCalculator = tillCalculator;
            _stepCalculator = stepCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;

            ResetNonRecursive();
        }

        /// <summary> Just returns _aggregate. </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double input = _inputCalculator.Calculate();

            double valueBefore;
            double valueAfter;

            BinarySearchInexactHighPerformance(_sortedItems, input, out valueBefore, out valueAfter);

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
        public sealed override void Reset()
        {
            base.Reset();

            ResetNonRecursive();
        }

        /// <summary> does nothing </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void ResetNonRecursive()
        { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void RecalculateCollection()
        {
            double from = _fromCalculator.Calculate();
            double till = _tillCalculator.Calculate();
            double step = _stepCalculator.Calculate();

            double length = till - from;

            double countDouble = length / step;

            if (!ConversionHelper.CanCastToNonNegativeInt32(countDouble))
            {
                _sortedItems = new double[0];
            }
            int countInt = (int)countDouble;

            var items = new double[countInt];

            double position = from;

            for (int i = 0; i < countInt; i++)
            {
#if ASSERT_INVAR_INDICES
                OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
#if !USE_INVAR_INDICES
                _dimensionStack.Set(position);
#else
                _dimensionStack.Set(_dimensionStackIndex, position);
#endif
                double item = _collectionCalculator.Calculate();
                items[i] = item;

                position += step;
            }

            Array.Sort(items);

            _sortedItems = items;
        }

        // TODO: Move to framework.
        private void BinarySearchInexactHighPerformance(double[] sortedArray, double input, out double valueBefore, out double valueAfter)
        {
            int length = sortedArray.Length;
            int halfLength = length >> 1;

            int range = halfLength;
            int index = halfLength;
            int previousIndex = -1;

            while (index != previousIndex)
            {
                double sample = sortedArray[index];

                previousIndex = index;
                range = range >> 1;

                if (sample > input)
                {
                    index -= range;
                }
                else
                {
                    index += range;
                }
            }

            valueBefore = sortedArray[index];

            if (index == length - 1)
            {
                valueAfter = valueBefore;
            }
            else
            {
                valueAfter = sortedArray[index + 1];
            }
        }
    }
}
