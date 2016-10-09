using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Demos.Synthesizer.Inlining.WithGenericMutableStructsAndHelpers
{
    internal struct Add_OperatorCalculator_VarA_VarB<TACalculator, TBCalculator> : IOperatorCalculator
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

            return OperatorCalculatorHelper.Add(a, b);
        }
    }
}
