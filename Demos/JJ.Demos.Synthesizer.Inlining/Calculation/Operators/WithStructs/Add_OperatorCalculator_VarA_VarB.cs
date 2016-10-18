using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Demos.Synthesizer.Inlining.Helpers;

namespace JJ.Demos.Synthesizer.Inlining.Calculation.Operators.WithStructs
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal struct Add_OperatorCalculator_VarA_VarB<TACalculator, TBCalculator>
        : IOperatorCalculator_VarA_VarB
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

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}