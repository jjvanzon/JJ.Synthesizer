using System;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Loop_OperatorCalculator_ManyConstants : Loop_OperatorCalculator_Base
    {
        private readonly double _skip;
        private readonly double _loopStartMarker;
        private readonly double _loopEndMarker;
        private readonly double _releaseEndMarker;
        private readonly OperatorCalculatorBase _noteDurationCalculator;

        private readonly double _cycleLength;
        private readonly double _outputLoopStart;
        private readonly double _releaseLength;

        public Loop_OperatorCalculator_ManyConstants(
            OperatorCalculatorBase signalCalculator,
            double skip,
            double loopStartMarker,
            double loopEndMarker,
            double releaseEndMarker,
            OperatorCalculatorBase noteDurationCalculator,
            DimensionEnum dimensionEnum,
            DimensionStack dimensionStack)
            : base(
                  signalCalculator,
                  dimensionEnum,
                  dimensionStack,
                  new OperatorCalculatorBase[] 
                  {
                      signalCalculator,
                      noteDurationCalculator
                  }.Where(x => x != null).ToArray())
        {
            _skip = skip;
            _loopStartMarker = loopStartMarker;
            _loopEndMarker = loopEndMarker;
            _releaseEndMarker = releaseEndMarker;
            _noteDurationCalculator = noteDurationCalculator;

            _cycleLength = _loopEndMarker - _loopStartMarker;
            _outputLoopStart = _loopStartMarker - _skip;
            _releaseLength = _releaseEndMarker - _loopEndMarker;
        }

        protected override double? TransformPosition()
        {
            double outputPosition = _dimensionStack.Get(_dimensionIndex);

            outputPosition -= _origin;

            // BeforeAttack
            double inputPosition = outputPosition + _skip;
            bool isBeforeAttack = inputPosition < _skip;
            if (isBeforeAttack)
            {
                return null;
            }

            // InAttack
            bool isInAttack = inputPosition < _loopStartMarker;
            if (isInAttack)
            {
                return inputPosition;
            }

            // InLoop
            double noteDuration = GetNoteDuration();

            // Round up end of loop to whole cycles.
            double noteEndPhase = (noteDuration - _outputLoopStart) / _cycleLength;
            double outputLoopEnd = _outputLoopStart + Math.Ceiling(noteEndPhase) * _cycleLength;

            bool isInLoop = outputPosition < outputLoopEnd;
            if (isInLoop)
            {
                double phase = (inputPosition - _loopStartMarker) % _cycleLength;
                inputPosition = _loopStartMarker + phase;
                return inputPosition;
            }

            // InRelease
            double outputReleaseEndPosition = outputLoopEnd + _releaseLength;
            bool isInRelease = outputPosition < outputReleaseEndPosition;
            if (isInRelease)
            {
                double positionInRelease = outputPosition - outputLoopEnd;
                inputPosition = _loopEndMarker + positionInRelease;
                return inputPosition;
            }

            // AfterRelease
            return null;
        }

        // Helpers

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetNoteDuration()
        {
            double value = CalculationHelper.VERY_HIGH_VALUE;
            if (_noteDurationCalculator != null)
            {
                value = _noteDurationCalculator.Calculate();
            }

            return value;
        }
    }
}