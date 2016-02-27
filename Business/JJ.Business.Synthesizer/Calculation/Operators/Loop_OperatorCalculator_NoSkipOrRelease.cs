using System;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Loop_OperatorCalculator_NoSkipOrRelease : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _loopStartMarkerCalculator;
        private readonly OperatorCalculatorBase _loopEndMarkerCalculator;
        private readonly OperatorCalculatorBase _noteDurationCalculator;

        public Loop_OperatorCalculator_NoSkipOrRelease(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase loopStartMarkerCalculator,
            OperatorCalculatorBase loopEndMarkerCalculator,
            OperatorCalculatorBase noteDurationCalculator)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator,
                loopStartMarkerCalculator,
                loopEndMarkerCalculator,
                noteDurationCalculator
            }.Where(x => x != null).ToArray())
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);

            _signalCalculator = signalCalculator;
            _loopStartMarkerCalculator = loopStartMarkerCalculator;
            _loopEndMarkerCalculator = loopEndMarkerCalculator;
            _noteDurationCalculator = noteDurationCalculator;
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

            // InLoop
            double noteDuration = GetNoteDuration(time, channelIndex);
            bool isInLoop = time < noteDuration;
            if (isInLoop)
            {
                double loopEndMarker = GetLoopEndMarker(time, channelIndex);
                double cycleDuration = loopEndMarker - loopStartMarker;
                double phase = (time - loopStartMarker) % cycleDuration;
                double inputTime = loopStartMarker + phase;
                double value = _signalCalculator.Calculate(inputTime, channelIndex);
                return value;
            }

            // AfterLoop
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
        private double GetLoopEndMarker(double outputTime, int channelIndex)
        {
            double inputEndTime = 0;
            if (_loopEndMarkerCalculator != null)
            {
                inputEndTime = _loopEndMarkerCalculator.Calculate(outputTime, channelIndex);
            }

            return inputEndTime;
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
    }
}