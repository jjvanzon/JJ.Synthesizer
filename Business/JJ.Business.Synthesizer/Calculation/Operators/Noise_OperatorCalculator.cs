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
        private readonly int _dimensionIndex;
        private readonly DimensionStacks _dimensionStack;

        public Noise_OperatorCalculator(
            NoiseCalculator noiseCalculator, 
            double offset,
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack)
        {
            if (noiseCalculator == null) throw new NullException(() => noiseCalculator);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _noiseCalculator = noiseCalculator;
            _offset = offset;
            _dimensionIndex = (int)dimensionEnum;
            _dimensionStack = dimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionIndex);

            double value = _noiseCalculator.GetValue(position + _offset);

            return value;
        }
    }
}
