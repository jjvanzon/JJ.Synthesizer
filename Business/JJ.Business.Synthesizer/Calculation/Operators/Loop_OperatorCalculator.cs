using System;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Loop_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _attackCalculator;
        private readonly OperatorCalculatorBase _startCalculator;
        private readonly OperatorCalculatorBase _sustainCalculator;
        private readonly OperatorCalculatorBase _endCalculator;
        private readonly OperatorCalculatorBase _releaseCalculator;

        public Loop_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase attackCalculator,
            OperatorCalculatorBase startCalculator,
            OperatorCalculatorBase sustainCalculator,
            OperatorCalculatorBase endCalculator,
            OperatorCalculatorBase releaseCalculator)
            : base(new OperatorCalculatorBase[] 
            {
                signalCalculator,
                attackCalculator,
                startCalculator,
                sustainCalculator,
                endCalculator,
                releaseCalculator
            })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (attackCalculator == null) throw new NullException(() => attackCalculator);
            if (startCalculator == null) throw new NullException(() => startCalculator);
            if (sustainCalculator == null) throw new NullException(() => sustainCalculator);
            if (endCalculator == null) throw new NullException(() => endCalculator);
            if (releaseCalculator == null) throw new NullException(() => releaseCalculator);

            _signalCalculator = signalCalculator;
            _attackCalculator = attackCalculator;
            _startCalculator = startCalculator;
            _sustainCalculator = sustainCalculator;
            _endCalculator = endCalculator;
            _releaseCalculator = releaseCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double outputTime = time;
            double inputAttack = _attackCalculator.Calculate(outputTime, channelIndex);
            double inputTime = time + inputAttack;

            bool isBeforeAttack = inputTime < inputAttack;
            if (isBeforeAttack)
            {
                return 0;
            }

            double inputStart = _startCalculator.Calculate(outputTime, channelIndex);
            bool isInAttack = inputTime < inputStart;
            if (isInAttack)
            {
                double value = _signalCalculator.Calculate(inputTime, channelIndex);
                return value;
            }

            double inputEnd = _endCalculator.Calculate(outputTime, channelIndex);
            double outputSustain = _sustainCalculator.Calculate(outputTime, channelIndex);
            double outputEnd = inputStart - inputAttack + outputSustain;
            bool isInLoop = outputTime < outputEnd;
            if (isInLoop)
            {
                double inputSustain = inputEnd - inputStart;
                double positionInCycle = (inputTime - inputStart) % inputSustain;
                inputTime = inputStart + positionInCycle;
                double value = _signalCalculator.Calculate(inputTime, channelIndex);
                return value;
            }

            double inputRelease = _releaseCalculator.Calculate(outputTime, channelIndex);
            double releaseDuration = inputRelease - inputEnd;
            double outputRelease = outputEnd + releaseDuration;
            bool isInRelease = outputTime < outputRelease;
            if (isInRelease)
            {
                double positionInRelease = outputTime - outputEnd;
                inputTime = inputEnd + positionInRelease;
                double value = _signalCalculator.Calculate(inputTime, channelIndex);
                return value;
            }

            // IsAfterRelease
            return 0;
        }
    }
}