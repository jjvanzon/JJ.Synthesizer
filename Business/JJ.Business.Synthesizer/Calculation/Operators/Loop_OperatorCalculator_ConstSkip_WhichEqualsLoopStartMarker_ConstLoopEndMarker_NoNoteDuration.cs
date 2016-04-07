using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Loop_OperatorCalculator_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration 
        : Loop_OperatorCalculator_Base
    {
        private readonly double _loopStartMarker;
        private readonly double _loopEndMarker;
        private readonly double _cycleDuration;

        public Loop_OperatorCalculator_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration(
            OperatorCalculatorBase signalCalculator,
            double loopStartMarker,
            double loopEndMarker)
            : base(signalCalculator, new OperatorCalculatorBase[] { signalCalculator })
        {
            _loopStartMarker = loopStartMarker;
            _loopEndMarker = loopEndMarker;
            
            _cycleDuration = _loopEndMarker - _loopStartMarker;
        }

        protected override double? TransformTime(double time, int channelIndex)
        {
            time -= _origin;

            // BeforeLoop
            double inputTime = time + _loopStartMarker;
            bool isBeforeLoop = inputTime < _loopStartMarker;
            if (isBeforeLoop)
            {
                return 0;
            }

            // InLoop
            double phase = (inputTime - _loopStartMarker) % _cycleDuration;
            return phase;
        }
    }
}