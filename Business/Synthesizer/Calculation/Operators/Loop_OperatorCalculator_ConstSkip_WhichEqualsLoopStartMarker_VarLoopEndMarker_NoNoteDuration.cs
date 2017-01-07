using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Loop_OperatorCalculator_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration
        : Loop_OperatorCalculator_Base
    {
        private readonly double _loopStartMarker;
        private readonly OperatorCalculatorBase _loopEndMarkerCalculator;

        private double _outputCycleEnd;
        private double _loopEndMarker;
        private double _cycleLength;

        public Loop_OperatorCalculator_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration(
            OperatorCalculatorBase signalCalculator,
            double loopStartMarker,
            OperatorCalculatorBase loopEndMarkerCalculator,
            DimensionStack dimensionStack)
            : base(
                  signalCalculator,
                  dimensionStack,
                  new OperatorCalculatorBase[] { signalCalculator, loopEndMarkerCalculator })
        {
            _loopStartMarker = loopStartMarker;
            _loopEndMarkerCalculator = loopEndMarkerCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override double? GetTransformedPosition()
        {
#if !USE_INVAR_INDICES
            double outputPosition = _dimensionStack.Get();
#else
            double outputPosition = _dimensionStack.Get(_previousDimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _previousDimensionStackIndex);
#endif

            double tempPosition = outputPosition - _origin;

            // BeforeLoop
            double inputPosition = tempPosition + _loopStartMarker;
            bool isBeforeLoop = inputPosition < _loopStartMarker;
            if (isBeforeLoop)
            {
                return null;
            }

            // InLoop
            if (tempPosition > _outputCycleEnd)
            {
#if !USE_INVAR_INDICES
                _dimensionStack.Push(tempPosition);
#else
                _dimensionStack.Set(_nextDimensionStackIndex, tempPosition);
#endif
#if ASSERT_INVAR_INDICES
                OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _nextDimensionStackIndex);
#endif

                _loopEndMarker = GetLoopEndMarker();

#if !USE_INVAR_INDICES
                _dimensionStack.Pop();
#endif
                _cycleLength = _loopEndMarker - _loopStartMarker;
                _outputCycleEnd += _cycleLength;
            }

            double phase = (inputPosition - _loopStartMarker) % _cycleLength;
            inputPosition = _loopStartMarker + phase;
            return inputPosition;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetLoopEndMarker()
        {
            double value = 0;
            if (_loopEndMarkerCalculator != null)
            {
                value = _loopEndMarkerCalculator.Calculate();
            }

            return value;
        }
    }
}