using System;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Loop_OperatorCalculator_ManyConstants : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
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
            : base(new OperatorCalculatorBase[] 
            {
                signalCalculator,
                noteDurationCalculator
            }.Where(x => x != null).ToArray())
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);

            _signalCalculator = signalCalculator;
            _skip = skip;
            _loopStartMarker = loopStartMarker;
            _loopEndMarker = loopEndMarker;
            _releaseEndMarker = releaseEndMarker;
            _noteDurationCalculator = noteDurationCalculator;

            _cycleDuration = _loopEndMarker - _loopStartMarker;
            _outputLoopStart = _loopStartMarker - _skip;
            _releaseDuration = _releaseEndMarker - _loopEndMarker;
        }

        public override double Calculate(double outputTime, int channelIndex)
        {
            // BeforeAttack
            double inputTime = outputTime + _skip;
            bool isBeforeAttack = inputTime < _skip;
            if (isBeforeAttack)
            {
                return 0;
            }

            // InAttack
            bool isInAttack = inputTime < _loopStartMarker;
            if (isInAttack)
            {
                double value = _signalCalculator.Calculate(inputTime, channelIndex);
                return value;
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
                double value = _signalCalculator.Calculate(inputTime, channelIndex);
                return value;
            }

            // InRelease
            double outputReleaseEndTime = outputLoopEnd + _releaseDuration;
            bool isInRelease = outputTime < outputReleaseEndTime;
            if (isInRelease)
            {
                double positionInRelease = outputTime - outputLoopEnd;
                inputTime = _loopEndMarker + positionInRelease;
                double value = _signalCalculator.Calculate(inputTime, channelIndex);
                return value;
            }

            // AfterRelease
            return 0;
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