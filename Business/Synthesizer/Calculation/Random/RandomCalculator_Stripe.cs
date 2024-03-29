﻿using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation.Arrays;

namespace JJ.Business.Synthesizer.Calculation.Random
{
    internal class RandomCalculator_Stripe : RandomCalculatorBase, ICalculatorWithPosition
    {
        private static readonly ArrayCalculator_RotatePosition_Stripe_NoRate _arrayCalculator =
                            new ArrayCalculator_RotatePosition_Stripe_NoRate(RandomCalculatorHelper.Samples);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate(double time) => _arrayCalculator.Calculate(time + _offset);
    }
}
