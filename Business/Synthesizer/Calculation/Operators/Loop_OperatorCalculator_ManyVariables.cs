using System;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Loop_OperatorCalculator_SoundVarOrConst_OtherInputsVar : Loop_OperatorCalculator_Base
    {
        private readonly OperatorCalculatorBase _skipCalculator;
        private readonly OperatorCalculatorBase _loopStartMarkerCalculator;
        private readonly OperatorCalculatorBase _loopEndMarkerCalculator;
        private readonly OperatorCalculatorBase _releaseEndMarkerCalculator;
        private readonly OperatorCalculatorBase _noteDurationCalculator;

        public Loop_OperatorCalculator_SoundVarOrConst_OtherInputsVar(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase skipCalculator,
            OperatorCalculatorBase loopStartMarkerCalculator,
            OperatorCalculatorBase loopEndMarkerCalculator,
            OperatorCalculatorBase releaseEndMarkerCalculator,
            OperatorCalculatorBase noteDurationCalculator,
            DimensionStack dimensionStack)
            : base(
                  signalCalculator,
                  dimensionStack,
                  new[]
                  {
                      signalCalculator,
                      skipCalculator,
                      loopStartMarkerCalculator,
                      loopEndMarkerCalculator,
                      releaseEndMarkerCalculator,
                      noteDurationCalculator
                  }.Where(x => x != null).ToArray())
        {
            _skipCalculator = skipCalculator;
            _loopStartMarkerCalculator = loopStartMarkerCalculator;
            _loopEndMarkerCalculator = loopEndMarkerCalculator;
            _releaseEndMarkerCalculator = releaseEndMarkerCalculator;
            _noteDurationCalculator = noteDurationCalculator;
        }

        /// <summary> Returns null if before attack or after release. </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override double? GetTransformedPosition()
        {
            double outputPosition = _dimensionStack.Get();

            outputPosition -= _origin;

            // BeforeAttack
            double skip = GetSkip();
            double inputPosition = outputPosition + skip;
            bool isBeforeAttack = inputPosition < skip;
            if (isBeforeAttack)
            {
                return null;
            }

            // InAttack
            double loopStartMarker = GetLoopStartMarker();
            bool isInAttack = inputPosition < loopStartMarker;
            if (isInAttack)
            {
                return inputPosition;
            }

            // InLoop
            double noteDuration = GetNoteDuration();
            double loopEndMarker = GetLoopEndMarker();
            double cycleLength = loopEndMarker - loopStartMarker;

            // Round up end of loop to whole cycles.
            double outputLoopStart = loopStartMarker - skip;
            double noteEndPhase = (noteDuration - outputLoopStart) / cycleLength;
            double outputLoopEnd = outputLoopStart + Math.Ceiling(noteEndPhase) * cycleLength;

            bool isInLoop = outputPosition < outputLoopEnd;
            if (isInLoop)
            {
                double phase = (inputPosition - loopStartMarker) % cycleLength;
                inputPosition = loopStartMarker + phase;
                return inputPosition;
            }

            // InRelease
            double releaseEndMarker = GetReleaseEndMarker();
            double releaseLength = releaseEndMarker - loopEndMarker;
            double outputReleaseEndPosition = outputLoopEnd + releaseLength;
            bool isInRelease = outputPosition < outputReleaseEndPosition;
            if (isInRelease)
            {
                double positionInRelease = outputPosition - outputLoopEnd;
                inputPosition = loopEndMarker + positionInRelease;
                return inputPosition;
            }

            // AfterRelease
            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetSkip()
        {
            double value = 0;
            if (_skipCalculator != null)
            {
                value = _skipCalculator.Calculate();
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetLoopStartMarker()
        {
            double value = 0;
            if (_loopStartMarkerCalculator != null)
            {
                value = _loopStartMarkerCalculator.Calculate();
            }

            return value;
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetReleaseEndMarker()
        {
            double value = 0;
            if (_releaseEndMarkerCalculator != null)
            {
                value = _releaseEndMarkerCalculator.Calculate();
            }

            return value;
        }

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