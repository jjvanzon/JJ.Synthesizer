using System;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Loop_OperatorCalculator_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _loopStartMarker;
        private readonly OperatorCalculatorBase _loopEndMarkerCalculator;

        public Loop_OperatorCalculator_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration(
            OperatorCalculatorBase signalCalculator,
            double loopStartMarker,
            OperatorCalculatorBase loopEndMarkerCalculator)
            : base(new OperatorCalculatorBase[] { signalCalculator, loopEndMarkerCalculator })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);

            _signalCalculator = signalCalculator;
            _loopStartMarker = loopStartMarker;
            _loopEndMarkerCalculator = loopEndMarkerCalculator;
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
            double loopEndMarker = GetLoopEndMarker(outputTime, channelIndex);

            double cycleDuration = loopEndMarker - _loopStartMarker;

            double phase = (inputTime - _loopStartMarker) % cycleDuration;
            inputTime = _loopStartMarker + phase;
            double value = _signalCalculator.Calculate(inputTime, channelIndex);
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetLoopEndMarker(double outputTime, int channelIndex)
        {
            double value = 0;
            if (_loopEndMarkerCalculator != null)
            {
                value = _loopEndMarkerCalculator.Calculate(outputTime, channelIndex);
            }

            return value;
        }
    }
}