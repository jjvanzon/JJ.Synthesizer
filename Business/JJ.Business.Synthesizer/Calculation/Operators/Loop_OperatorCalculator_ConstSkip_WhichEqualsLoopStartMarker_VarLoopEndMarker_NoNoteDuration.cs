using System;
using System.Linq;
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
            double outputPosition = _dimensionStack.Get(_previousDimensionStackIndex);

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
                _dimensionStack.Set(_currentDimensionStackIndex, tempPosition);

                _loopEndMarker = GetLoopEndMarker();

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