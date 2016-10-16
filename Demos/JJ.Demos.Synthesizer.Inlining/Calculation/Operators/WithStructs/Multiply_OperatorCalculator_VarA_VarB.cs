using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Demos.Synthesizer.Inlining.Calculation.Operators.WithStructs
{
    internal struct Multiply_OperatorCalculator_VarA_VarB<TACalculator, TBCalculator>
        : IOperatorCalculator_VarA_VarB
        where TACalculator : struct, IOperatorCalculator
        where TBCalculator : struct, IOperatorCalculator
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

        IOperatorCalculator IOperatorCalculator_VarA_VarB.ACalculator
        {
            get { return _aCalculator; }
            set { _aCalculator = (TACalculator)value; }
        }

        IOperatorCalculator IOperatorCalculator_VarA_VarB.BCalculator
        {
            get { return _bCalculator; }
            set { _bCalculator = (TBCalculator)value; }
        }
    }
}
