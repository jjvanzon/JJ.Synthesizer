using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers;

namespace JJ.Demos.Synthesizer.NanoOptimization.Calculation.Operators.WithStructs
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal struct Add_OperatorCalculator_VarA_ConstB<TACalculator> 
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

            return OperatorCalculatorHelper.Add(a, _b);
        }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
