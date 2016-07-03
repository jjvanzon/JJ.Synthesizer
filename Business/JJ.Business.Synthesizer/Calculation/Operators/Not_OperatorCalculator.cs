using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Not_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _xCalculator;

        public Not_OperatorCalculator(OperatorCalculatorBase xCalculator)
            : base(new OperatorCalculatorBase[] { xCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(xCalculator, () => xCalculator);

            _xCalculator = xCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double x = _xCalculator.Calculate();

            bool xIsFalse = x == 0.0;

            if (xIsFalse) return 1.0;
            else return 0.0;
        }
    }
}