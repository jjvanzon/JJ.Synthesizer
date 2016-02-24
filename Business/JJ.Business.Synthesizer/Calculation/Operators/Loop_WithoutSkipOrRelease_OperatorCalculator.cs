using System;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Loop_WithoutSkipOrRelease_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _loopStartMarkerCalculator;
        private readonly OperatorCalculatorBase _noteDurationCalculator;
        private readonly OperatorCalculatorBase _loopEndMarkerCalculator;

        public Loop_WithoutSkipOrRelease_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase loopStartMarkerCalculator,
            OperatorCalculatorBase noteDurationCalculator,
            OperatorCalculatorBase loopEndMarkerCalculator)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator,
                loopStartMarkerCalculator,
                noteDurationCalculator,
                loopEndMarkerCalculator,
            }.Where(x => x != null).ToArray())
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);

            _signalCalculator = signalCalculator;
            _loopStartMarkerCalculator = loopStartMarkerCalculator;
            _noteDurationCalculator = noteDurationCalculator;
            _loopEndMarkerCalculator = loopEndMarkerCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            // BeforeAttack
            bool isBeforeAttack = time < 0;
            if (isBeforeAttack)
            {
                return 0;
            }

            // BeforeLoop
            double loopStartMarker = GetLoopStartMarker(time, channelIndex);
            bool isInAttack = time < loopStartMarker;
            if (isInAttack)
            {
                double value = _signalCalculator.Calculate(time, channelIndex);
                return value;
            }

            // InSustain
            double outputNoteDuration = GetNoteDuration(time, channelIndex);
            double outputLoopEndTime = outputNoteDuration;
            bool isInLoop = time < outputLoopEndTime;
            if (isInLoop)
            {
                double inputLoopEndMarker = GetLoopEndMarker(time, channelIndex);
                double inputCycleDuration = inputLoopEndMarker - loopStartMarker;
                double positionInCycle = (time - loopStartMarker) % inputCycleDuration;
                double inputTime = loopStartMarker + positionInCycle;
                double value = _signalCalculator.Calculate(inputTime, channelIndex);
                return value;
            }

            // AfterSustain
            return 0;
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
        private double GetNoteDuration(double outputTime, int channelIndex)
        {
            double value = CalculationHelper.VERY_HIGH_VALUE;
            if (_noteDurationCalculator != null)
            {
                value = _noteDurationCalculator.Calculate(outputTime, channelIndex);
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetLoopEndMarker(double outputTime, int channelIndex)
        {
            double inputEndTime = 0;
            if (_loopEndMarkerCalculator != null)
            {
                inputEndTime = _loopEndMarkerCalculator.Calculate(outputTime, channelIndex);
            }

            return inputEndTime;
        }
    }
}