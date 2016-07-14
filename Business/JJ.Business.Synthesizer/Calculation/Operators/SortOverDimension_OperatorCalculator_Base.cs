using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal abstract class SortOverDimension_OperatorCalculator_Base 
        : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _fromCalculator;
        private readonly OperatorCalculatorBase _tillCalculator;
        private readonly OperatorCalculatorBase _stepCalculator;
        protected readonly DimensionStack _dimensionStack;
        protected readonly int _dimensionStackIndex;
        protected double[] _items;

        public SortOverDimension_OperatorCalculator_Base(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase fromCalculator,
            OperatorCalculatorBase tillCalculator,
            OperatorCalculatorBase stepCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator,
                fromCalculator,
                tillCalculator,
                stepCalculator
            })
        {
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _signalCalculator = signalCalculator;
            _fromCalculator = fromCalculator;
            _tillCalculator = tillCalculator;
            _stepCalculator = stepCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;

            ResetNonRecursive();
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
#if !USE_INVAR_INDICES
            double originalPosition = _dimensionStack.Get();
#else
            double originalPosition = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
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
                _items = new double[0];
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
                double item = _signalCalculator.Calculate();
                items[i] = item;

                position += step;
            }

            Array.Sort(items);

#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
#if !USE_INVAR_INDICES
            _dimensionStack.Set(originalPosition);
#else
            _dimensionStack.Set(_dimensionStackIndex, originalPosition);
#endif
            _items = items;
        }
    }
}