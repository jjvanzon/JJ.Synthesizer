using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SortOverDimension_OperatorCalculator_CollectionRecalculationContinuous
        : SortOverDimension_OperatorCalculator_Base
    {
        public SortOverDimension_OperatorCalculator_CollectionRecalculationContinuous(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase fromCalculator,
            OperatorCalculatorBase tillCalculator,
            OperatorCalculatorBase stepCalculator,
            DimensionStack dimensionStack)
            : base(signalCalculator, fromCalculator, tillCalculator, stepCalculator, dimensionStack)
        { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            RecalculateCollection();

#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
            // Return if sample not in range.
            // Execute it on the doubles, to prevent integer overflow later.
            if (position < 0.0) return 0.0;
            if (position > _items.Length) return 0.0;
            if (Double.IsNaN(position)) return 0.0;
            if (Double.IsInfinity(position)) return 0.0;

            // Stripe interpolation
            position += 0.5;

            int i = (int)position;

            double value = _items[i];
            return value;
        }
    }
}