using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Tests.NanoOptimization.Calculation.WithInheritance
{
    internal class Multiply_OperatorCalculator_VarA_ConstB : OperatorCalculatorBase
    {
        private readonly OperatorCalculatorBase _aCalculator;
        private readonly double _b;

        public Multiply_OperatorCalculator_VarA_ConstB(
            OperatorCalculatorBase aCalculator,
            double b)
        {
            if (aCalculator == null) throw new NullException(() => aCalculator);

            _aCalculator = aCalculator;
            _b = b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double a = _aCalculator.Calculate();
            double result = OperatorCalculatorHelper.Multiply(a, _b);
            return result;
        }
    }
}
