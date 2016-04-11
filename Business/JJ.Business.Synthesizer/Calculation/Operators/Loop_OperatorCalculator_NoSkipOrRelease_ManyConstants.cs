using System;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Loop_OperatorCalculator_NoSkipOrRelease_ManyConstants 
        : Loop_OperatorCalculator_Base
    {
        private readonly double _loopStartMarker;
        private readonly double _loopEndMarker;
        private readonly OperatorCalculatorBase _noteDurationCalculator;

        private readonly double _cycleDuration;

        public Loop_OperatorCalculator_NoSkipOrRelease_ManyConstants(
            OperatorCalculatorBase signalCalculator,
            double loopStartMarker,
            double loopEndMarker,
            OperatorCalculatorBase noteDurationCalculator)
            : base(
                  signalCalculator,
                  new OperatorCalculatorBase[]
                  {
                      signalCalculator,
                      noteDurationCalculator,
                  }.Where(x => x != null).ToArray())
        {
            _loopStartMarker = loopStartMarker;
            _loopEndMarker = loopEndMarker;
            _noteDurationCalculator = noteDurationCalculator;

            _cycleDuration = _loopEndMarker - _loopStartMarker;
        }

        protected override double? TransformTime(DimensionStack dimensionStack)
        {
            double time = dimensionStack.Get(DimensionEnum.Time);

            time -= _origin;

            // BeforeAttack
            bool isBeforeAttack = time < 0;
            if (isBeforeAttack)
            {
                return 0;
            }

            // BeforeLoop
            bool isInAttack = time < _loopStartMarker;
            if (isInAttack)
            {
                return time;
            }

            // InLoop
            double noteDuration = GetNoteDuration(dimensionStack);
            bool isInLoop = time < noteDuration;
            if (isInLoop)
            {
                double phase = (time - _loopStartMarker) % _cycleDuration;
                double inputTime = _loopStartMarker + phase;
                return inputTime;
            }

            // AfterLoop
            return 0;
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