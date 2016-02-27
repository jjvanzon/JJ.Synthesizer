using System;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Loop_OperatorCalculator_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _loopStartMarker;
        private readonly double _loopEndMarker;

        private readonly double _cycleDuration;

        public Loop_OperatorCalculator_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration(
            OperatorCalculatorBase signalCalculator,
            double loopStartMarker,
            double loopEndMarker)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);

            _signalCalculator = signalCalculator;
            _loopStartMarker = loopStartMarker;
            _loopEndMarker = loopEndMarker;
            
            _cycleDuration = _loopEndMarker - _loopStartMarker;
        }

        public override double Calculate(double outputTime, int channelIndex)
        {
            // BeforeLoop
            double inputTime = outputTime + _loopStartMarker;
            bool isBeforeLoop = inputTime < _loopStartMarker;
            if (isBeforeLoop)
            {
                return 0;
            }

            // InLoop
            double phase = (inputTime - _loopStartMarker) % _cycleDuration;
            inputTime = _loopStartMarker + phase;
            double value = _signalCalculator.Calculate(inputTime, channelIndex);
            return value;
        }
    }
}