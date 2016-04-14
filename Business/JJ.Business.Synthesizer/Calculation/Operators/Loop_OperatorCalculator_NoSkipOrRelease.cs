using System;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;

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
            DimensionEnum dimensionEnum)
            : base(
                  signalCalculator, 
                  dimensionEnum, 
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
        
        protected override double? TransformPosition(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            // Apply origin
            position -= _origin;
            
            // BeforeAttack
            bool isBeforeAttack = position < 0;
            if (isBeforeAttack)
            {
                return null;
            }

            // BeforeLoop
            double loopStartMarker = GetLoopStartMarker(dimensionStack);
            bool isInAttack = position < loopStartMarker;
            if (isInAttack)
            {
                return position;
            }

            // InLoop
            double noteDuration = GetNoteDuration(dimensionStack);
            bool isInLoop = position < noteDuration;
            if (isInLoop)
            {
                if (position > _outputCycleEnd)
                {
                    _loopEndMarker = GetLoopEndMarker(dimensionStack);
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
        private double GetLoopStartMarker(DimensionStack dimensionStack)
        {
            double value = 0;
            if (_loopStartMarkerCalculator != null)
            {
                value = _loopStartMarkerCalculator.Calculate(dimensionStack);
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetLoopEndMarker(DimensionStack dimensionStack)
        {
            double inputEndPosition = 0;
            if (_loopEndMarkerCalculator != null)
            {
                inputEndPosition = _loopEndMarkerCalculator.Calculate(dimensionStack);
            }

            return inputEndPosition;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetNoteDuration(DimensionStack dimensionStack)
        {
            double value = CalculationHelper.VERY_HIGH_VALUE;
            if (_noteDurationCalculator != null)
            {
                value = _noteDurationCalculator.Calculate(dimensionStack);
            }

            return value;
        }
    }
}