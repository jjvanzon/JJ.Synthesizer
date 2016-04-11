using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation.Random;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    // Right now there aren't any other variations than VarFrequency and VarPhaseShift.
    internal class Random_OperatorCalculator_BlockAndStripe_VarFrequency_VarPhaseShift : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly RandomCalculatorBase _randomCalculator;
        private readonly double _randomCalculatorOffset;
        private readonly OperatorCalculatorBase _rateCalculator;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;

        private double _phase;
        private double _previousTime;

        public Random_OperatorCalculator_BlockAndStripe_VarFrequency_VarPhaseShift(
            RandomCalculatorBase randomCalculator,
            double randomCalculatorOffset,
            OperatorCalculatorBase rateCalculator,
            OperatorCalculatorBase phaseShiftCalculator)
            : base(new OperatorCalculatorBase[] { rateCalculator, phaseShiftCalculator })
        {
            if (randomCalculator == null) throw new NullException(() => randomCalculator);
            // TODO: Make assertion strict again, once you have more calculator variations.
            //OperatorCalculatorHelper.AssertOperatorCalculatorBase(frequencyCalculator, () => frequencyCalculator);
            //OperatorCalculatorHelper.AssertOperatorCalculatorBase(phaseShiftCalculator, () => phaseShiftCalculator);

            _randomCalculator = randomCalculator;
            _randomCalculatorOffset = randomCalculatorOffset;
            _rateCalculator = rateCalculator;
            _phaseShiftCalculator = phaseShiftCalculator;

            // TODO: Make sure you asser this strictly, so it does not become NaN.
            _phase = _randomCalculatorOffset;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double time = dimensionStack.Get(DimensionEnum.Time);

            double rate = _rateCalculator.Calculate(dimensionStack);
            double phaseShift = _phaseShiftCalculator.Calculate(dimensionStack);

            double dt = time - _previousTime;
            double phase = _phase + dt * rate;

            // Prevent phase from becoming a special number, rendering it unusable forever.
            if (Double.IsNaN(phase) || Double.IsInfinity(phase))
            {
                return Double.NaN;
            }
            _phase = phase;

            double shiftedPhase = _phase + phaseShift;

            double value = _randomCalculator.GetValue(_phase);

            _previousTime = time;

            return value;
        }

        public override void Reset(DimensionStack dimensionStack)
        {
            double time = dimensionStack.Get(DimensionEnum.Time);

            _previousTime = time;
            _phase = _randomCalculatorOffset;

            base.Reset(dimensionStack);
        }
    }
}
