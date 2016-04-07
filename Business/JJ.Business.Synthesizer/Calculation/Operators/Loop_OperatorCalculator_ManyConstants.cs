using System;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Loop_OperatorCalculator_ManyConstants : Loop_OperatorCalculator_Base
    {
        private readonly double _skip;
        private readonly double _loopStartMarker;
        private readonly double _loopEndMarker;
        private readonly double _releaseEndMarker;
        private readonly OperatorCalculatorBase _noteDurationCalculator;

        private readonly double _cycleDuration;
        private readonly double _outputLoopStart;
        private readonly double _releaseDuration;

        public Loop_OperatorCalculator_ManyConstants(
            OperatorCalculatorBase signalCalculator,
            double skip,
            double loopStartMarker,
            double loopEndMarker,
            double releaseEndMarker,
            OperatorCalculatorBase noteDurationCalculator)
            : base(
                  signalCalculator,
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

            _cycleDuration = _loopEndMarker - _loopStartMarker;
            _outputLoopStart = _loopStartMarker - _skip;
            _releaseDuration = _releaseEndMarker - _loopEndMarker;
        }

        protected override double? TransformTime(double outputTime, int channelIndex)
        {
            outputTime -= _origin;

            // BeforeAttack
            double inputTime = outputTime + _skip;
            bool isBeforeAttack = inputTime < _skip;
            if (isBeforeAttack)
            {
                return null;
            }

            // InAttack
            bool isInAttack = inputTime < _loopStartMarker;
            if (isInAttack)
            {
                return inputTime;
            }

            // InLoop
            double noteDuration = GetNoteDuration(outputTime, channelIndex);

            // Round up end of loop to whole cycles.
            double noteEndPhase = (noteDuration - _outputLoopStart) / _cycleDuration;
            double outputLoopEnd = _outputLoopStart + Math.Ceiling(noteEndPhase) * _cycleDuration;

            bool isInLoop = outputTime < outputLoopEnd;
            if (isInLoop)
            {
                double phase = (inputTime - _loopStartMarker) % _cycleDuration;
                inputTime = _loopStartMarker + phase;
                return inputTime;
            }

            // InRelease
            double outputReleaseEndTime = outputLoopEnd + _releaseDuration;
            bool isInRelease = outputTime < outputReleaseEndTime;
            if (isInRelease)
            {
                double positionInRelease = outputTime - outputLoopEnd;
                inputTime = _loopEndMarker + positionInRelease;
                return inputTime;
            }

            // AfterRelease
            return null;
        }

        // Helpers

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetNoteDuration(double outputTime, int channelIndex)
        {
            double value = CalculationHelper.VERY_HIGH_VALUE;
            if (_noteDurationCalculator != null)
            {
                value = _noteDurationCalculator.Calculate(outputTime, channelIndex);
            }

            return value;
        }
    }
}