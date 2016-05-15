using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation.Random;
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
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _phase;
        private double _previousPosition;

        public Random_OperatorCalculator_BlockAndStripe_VarFrequency_VarPhaseShift(
            RandomCalculatorBase randomCalculator,
            double randomCalculatorOffset,
            OperatorCalculatorBase rateCalculator,
            OperatorCalculatorBase phaseShiftCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { rateCalculator, phaseShiftCalculator })
        {
            if (randomCalculator == null) throw new NullException(() => randomCalculator);
            // TODO: Make assertion strict again, once you have more calculator variations.
            //OperatorCalculatorHelper.AssertOperatorCalculatorBase(frequencyCalculator, () => frequencyCalculator);
            //OperatorCalculatorHelper.AssertOperatorCalculatorBase(phaseShiftCalculator, () => phaseShiftCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForReaders(dimensionStack);

            _randomCalculator = randomCalculator;
            _randomCalculatorOffset = randomCalculatorOffset;
            _rateCalculator = rateCalculator;
            _phaseShiftCalculator = phaseShiftCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;

            // TODO: Make sure you asser this strictly, so it does not become NaN.
            _phase = _randomCalculatorOffset;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionStackIndex);

            double rate = _rateCalculator.Calculate();
            double phaseShift = _phaseShiftCalculator.Calculate();

            double positionChange = position - _previousPosition;
            double phase = _phase + positionChange * rate;

            _phase = phase;

            double shiftedPhase = _phase + phaseShift;

            double value = _randomCalculator.GetValue(_phase);

            _previousPosition = position;

            return value;
        }

        public override void Reset()
        {
            _previousPosition = _dimensionStack.Get(_dimensionStackIndex);

            base.Reset();
        }
    }
}
