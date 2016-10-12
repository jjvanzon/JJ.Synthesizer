using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Demos.Synthesizer.Inlining.Shared;

namespace JJ.Demos.Synthesizer.Inlining.WithGenericMutableStructsAndHelpers
{
    internal struct Add_OperatorCalculator_VarA_VarB<TACalculator> : IOperatorCalculator
        where TACalculator : IOperatorCalculator
    {
        public TACalculator _aCalculator;
        public double _b;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate()
        {
            double a = _aCalculator.Calculate();

            return OperatorCalculatorHelper.Add(a, _b);
        }
    }
}
