using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Noise_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly NoiseCalculator _noiseCalculator;
        /// <summary> Each operator should start at a different time offset in the pre-generated noise, to prevent artifacts. </summary>
        private readonly double _offset;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        public Noise_OperatorCalculator(
            NoiseCalculator noiseCalculator,
            double offset,
            DimensionStack dimensionStack)
        {
            if (noiseCalculator == null) throw new NullException(() => noiseCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _noiseCalculator = noiseCalculator;
            _offset = offset;
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

            double value = _noiseCalculator.GetValue(position + _offset);

            return value;
        }
    }
}
