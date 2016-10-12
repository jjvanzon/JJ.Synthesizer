using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Demos.Synthesizer.Inlining.Calculation.Operators.WithStructs
{
    internal struct VariableInput_OperatorCalculator : IOperatorCalculator
    {
        public double _value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate()
        {
            return _value;
        }
    }
}
