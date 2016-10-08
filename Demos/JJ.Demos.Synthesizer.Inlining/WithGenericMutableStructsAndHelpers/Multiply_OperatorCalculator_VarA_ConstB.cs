using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Demos.Synthesizer.Inlining.WithGenericMutableStructsAndHelpers
{
    internal struct Multiply_OperatorCalculator_VarA_ConstB<TACalculator> : IOperatorCalculator
        where TACalculator : IOperatorCalculator
    {
        public TACalculator _aCalculator;
        public double _b;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate()
        {
            double a = _aCalculator.Calculate();

            return OperatorCalculatorHelper.Multiply(a, _b);
        }
    }
}
