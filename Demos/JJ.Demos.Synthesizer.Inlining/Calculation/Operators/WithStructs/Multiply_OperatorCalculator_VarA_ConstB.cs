using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Demos.Synthesizer.Inlining.Helpers;

namespace JJ.Demos.Synthesizer.Inlining.Calculation.Operators.WithStructs
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal struct Multiply_OperatorCalculator_VarA_ConstB<TACalculator> 
        : IOperatorCalculator_VarA_ConstB
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

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
