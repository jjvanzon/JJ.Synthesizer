using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.SynthesizerPrototype.Tests.Helpers;
using JJ.Business.SynthesizerPrototype.WithStructs.CopiedCode.From_JJ_Business_SynthesizerPrototype;

namespace JJ.Business.SynthesizerPrototype.WithStructs.Calculation
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public struct Multiply_OperatorCalculator_VarA_ConstB<TACalculator> 
        : IOperatorCalculator_VarA_ConstB
        where TACalculator : IOperatorCalculator
    {
        private TACalculator _aCalculator;
        public IOperatorCalculator ACalculator
        {
            get { return _aCalculator; }
            set { _aCalculator = (TACalculator)value; }
        }

        private double _b;
        public double B
        {
            get { return _b; }
            set { _b = value; }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate()
        {
            double a = _aCalculator.Calculate();

            return OperatorCalculatorHelper.Multiply(a, _b);
        }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
