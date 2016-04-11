using System;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Loop_OperatorCalculator_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration 
        : Loop_OperatorCalculator_Base
    {
        private readonly double _loopStartMarker;
        private readonly OperatorCalculatorBase _loopEndMarkerCalculator;

        private double _outputCycleEnd;
        private double _loopEndMarker;
        private double _cycleDuration;

        public Loop_OperatorCalculator_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration(
            OperatorCalculatorBase signalCalculator,
            double loopStartMarker,
            OperatorCalculatorBase loopEndMarkerCalculator)
            : base(
                  signalCalculator, 
                  new OperatorCalculatorBase[] { signalCalculator, loopEndMarkerCalculator })
        {
            _loopStartMarker = loopStartMarker;
            _loopEndMarkerCalculator = loopEndMarkerCalculator;
        }

        protected override double? TransformTime(DimensionStack dimensionStack)
        {
            double outputTime = dimensionStack.Get(DimensionEnum.Time);

            double tempTime = outputTime - _origin;

            // BeforeLoop
            double inputTime = tempTime + _loopStartMarker;
            bool isBeforeLoop = inputTime < _loopStartMarker;
            if (isBeforeLoop)
            {
                return null;
            }

            // InLoop
            if (tempTime > _outputCycleEnd)
            {
                dimensionStack.Push(DimensionEnum.Time, tempTime);
                _loopEndMarker = GetLoopEndMarker(dimensionStack);
                dimensionStack.Pop(DimensionEnum.Time);

                _cycleDuration = _loopEndMarker - _loopStartMarker;
                _outputCycleEnd += _cycleDuration;
            }

            double phase = (inputTime - _loopStartMarker) % _cycleDuration;
            inputTime = _loopStartMarker + phase;
            return inputTime;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetLoopEndMarker(DimensionStack dimensionStack)
        {
            double value = 0;
            if (_loopEndMarkerCalculator != null)
            {
                value = _loopEndMarkerCalculator.Calculate(dimensionStack);
            }

            return value;
        }
    }
}