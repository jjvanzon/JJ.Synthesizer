using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Loop_OperatorCalculator_NoSkipOrRelease : Loop_OperatorCalculator_Base
    {
        private readonly OperatorCalculatorBase _loopStartMarkerCalculator;
        private readonly OperatorCalculatorBase _loopEndMarkerCalculator;
        private readonly OperatorCalculatorBase _noteDurationCalculator;

        private double _outputCycleEnd;
        private double _loopEndMarker;
        private double _cycleLength;

        public Loop_OperatorCalculator_NoSkipOrRelease(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase loopStartMarkerCalculator,
            OperatorCalculatorBase loopEndMarkerCalculator,
            OperatorCalculatorBase noteDurationCalculator,
            DimensionStack dimensionStack)
            : base(
                  signalCalculator, 
                  dimensionStack,
                  new OperatorCalculatorBase[]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override double? GetTransformedPosition()
        {
            double position = _dimensionStack.Get(_previousDimensionStackIndex);

            // Apply origin
            position -= _origin;
            
            // BeforeAttack
            bool isBeforeAttack = position < 0;
            if (isBeforeAttack)
            {
                return null;
            }

            // BeforeLoop
            double loopStartMarker = GetLoopStartMarker();
            bool isInAttack = position < loopStartMarker;
            if (isInAttack)
            {
                return position;
            }

            // InLoop
            double noteDuration = GetNoteDuration();
            bool isInLoop = position < noteDuration;
            if (isInLoop)
            {
                if (position > _outputCycleEnd)
                {
                    _loopEndMarker = GetLoopEndMarker();
                    _cycleLength = _loopEndMarker - loopStartMarker;
                    _outputCycleEnd += _cycleLength;
                }

                double phase = (position - loopStartMarker) % _cycleLength;
                double inputPosition = loopStartMarker + phase;
                return inputPosition;
            }

            // AfterLoop
            return null;

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetLoopStartMarker()
        {
            double value = 0;
            if (_loopStartMarkerCalculator != null)
            {
                value = _loopStartMarkerCalculator.Calculate();
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetLoopEndMarker()
        {
            double inputEndPosition = 0;
            if (_loopEndMarkerCalculator != null)
            {
                inputEndPosition = _loopEndMarkerCalculator.Calculate();
            }

            return inputEndPosition;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetNoteDuration()
        {
            double value = CalculationHelper.VERY_HIGH_VALUE;
            if (_noteDurationCalculator != null)
            {
                value = _noteDurationCalculator.Calculate();
            }

            return value;
        }
    }
}