using System;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Loop_OperatorCalculator_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration 
        : Loop_OperatorCalculator_Base
    {
        private readonly double _loopStartMarker;
        private readonly double _loopEndMarker;
        private readonly double _cycleLength;

        public Loop_OperatorCalculator_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration(
            OperatorCalculatorBase signalCalculator,
            double loopStartMarker,
            double loopEndMarker,
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack)
            : base(
                  signalCalculator, 
                  dimensionEnum,
                  dimensionStack,
                  new OperatorCalculatorBase[] { signalCalculator })
        {
            _loopStartMarker = loopStartMarker;
            _loopEndMarker = loopEndMarker;
            
            _cycleLength = _loopEndMarker - _loopStartMarker;
        }

        protected override double? GetTransformedPosition()
        {
            double position = _dimensionStack.Get(_dimensionIndex);

            position -= _origin;

            // BeforeLoop
            double inputPosition = position + _loopStartMarker;
            bool isBeforeLoop = inputPosition < _loopStartMarker;
            if (isBeforeLoop)
            {
                return 0;
            }

            // InLoop
            double phase = (inputPosition - _loopStartMarker) % _cycleLength;
            return phase;
        }
    }
}