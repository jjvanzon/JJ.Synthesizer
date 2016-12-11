using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Or_OperatorCalculator_VarA_VarB : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _aCalculator;
        private readonly OperatorCalculatorBase _bCalculator;

        public Or_OperatorCalculator_VarA_VarB(
            OperatorCalculatorBase aCalculator,
            OperatorCalculatorBase bCalculator)
            : base(new OperatorCalculatorBase[] { aCalculator, bCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(aCalculator, () => aCalculator);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(bCalculator, () => bCalculator);

            _aCalculator = aCalculator;
            _bCalculator = bCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double a = _aCalculator.Calculate();
            double b = _bCalculator.Calculate();

            bool aIsTrue = a != 0.0;
            bool bIsTrue = b != 0.0;

            if (aIsTrue || bIsTrue) return 1.0;
            else return 0.0;
        }
    }
}