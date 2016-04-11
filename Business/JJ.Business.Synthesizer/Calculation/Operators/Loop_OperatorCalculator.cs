using System;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Loop_OperatorCalculator : Loop_OperatorCalculator_Base
    {
        private readonly OperatorCalculatorBase _skipCalculator;
        private readonly OperatorCalculatorBase _loopStartMarkerCalculator;
        private readonly OperatorCalculatorBase _loopEndMarkerCalculator;
        private readonly OperatorCalculatorBase _releaseEndMarkerCalculator;
        private readonly OperatorCalculatorBase _noteDurationCalculator;

        public Loop_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase skipCalculator,
            OperatorCalculatorBase loopStartMarkerCalculator,
            OperatorCalculatorBase loopEndMarkerCalculator,
            OperatorCalculatorBase releaseEndMarkerCalculator,
            OperatorCalculatorBase noteDurationCalculator)
            : base(
                  signalCalculator,
                  new OperatorCalculatorBase[]
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
        protected override double? TransformTime(DimensionStack dimensionStack)
        {
            double outputTime = dimensionStack.Get(DimensionEnum.Time);

            // BeforeAttack
            double skip = GetSkip(dimensionStack);
            double inputTime = outputTime + skip;
            bool isBeforeAttack = inputTime < skip;
            if (isBeforeAttack)
            {
                return null;
            }

            // InAttack
            double loopStartMarker = GetLoopStartMarker(dimensionStack);
            bool isInAttack = inputTime < loopStartMarker;
            if (isInAttack)
            {
                return inputTime;
            }

            // InLoop
            double noteDuration = GetNoteDuration(dimensionStack);
            double loopEndMarker = GetLoopEndMarker(dimensionStack);
            double cycleDuration = loopEndMarker - loopStartMarker;

            // Round up end of loop to whole cycles.
            double outputLoopStart = loopStartMarker - skip;
            double noteEndPhase = (noteDuration - outputLoopStart) / cycleDuration;
            double outputLoopEnd = outputLoopStart + Math.Ceiling(noteEndPhase) * cycleDuration;

            bool isInLoop = outputTime < outputLoopEnd;
            if (isInLoop)
            {
                double phase = (inputTime - loopStartMarker) % cycleDuration;
                inputTime = loopStartMarker + phase;
                return inputTime;
            }

            // InRelease
            double releaseEndMarker = GetReleaseEndMarker(dimensionStack);
            double releaseDuration = releaseEndMarker - loopEndMarker;
            double outputReleaseEndTime = outputLoopEnd + releaseDuration;
            bool isInRelease = outputTime < outputReleaseEndTime;
            if (isInRelease)
            {
                double positionInRelease = outputTime - outputLoopEnd;
                inputTime = loopEndMarker + positionInRelease;
                return inputTime;
            }

            // AfterRelease
            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetSkip(DimensionStack dimensionStack)
        {
            double value = 0;
            if (_skipCalculator != null)
            {
                value = _skipCalculator.Calculate(dimensionStack);
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetLoopStartMarker(DimensionStack dimensionStack)
        {
            double value = 0;
            if (_loopStartMarkerCalculator != null)
            {
                value = _loopStartMarkerCalculator.Calculate(dimensionStack);
            }

            return value;
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetReleaseEndMarker(DimensionStack dimensionStack)
        {
            double value = 0;
            if (_releaseEndMarkerCalculator != null)
            {
                value = _releaseEndMarkerCalculator.Calculate(dimensionStack);
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetNoteDuration(DimensionStack dimensionStack)
        {
            double value = CalculationHelper.VERY_HIGH_VALUE;
            if (_noteDurationCalculator != null)
            {
                value = _noteDurationCalculator.Calculate(dimensionStack);
            }

            return value;
        }
    }
}