using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal abstract class SortOverDimension_OperatorCalculator_CollectionRecalculationUponReset 
        : OperatorCalculatorBase_WithChildCalculators
    {
        protected readonly OperatorCalculatorBase _inputCalculator;
        private readonly OperatorCalculatorBase _collectionCalculator;
        private readonly OperatorCalculatorBase _fromCalculator;
        private readonly OperatorCalculatorBase _tillCalculator;
        private readonly OperatorCalculatorBase _stepCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private ArrayCalculator_MinPositionZero_Stripe_NoRate _arrayCalculator;

        public SortOverDimension_OperatorCalculator_CollectionRecalculationUponReset(
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif

            double value = _arrayCalculator.CalculateValue(position);
            return value;
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

            // 0-3 has length 3 in doubles, but length 4 in integers.
            // But adding 1 works for non-integer double values too.
            countDouble += 1;

            if (!ConversionHelper.CanCastToNonNegativeInt32(countDouble))
            {
                _arrayCalculator = new ArrayCalculator_MinPositionZero_Stripe_NoRate(new double[0]);
                return;
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

            _arrayCalculator = new ArrayCalculator_MinPositionZero_Stripe_NoRate(items);
        }
    }
}