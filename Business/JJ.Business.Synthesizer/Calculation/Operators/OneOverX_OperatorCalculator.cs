using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class OneOverX_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _xCalculator;

        public OneOverX_OperatorCalculator(OperatorCalculatorBase xCalculator)
            : base(new OperatorCalculatorBase[] { xCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(xCalculator, () => xCalculator);

            _xCalculator = xCalculator;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double x = _xCalculator.Calculate(dimensionStack);
            return 1 / x;
        }
    }
}