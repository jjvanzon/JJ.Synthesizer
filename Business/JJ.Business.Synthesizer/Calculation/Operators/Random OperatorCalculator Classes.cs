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
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;

        private double _phase;
        private double _previousTime;

        public Random_VarFrequency_VarPhaseShift_BlockInterpolation_OperatorCalculator(
            RandomCalculator_WithBlockInterpolation randomCalculator,
            double randomCalculatorOffset,
            OperatorCalculatorBase frequencyCalculator,
            OperatorCalculatorBase phaseShiftCalculator)
            : base(new OperatorCalculatorBase[] { frequencyCalculator, phaseShiftCalculator })
        {
            if (randomCalculator == null) throw new NullException(() => randomCalculator);
            // TODO: Make assertion strict again, once you have more calculator variations.
            //OperatorCalculatorHelper.AssertOperatorCalculatorBase(frequencyCalculator, () => frequencyCalculator);
            //OperatorCalculatorHelper.AssertOperatorCalculatorBase(phaseShiftCalculator, () => phaseShiftCalculator);

            _randomCalculator = randomCalculator;
            _randomCalculatorOffset = randomCalculatorOffset;
            _frequencyCalculator = frequencyCalculator;
            _phaseShiftCalculator = phaseShiftCalculator;

            _phase = _randomCalculatorOffset;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double frequency = _frequencyCalculator.Calculate(time, channelIndex);
            double phaseShift = _phaseShiftCalculator.Calculate(time, channelIndex);

            double dt = time - _previousTime;
            _phase = _phase + dt * frequency;

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
