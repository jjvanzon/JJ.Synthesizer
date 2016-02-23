using System;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Loop_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _skipCalculator;
        private readonly OperatorCalculatorBase _loopStartMarkerCalculator;
        private readonly OperatorCalculatorBase _sustainDurationCalculator;
        private readonly OperatorCalculatorBase _loopEndMarkerCalculator;
        private readonly OperatorCalculatorBase _releaseEndMarkerCalculator;

        public Loop_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase skipCalculator,
            OperatorCalculatorBase loopStartMarkerCalculator,
            OperatorCalculatorBase sustainDurationCalculator,
            OperatorCalculatorBase loopEndMarkerCalculator,
            OperatorCalculatorBase releaseEndMarkerCalculator)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator,
                skipCalculator,
                loopStartMarkerCalculator,
                sustainDurationCalculator,
                loopEndMarkerCalculator,
                releaseEndMarkerCalculator
            }.Where(x => x != null).ToArray())
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);

            _signalCalculator = signalCalculator;
            _skipCalculator = skipCalculator;
            _loopStartMarkerCalculator = loopStartMarkerCalculator;
            _sustainDurationCalculator = sustainDurationCalculator;
            _loopEndMarkerCalculator = loopEndMarkerCalculator;
            _releaseEndMarkerCalculator = releaseEndMarkerCalculator;
        }

        public override double Calculate(double outputTime, int channelIndex)
        {
            // BeforeAttack
            double inputSkip = GetSkip(outputTime, channelIndex);
            double inputTime = outputTime + inputSkip;
            bool isBeforeAttack = inputTime < inputSkip;
            if (isBeforeAttack)
            {
                return 0;
            }

            // InAttack
            double inputLoopStartMarker = GetLoopStartMarker(outputTime, channelIndex);
            bool isInAttack = inputTime < inputLoopStartMarker;
            if (isInAttack)
            {
                double value = _signalCalculator.Calculate(inputTime, channelIndex);
                return value;
            }

            // InLoop
            double outputSustainDuration = GetSustainDuration(outputTime, channelIndex);
            double inputLoopEndMarker = GetLoopEndMarker(outputTime, channelIndex);
            double outputLoopEndTime = inputLoopStartMarker - inputSkip + outputSustainDuration;
            bool isInLoop = outputTime < outputLoopEndTime;
            if (isInLoop)
            {
                double inputSustainDuration = inputLoopEndMarker - inputLoopStartMarker;
                double positionInCycle = (inputTime - inputLoopStartMarker) % inputSustainDuration;
                inputTime = inputLoopStartMarker + positionInCycle;
                double value = _signalCalculator.Calculate(inputTime, channelIndex);
                return value;
            }

            // InRelease
            double inputReleaseEndMarker = GetReleaseEndMarker(outputTime, channelIndex);
            double releaseDuration = inputReleaseEndMarker - inputLoopEndMarker;
            double outputReleaseEndTime = outputLoopEndTime + releaseDuration;
            bool isInRelease = outputTime < outputReleaseEndTime;
            if (isInRelease)
            {
                double positionInRelease = outputTime - outputLoopEndTime;
                inputTime = inputLoopEndMarker + positionInRelease;
                double value = _signalCalculator.Calculate(inputTime, channelIndex);
                return value;
            }

            // AfterRelease
            return 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetSkip(double outputTime, int channelIndex)
        {
            double value = 0;
            if (_skipCalculator != null)
            {
                value = _skipCalculator.Calculate(outputTime, channelIndex);
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetLoopStartMarker(double outputTime, int channelIndex)
        {
            double value = 0;
            if (_loopStartMarkerCalculator != null)
            {
                value = _loopStartMarkerCalculator.Calculate(outputTime, channelIndex);
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetSustainDuration(double outputTime, int channelIndex)
        {
            double value = CalculationHelper.VERY_HIGH_VALUE;
            if (_sustainDurationCalculator != null)
            {
                value = _sustainDurationCalculator.Calculate(outputTime, channelIndex);
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetLoopEndMarker(double outputTime, int channelIndex)
        {
            double value = 0;
            if (_loopEndMarkerCalculator != null)
            {
                value = _loopEndMarkerCalculator.Calculate(outputTime, channelIndex);
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetReleaseEndMarker(double outputTime, int channelIndex)
        {
            double value = 0;
            if (_releaseEndMarkerCalculator != null)
            {
                value = _releaseEndMarkerCalculator.Calculate(outputTime, channelIndex);
            }

            return value;
        }
    }
}