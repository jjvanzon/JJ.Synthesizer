using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Mathematics;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Range_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _fromCalculator;
        private readonly OperatorCalculatorBase _tillCalculator;
        private readonly OperatorCalculatorBase _stepCalculator;
        private readonly DimensionStack _dimensionStack;

        public Range_OperatorCalculator(
            OperatorCalculatorBase fromCalculator,
            OperatorCalculatorBase tillCalculator,
            OperatorCalculatorBase stepCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { fromCalculator, tillCalculator, stepCalculator })
        {
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _fromCalculator = fromCalculator;
            _tillCalculator = tillCalculator;
            _stepCalculator = stepCalculator;
            _dimensionStack = dimensionStack;
        }

        public override double Calculate()
        {
            // Example:
            // index { 0, 1, 2 } => value { 0.5, 2.25, 4 }

            double from = _fromCalculator.Calculate();
            double till = _tillCalculator.Calculate();
            double step = _stepCalculator.Calculate();
            double index = _dimensionStack.Get();

            double nonRounded = from + index * step;
            double rounded = Maths.RoundWithStep(nonRounded, step);

            return rounded;
        }
    }
}
