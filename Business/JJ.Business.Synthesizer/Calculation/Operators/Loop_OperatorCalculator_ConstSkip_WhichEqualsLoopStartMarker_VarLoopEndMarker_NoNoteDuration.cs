using System;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;

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
            DimensionEnum dimensionEnum)
            : base(
                  signalCalculator,
                  dimensionEnum,
                  new OperatorCalculatorBase[] { signalCalculator, loopEndMarkerCalculator })
        {
            _loopStartMarker = loopStartMarker;
            _loopEndMarkerCalculator = loopEndMarkerCalculator;
        }

        protected override double? TransformPosition(DimensionStack dimensionStack)
        {
            double outputPosition = dimensionStack.Get(_dimensionIndex);

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
                dimensionStack.Push(_dimensionIndex, tempPosition);
                _loopEndMarker = GetLoopEndMarker(dimensionStack);
                dimensionStack.Pop(_dimensionIndex);

                _cycleLength = _loopEndMarker - _loopStartMarker;
                _outputCycleEnd += _cycleLength;
            }

            double phase = (inputPosition - _loopStartMarker) % _cycleLength;
            inputPosition = _loopStartMarker + phase;
            return inputPosition;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetLoopEndMarker(DimensionStack dimensionStack)
        {
            double value = 0;
            if (_loopEndMarkerCalculator != null)
            {
                value = _loopEndMarkerCalculator.Calculate(dimensionStack);
            }

            return value;
        }
    }
}