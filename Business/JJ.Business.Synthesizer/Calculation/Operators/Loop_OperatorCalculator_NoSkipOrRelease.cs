using System;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Loop_OperatorCalculator_NoSkipOrRelease : Loop_OperatorCalculator_Base
    {
        private readonly OperatorCalculatorBase _loopStartMarkerCalculator;
        private readonly OperatorCalculatorBase _loopEndMarkerCalculator;
        private readonly OperatorCalculatorBase _noteDurationCalculator;

        private double _outputCycleEnd;
        private double _loopEndMarker;
        private double _cycleDuration;

        public Loop_OperatorCalculator_NoSkipOrRelease(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase loopStartMarkerCalculator,
            OperatorCalculatorBase loopEndMarkerCalculator,
            OperatorCalculatorBase noteDurationCalculator)
            : base(
                  signalCalculator, new OperatorCalculatorBase[]
                  {
                      signalCalculator,
                      loopStartMarkerCalculator,
                      loopEndMarkerCalculator,
                      noteDurationCalculator
                  }.Where(x => x != null).ToArray())
        {
            _loopStartMarkerCalculator = loopStartMarkerCalculator;
            _loopEndMarkerCalculator = loopEndMarkerCalculator;
            _noteDurationCalculator = noteDurationCalculator;
        }
        
        protected override double? TransformTime(double time, int channelIndex)
        {
            // Apply origin
            time -= _origin;
            
            // BeforeAttack
            bool isBeforeAttack = time < 0;
            if (isBeforeAttack)
            {
                return null;
            }

            // BeforeLoop
            double loopStartMarker = GetLoopStartMarker(time, channelIndex);
            bool isInAttack = time < loopStartMarker;
            if (isInAttack)
            {
                return time;
            }

            // InLoop
            double noteDuration = GetNoteDuration(time, channelIndex);
            bool isInLoop = time < noteDuration;
            if (isInLoop)
            {
                if (time > _outputCycleEnd)
                {
                    _loopEndMarker = GetLoopEndMarker(time, channelIndex);
                    _cycleDuration = _loopEndMarker - loopStartMarker;
                    _outputCycleEnd += _cycleDuration;
                }

                double phase = (time - loopStartMarker) % _cycleDuration;
                double inputTime = loopStartMarker + phase;
                return inputTime;
            }

            // AfterLoop
            return null;

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