using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Loop_OperatorCalculator_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration
        : Loop_OperatorCalculator_Base
    {
        private readonly double _loopStartMarker;
        private readonly double _loopEndMarker;
        private readonly double _cycleLength;

        public Loop_OperatorCalculator_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration(
            OperatorCalculatorBase signalCalculator,
            double loopStartMarker,
            double loopEndMarker,
            DimensionStack dimensionStack)
            : base(
                  signalCalculator,
                  dimensionStack,
                  new OperatorCalculatorBase[] { signalCalculator })
        {
            _loopStartMarker = loopStartMarker;
            _loopEndMarker = loopEndMarker;

            _cycleLength = _loopEndMarker - _loopStartMarker;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override double? GetTransformedPosition()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_previousDimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _previousDimensionStackIndex);
#endif
            position -= _origin;

            // BeforeLoop
            double inputPosition = position + _loopStartMarker;
            bool isBeforeLoop = inputPosition < _loopStartMarker;
            if (isBeforeLoop)
            {
                return 0;
            }

            // InLoop
            double phase = (inputPosition - _loopStartMarker) % _cycleLength;
            return phase;
        }
    }
}