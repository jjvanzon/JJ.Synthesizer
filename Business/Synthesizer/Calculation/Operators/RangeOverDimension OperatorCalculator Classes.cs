using System;
using JJ.Framework.Mathematics;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class RangeOverDimension_OperatorCalculator_OnlyVars : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _fromCalculator;
        private readonly OperatorCalculatorBase _tillCalculator;
        private readonly OperatorCalculatorBase _stepCalculator;
        private readonly DimensionStack _dimensionStack;

        public RangeOverDimension_OperatorCalculator_OnlyVars(
            OperatorCalculatorBase fromCalculator,
            OperatorCalculatorBase tillCalculator,
            OperatorCalculatorBase stepCalculator,
            DimensionStack dimensionStack)
            : base(new[] { fromCalculator, tillCalculator, stepCalculator })
        {
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _fromCalculator = fromCalculator;
            _tillCalculator = tillCalculator;
            _stepCalculator = stepCalculator;
            _dimensionStack = dimensionStack;
        }

        public override double Calculate()
        {
            // Example:
            // index { 0, 1, 2 } => value { 0.5, 2.25, 4 }

            double position = _dimensionStack.Get();
            if (position < 0.0) return 0.0;

            double from = _fromCalculator.Calculate();
            double till = _tillCalculator.Calculate();
            double step = _stepCalculator.Calculate();

            double valueNonRounded = from + position * step;

            double upperBound = till + step; // Sustain last value for the length a of step.

            if (valueNonRounded > upperBound) return 0.0;

            // Correct so that we round down and never up.
            double valueNonRoundedCorrected = valueNonRounded - step / 2.0;

            double valueRounded = MathHelper.RoundWithStep(valueNonRoundedCorrected, step);

            return valueRounded;
        }
    }

    internal class RangeOverDimension_OperatorCalculator_OnlyConsts : OperatorCalculatorBase
    {
        private readonly double _from;
        private readonly double _step;
        private readonly double _tillPlusStep;
        private readonly double _stepDividedBy2;
        private readonly DimensionStack _dimensionStack;

        public RangeOverDimension_OperatorCalculator_OnlyConsts(
            double from,
            double till,
            double step,
            DimensionStack dimensionStack)
        {
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _from = from;
            _step = step;
            _dimensionStack = dimensionStack;

            _tillPlusStep = till + _step;
            _stepDividedBy2 = _step / 2.0;
        }

        public override double Calculate()
        {
            // Example:
            // index { 0, 1, 2 } => value { 0.5, 2.25, 4 }

            double position = _dimensionStack.Get();
            if (position < 0.0) return 0.0;

            double valueNonRounded = _from + position * _step;

            double upperBound = _tillPlusStep; // Sustain last value for the length a of step.

            if (valueNonRounded > upperBound) return 0.0;

            // Correct so that we round down and never up.
            double valueNonRoundedCorrected = valueNonRounded - _stepDividedBy2;

            double valueRounded = MathHelper.RoundWithStep(valueNonRoundedCorrected, _step);

            return valueRounded;
        }
    }

    internal class RangeOverDimension_OperatorCalculator_WithConsts_AndStepOne : OperatorCalculatorBase
    {
        private readonly double _from;
        private readonly double _tillPlusOne;
        private readonly DimensionStack _dimensionStack;

        public RangeOverDimension_OperatorCalculator_WithConsts_AndStepOne(
            double from,
            double till,
            DimensionStack dimensionStack)
        {
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _from = from;
            _dimensionStack = dimensionStack;

            _tillPlusOne = till + 1.0;
        }

        public override double Calculate()
        {
            // Example:
            // index { 0, 1, 2 } => value { 0.5, 2.25, 4 }

            double position = _dimensionStack.Get();
            if (position < 0.0) return 0.0;

            double valueNonRounded = _from + position;

            double upperBound = _tillPlusOne; // Sustain last value for the length a of step.

            if (valueNonRounded > upperBound) return 0.0;

            // Correct so that we round down and never up.
            double valueNonRoundedCorrected = valueNonRounded - 0.5;

            double valueRounded = Math.Round(valueNonRoundedCorrected, MidpointRounding.AwayFromZero);

            return valueRounded;
        }
    }
}
