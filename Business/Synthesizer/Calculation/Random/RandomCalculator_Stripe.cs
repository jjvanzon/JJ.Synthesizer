using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation.Arrays;

namespace JJ.Business.Synthesizer.Calculation.Random
{
    internal class RandomCalculator_Stripe : RandomCalculatorBase, ICalculatorWithPosition
    {
        private readonly ArrayCalculator_RotatePosition_Stripe_NoRate _arrayCalculator;

        public RandomCalculator_Stripe()
        {
            _arrayCalculator = new ArrayCalculator_RotatePosition_Stripe_NoRate(_samples);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate(double time)
        {
            return _arrayCalculator.Calculate(time + _offset);
        }
    }
}
