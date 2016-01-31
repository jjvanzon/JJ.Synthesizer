using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Mathematics;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Random_VarFrequency_VarPhaseShift_BlockInterpolation_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly RandomCalculator_WithBlockInterpolation _randomCalculator;
        private readonly double _randomCalculatorOffset;
        private readonly OperatorCalculatorBase _valueDurationCalculator;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;

        private double _phase;
        private double _previousTime;

        public Random_VarFrequency_VarPhaseShift_BlockInterpolation_OperatorCalculator(
            RandomCalculator_WithBlockInterpolation randomCalculator,
            double randomCalculatorOffset,
            OperatorCalculatorBase valueDurationCalculator,
            OperatorCalculatorBase phaseShiftCalculator)
            : base(new OperatorCalculatorBase[] { valueDurationCalculator, phaseShiftCalculator })
        {
            if (randomCalculator == null) throw new NullException(() => randomCalculator);
            // TODO: Make assertion strict again, once you have more calculator variations.
            //OperatorCalculatorHelper.AssertOperatorCalculatorBase(frequencyCalculator, () => frequencyCalculator);
            //OperatorCalculatorHelper.AssertOperatorCalculatorBase(phaseShiftCalculator, () => phaseShiftCalculator);

            _randomCalculator = randomCalculator;
            _randomCalculatorOffset = randomCalculatorOffset;
            _valueDurationCalculator = valueDurationCalculator;
            _phaseShiftCalculator = phaseShiftCalculator;

            _phase = _randomCalculatorOffset;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double valueDuration = _valueDurationCalculator.Calculate(time, channelIndex);
            double phaseShift = _phaseShiftCalculator.Calculate(time, channelIndex);

            double dt = time - _previousTime;
            _phase = _phase + dt / valueDuration;

            double shiftedPhase = _phase + phaseShift;

            double value = _randomCalculator.GetValue(_phase);

            _previousTime = time;

            return value;
        }

        public override void ResetState()
        {
            base.ResetState();

            _phase = _randomCalculatorOffset;
            _previousTime = 0;
        }
    }
}
