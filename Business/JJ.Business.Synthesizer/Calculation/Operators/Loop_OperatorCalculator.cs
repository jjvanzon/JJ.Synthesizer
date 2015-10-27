using System;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Loop_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _startCalculator;
        private OperatorCalculatorBase _loopStartCalculator;
        private OperatorCalculatorBase _loopEndCalculator;
        private OperatorCalculatorBase _endCalculator;

        public Loop_OperatorCalculator(
            OperatorCalculatorBase startCalculator,
            OperatorCalculatorBase loopStartCalculator,
            OperatorCalculatorBase loopEndCalculator,
            OperatorCalculatorBase endCalculator)
        {
            if (startCalculator == null) throw new NullException(() => startCalculator);
            if (loopStartCalculator == null) throw new NullException(() => loopStartCalculator);
            if (loopEndCalculator == null) throw new NullException(() => loopEndCalculator);
            if (endCalculator == null) throw new NullException(() => endCalculator);

            _startCalculator = startCalculator;
            _loopStartCalculator = loopStartCalculator;
            _loopEndCalculator = loopEndCalculator;
            _endCalculator = endCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double start = _startCalculator.Calculate(time, channelIndex);
            double loopStart = _loopStartCalculator.Calculate(time, channelIndex);
            double loopEnd = _loopEndCalculator.Calculate(time, channelIndex);
            double end = _endCalculator.Calculate(time, channelIndex);

            throw new NotImplementedException();
            //return a + b;
        }

        private double TrySomething(double time, int channelIndex)
        {
            OperatorCalculatorBase signalCalculator = null;

            double attackStart = 0.5; // At t = 0 we start with input t = 0.5. To cut of a piece of the start
            double loopStart = 1;
            double loopDuration = 6;
            double loopEnd = 4; // After loop time we skip go 'back in time' to the releaseStart
            double releaseEnd = 6; // To cut off a piece of the end.

            bool isBeforeAttack = time < attackStart;
            if (isBeforeAttack)
            {
                return 0;
            }

            bool isInAttack = time < loopStart;
            if (isInAttack)
            {
                double transformedTime = time + attackStart;
                double value = signalCalculator.Calculate(transformedTime, channelIndex);
                return value;
            }

            double shiftedLoopEnd = loopStart + loopDuration;
            bool isInLoop = time < shiftedLoopEnd;
            if (isInLoop)
            {
                double positionInCycle = (time - loopStart) % loopDuration;
                double transformedTime = loopStart + positionInCycle;
                double value = signalCalculator.Calculate(transformedTime, channelIndex);
                return value;
            }

            double releaseDuration = releaseEnd - loopEnd;
            double shiftedReleaseEnd = shiftedLoopEnd + releaseDuration;
            bool isInRelease = time < shiftedReleaseEnd;
            if (isInRelease)
            {
                double cycleDuration = loopEnd - loopStart;
                double transformedTime = time - loopDuration + cycleDuration;
                double value = signalCalculator.Calculate(transformedTime, channelIndex);
                return value;
            }

            // IsAfterRelease
            return 0;
        }
    }
}