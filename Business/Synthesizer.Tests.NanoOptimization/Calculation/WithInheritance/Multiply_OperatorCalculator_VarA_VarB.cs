using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Tests.NanoOptimization.Calculation.WithInheritance
{
    internal class Multiply_OperatorCalculator_VarA_VarB : OperatorCalculatorBase
    {
        private readonly OperatorCalculatorBase _aCalculator;
        private readonly OperatorCalculatorBase _bCalculator;

        public Multiply_OperatorCalculator_VarA_VarB(
            OperatorCalculatorBase aCalculator,
            OperatorCalculatorBase bCalculator)
        {
            if (aCalculator == null) throw new NullException(() => aCalculator);
            if (bCalculator == null) throw new NullException(() => bCalculator);

            _aCalculator = aCalculator;
            _bCalculator = bCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double a = _aCalculator.Calculate();
            double b = _bCalculator.Calculate();
            double result = OperatorCalculatorHelper.Multiply(a, b);
            return result;
        }
    }
}
