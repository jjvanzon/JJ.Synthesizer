using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Demos.Synthesizer.Inlining.Calculation.Operators.WithStructs
{
    internal struct Multiply_OperatorCalculator_VarA_ConstB<TACalculator> 
        : IMultiply_OperatorDto_VarA_ConstB
        where TACalculator : struct, IOperatorCalculator
    {
        public TACalculator _aCalculator;
        public double _b;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate()
        {
            double a = _aCalculator.Calculate();

            return OperatorCalculatorHelper.Multiply(a, _b);
        }

        IOperatorCalculator IMultiply_OperatorDto_VarA_ConstB.ACalculator
        {
            get { return _aCalculator; }
            set { _aCalculator = (TACalculator)value; }
        }

        double IMultiply_OperatorDto_VarA_ConstB.B
        {
            get { return _b; }
            set { _b = value; }
        }
    }
}
