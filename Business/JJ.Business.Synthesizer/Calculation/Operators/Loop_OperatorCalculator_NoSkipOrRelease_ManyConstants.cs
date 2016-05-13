using System;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Loop_OperatorCalculator_NoSkipOrRelease_ManyConstants 
        : Loop_OperatorCalculator_Base
    {
        private readonly double _loopStartMarker;
        private readonly double _loopEndMarker;
        private readonly OperatorCalculatorBase _noteDurationCalculator;

        private readonly double _cycleLength;

        public Loop_OperatorCalculator_NoSkipOrRelease_ManyConstants(
            OperatorCalculatorBase signalCalculator,
            double loopStartMarker,
            double loopEndMarker,
            OperatorCalculatorBase noteDurationCalculator,
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack)
            : base(
                  signalCalculator,
                  dimensionEnum,
                  dimensionStack,
                  new OperatorCalculatorBase[]
                  {
                      signalCalculator,
                      noteDurationCalculator,
                  }.Where(x => x != null).ToArray())
        {
            _loopStartMarker = loopStartMarker;
            _loopEndMarker = loopEndMarker;
            _noteDurationCalculator = noteDurationCalculator;

            _cycleLength = _loopEndMarker - _loopStartMarker;
        }

        protected override double? GetTransformedPosition()
        {
            double position = _dimensionStack.Get(_dimensionIndex);

            position -= _origin;

            // BeforeAttack
            bool isBeforeAttack = position < 0;
            if (isBeforeAttack)
            {
                return 0;
            }

            // BeforeLoop
            bool isInAttack = position < _loopStartMarker;
            if (isInAttack)
            {
                return position;
            }

            // InLoop
            double noteDuration = GetNoteDuration();
            bool isInLoop = position < noteDuration;
            if (isInLoop)
            {
                double phase = (position - _loopStartMarker) % _cycleLength;
                double inputPosition = _loopStartMarker + phase;
                return inputPosition;
            }

            // AfterLoop
            return 0;
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