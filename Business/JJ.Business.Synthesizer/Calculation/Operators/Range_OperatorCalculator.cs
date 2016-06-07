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
            // position { 0, 1, 2 } => value { 0.5, 2.25, 4 }

            // TODO: This calculation should be simplified to something like this,
            // but then correct:
            //double valueNonRounded = from + (destRange / step) * position;

            double position = _dimensionStack.Get();
            double from = _fromCalculator.Calculate();
            double till = _tillCalculator.Calculate();
            double step = _stepCalculator.Calculate();

            double targetValueA = from;
            double targetValueB = till;
            double targetRange = targetValueB - targetValueA;

            double count = targetRange / step;

            double sourceRange = count;
            double sourceValueA = 0;
            double sourceValueB = count;

            double between0And1 = (position - sourceValueA) / sourceRange;
            double valueNonRounded = between0And1 * targetRange + targetValueA;

            double valueRounded = Maths.RoundWithStep(valueNonRounded, step);

            return valueRounded;
        }
    }
}
