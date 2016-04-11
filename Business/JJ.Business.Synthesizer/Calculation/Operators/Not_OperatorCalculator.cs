using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Not_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _xCalculator;

        public Not_OperatorCalculator(OperatorCalculatorBase xCalculator)
            : base(new OperatorCalculatorBase[] { xCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(xCalculator, () => xCalculator);

            _xCalculator = xCalculator;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double x = _xCalculator.Calculate(dimensionStack);

            bool xIsFalse = x == 0.0;

            if (xIsFalse) return 1.0;
            else return 0.0;
        }
    }
}