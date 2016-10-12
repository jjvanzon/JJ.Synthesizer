using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Demos.Synthesizer.Inlining.Shared;

namespace JJ.Demos.Synthesizer.Inlining.WithGenericMutableStructsAndHelpers
{
    internal struct Multiply_OperatorCalculator_VarA_VarB<TACalculator, TBCalculator> : IOperatorCalculator
        where TACalculator : IOperatorCalculator
        where TBCalculator : IOperatorCalculator
    {
        public TACalculator _aCalculator;
        public TBCalculator _bCalculator;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate()
        {
            double a = _aCalculator.Calculate();
            double b = _bCalculator.Calculate();

            return OperatorCalculatorHelper.Multiply(a, b);
        }
    }
}
