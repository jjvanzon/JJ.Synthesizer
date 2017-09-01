using System;
using JJ.Framework.Mathematics;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class RangeOverDimension_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _fromCalculator;
        private readonly OperatorCalculatorBase _tillCalculator;
        private readonly OperatorCalculatorBase _stepCalculator;
        private readonly OperatorCalculatorBase _positionCalculator;

        public RangeOverDimension_OperatorCalculator(
            OperatorCalculatorBase fromCalculator,
            OperatorCalculatorBase tillCalculator,
            OperatorCalculatorBase stepCalculator,
            OperatorCalculatorBase positionCalculator)
            : base(new[] { fromCalculator, tillCalculator, stepCalculator, positionCalculator })
        {
            _fromCalculator = fromCalculator;
            _tillCalculator = tillCalculator;
            _stepCalculator = stepCalculator;
            _positionCalculator = positionCalculator;
        }

        public override double Calculate()
        {
            // Example:
            // index { 0, 1, 2 } => value { 0.5, 2.25, 4 }

            double position = _positionCalculator.Calculate();
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
}