using System;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Loop_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _attackStartCalculator;
        private OperatorCalculatorBase _loopStartCalculator;
        private OperatorCalculatorBase _loopDurationCalculator;
        private OperatorCalculatorBase _loopEndCalculator;
        private OperatorCalculatorBase _releaseEndCalculator;

        public Loop_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase attackStartCalculator,
            OperatorCalculatorBase loopStartCalculator,
            OperatorCalculatorBase loopDurationCalculator,
            OperatorCalculatorBase loopEndCalculator,
            OperatorCalculatorBase releaseEndCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (attackStartCalculator == null) throw new NullException(() => attackStartCalculator);
            if (loopStartCalculator == null) throw new NullException(() => loopStartCalculator);
            if (loopDurationCalculator == null) throw new NullException(() => loopDurationCalculator);
            if (loopEndCalculator == null) throw new NullException(() => loopEndCalculator);
            if (releaseEndCalculator == null) throw new NullException(() => releaseEndCalculator);

            _signalCalculator = signalCalculator;
            _attackStartCalculator = attackStartCalculator;
            _loopStartCalculator = loopStartCalculator;
            _loopDurationCalculator = loopDurationCalculator;
            _loopEndCalculator = loopEndCalculator;
            _releaseEndCalculator = releaseEndCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double outputTime = time;

            double inputAttackStart = _attackStartCalculator.Calculate(time, channelIndex);

            double inputTime = time + inputAttackStart;

            bool isBeforeAttack = inputTime < inputAttackStart;
            if (isBeforeAttack)
            {
                return 0;
            }

            double inputLoopStart = _loopStartCalculator.Calculate(time, channelIndex);
            bool isInAttack = inputTime < inputLoopStart;
            if (isInAttack)
            {
                double value = _signalCalculator.Calculate(inputTime, channelIndex);
                return value;
            }

            double inputLoopEnd = _loopEndCalculator.Calculate(time, channelIndex);
            double inputLoopDuration = inputLoopEnd - inputLoopStart;
            double outputLoopDuration = _loopDurationCalculator.Calculate(time, channelIndex);
            double outputLoopEnd = inputLoopStart - inputAttackStart + outputLoopDuration;
            bool isInLoop = outputTime < outputLoopEnd;
            if (isInLoop)
            {
                double positionInCycle = (inputTime - inputLoopStart) % inputLoopDuration;
                inputTime = inputLoopStart + positionInCycle;
                double value = _signalCalculator.Calculate(inputTime, channelIndex);
                return value;
            }

            double inputReleaseEnd = _releaseEndCalculator.Calculate(time, channelIndex);
            double releaseDuration = inputReleaseEnd - inputLoopEnd;
            double outputReleaseEnd = outputLoopEnd + releaseDuration;
            bool isInRelease = outputTime < outputReleaseEnd;
            if (isInRelease)
            {
                double positionInRelease = outputTime - outputLoopEnd;
                inputTime = inputLoopEnd + positionInRelease;
                double value = _signalCalculator.Calculate(inputTime, channelIndex);
                return value;
            }

            // IsAfterRelease
            return 0;
        }
    }
}