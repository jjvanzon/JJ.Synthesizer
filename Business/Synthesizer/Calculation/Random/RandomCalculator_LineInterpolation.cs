using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Framework.Mathematics;

namespace JJ.Business.Synthesizer.Calculation.Random
{
    // TODO: Currently unused. Perhaps remove class.
    internal class RandomCalculator_LineInterpolation : RandomCalculatorBase
    {
        private ArrayCalculator_RotatePosition_Line_NoRate _arrayCalculator;

        public RandomCalculator_LineInterpolation()
        {
            _arrayCalculator = new ArrayCalculator_RotatePosition_Line_NoRate(_samples);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double GetValue(double time)
        {
            return _arrayCalculator.CalculateValue(time);
        }
    }
}

