using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Demos.Synthesizer.Inlining.Calculation.Operators.WithStructs
{
    internal struct Add_OperatorCalculator_VarA_ConstB<TACalculator> 
        : IOperatorCalculator_VarA_ConstB
        where TACalculator : struct, IOperatorCalculator
    {
        public TACalculator _aCalculator;
        public double _b;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate()
        {
            double a = _aCalculator.Calculate();

            return OperatorCalculatorHelper.Add(a, _b);
        }

        IOperatorCalculator IOperatorCalculator_VarA_ConstB.ACalculator
        {
            get { return _aCalculator; }
            set { _aCalculator = (TACalculator)value; }
        }

        double IOperatorCalculator_VarA_ConstB.B
        {
            get { return _b; }
            set { _b = value; }
        }
    }
}
