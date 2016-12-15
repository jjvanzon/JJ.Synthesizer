using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Noise_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private readonly NoiseCalculator _noiseCalculator;

        public Noise_OperatorCalculator(NoiseCalculator noiseCalculator, DimensionStack dimensionStack)
        {
            if (noiseCalculator == null) throw new NullException(() => noiseCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _noiseCalculator = noiseCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
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

            double value = _noiseCalculator.GetValue(position);

            return value;
        }

        public override void Reset()
        {
            _noiseCalculator.Reseed();
        }
    }
}
