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

        private double _outputCycleEnd;
        private double _loopEndMarker;
        private double _cycleDuration;

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

            ResetStateNonRecursive();
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
            if (outputTime > _outputCycleEnd)
            {
                _loopEndMarker = GetLoopEndMarker(outputTime, channelIndex);
                _cycleDuration = _loopEndMarker - _loopStartMarker;
                _outputCycleEnd += _cycleDuration;
            }

            double phase = (inputTime - _loopStartMarker) % _cycleDuration;
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
        public override void ResetState()
        {
            ResetStateNonRecursive();

            base.ResetState();
        }

        private void ResetStateNonRecursive()
        {
            _outputCycleEnd = 0;
            _loopEndMarker = 0;
            _cycleDuration = 0;
        }
    }
}