using System;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Loop_WithoutAttackOrRelease_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _loopStartMarkerCalculator;
        private readonly OperatorCalculatorBase _sustainDurationCalculator;
        private readonly OperatorCalculatorBase _loopEndMarkerCalculator;

        public Loop_WithoutAttackOrRelease_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase loopStartMarkerCalculator,
            OperatorCalculatorBase sustainDurationCalculator,
            OperatorCalculatorBase loopEndMarkerCalculator)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator,
                loopStartMarkerCalculator,
                sustainDurationCalculator,
                loopEndMarkerCalculator,
            }.Where(x => x != null).ToArray())
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);

            _signalCalculator = signalCalculator;
            _loopStartMarkerCalculator = loopStartMarkerCalculator;
            _sustainDurationCalculator = sustainDurationCalculator;
            _loopEndMarkerCalculator = loopEndMarkerCalculator;
        }

        public override double Calculate(double outputTime, int channelIndex)
        {
            double inputTime = outputTime;

            // BeforeLoop
            bool isBeforeLoop = inputTime < 0;
            if (isBeforeLoop)
            {
                return 0;
            }

            // InLoop
            double outputSustainDuration = GetSustainDuration(outputTime, channelIndex);
            bool isInLoop = outputTime < outputSustainDuration;
            if (isInLoop)
            {
                double inputLoopStartMarker = GetLoopStartMarker(outputTime, channelIndex);
                double inputLoopEndMarker = GetLoopEndMarker(outputTime, channelIndex);
                double inputSustainDuration = inputLoopEndMarker - inputLoopStartMarker;
                double positionInCycle = inputTime % inputSustainDuration;
                inputTime = inputLoopStartMarker + positionInCycle;
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
            double inputEndTime = 0;
            if (_loopEndMarkerCalculator != null)
            {
                inputEndTime = _loopEndMarkerCalculator.Calculate(outputTime, channelIndex);
            }

            return inputEndTime;
        }
    }
}