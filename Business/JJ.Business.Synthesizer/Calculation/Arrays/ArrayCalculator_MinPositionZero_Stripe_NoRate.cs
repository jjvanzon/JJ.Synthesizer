﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Arrays
{
    internal class ArrayCalculator_MinPositionZero_Stripe_NoRate : ArrayCalculatorBase_Stripe
    {
        private const double MIN_POSITION = 0.0;
        private const double RATE = 1.0;

        public ArrayCalculator_MinPositionZero_Stripe_NoRate(
            double[] array) 
            : base(array, RATE, MIN_POSITION)
        { }

        public ArrayCalculator_MinPositionZero_Stripe_NoRate(
            double[] array, double valueBefore, double valueAfter)
            : base(array, RATE, MIN_POSITION, valueBefore, valueAfter)
        { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double CalculateValue(double position)
        {
            // Return if sample not in range.
            // Execute it on the doubles, to prevent integer overflow later.
            if (position < 0) return _valueBefore;
            if (position > _maxPosition) return _valueAfter;
            if (Double.IsNaN(position)) return 0.0;
            if (Double.IsInfinity(position)) return 0.0;

            return base.CalculateValue(position);
        }
    }
}