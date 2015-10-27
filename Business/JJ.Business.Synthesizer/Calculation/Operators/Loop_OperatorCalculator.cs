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
            double attackStart = _attackStartCalculator.Calculate(time, channelIndex);

            bool isBeforeAttack = time < attackStart;
            if (isBeforeAttack)
            {
                return 0;
            }

            double loopStart = _loopStartCalculator.Calculate(time, channelIndex);

            bool isInAttack = time < loopStart;
            if (isInAttack)
            {
                double transformedTime = time + attackStart;
                double value = _signalCalculator.Calculate(transformedTime, channelIndex);
                return value;
            }

            double loopEnd = _loopEndCalculator.Calculate(time, channelIndex);
            double loopDuration = _loopDurationCalculator.Calculate(time, channelIndex);
            double shiftedLoopEnd = loopStart + loopDuration;
            bool isInLoop = time < shiftedLoopEnd;
            if (isInLoop)
            {
                double cycleDuration = loopEnd - loopStart;
                double positionInCycle = (time - loopStart) % cycleDuration;
                double transformedTime = loopStart + positionInCycle;
                double value = _signalCalculator.Calculate(transformedTime, channelIndex);
                return value;
            }

            double releaseEnd = _releaseEndCalculator.Calculate(time, channelIndex);
            double releaseDuration = releaseEnd - loopEnd;
            double shiftedReleaseEnd = shiftedLoopEnd + releaseDuration;
            bool isInRelease = time < shiftedReleaseEnd;
            if (isInRelease)
            {
                double cycleDuration = loopEnd - loopStart;
                double transformedTime = time - loopDuration + cycleDuration;
                double value = _signalCalculator.Calculate(transformedTime, channelIndex);
                return value;
            }

            // IsAfterRelease
            return 0;
        }
    }
}