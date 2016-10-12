using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Demos.Synthesizer.Inlining.Calculation.Operators.WithInheritance
{
    internal class VariableInput_OperatorCalculator : OperatorCalculatorBase
    {
        /// <summary> Public field for performance. </summary>
        public double _value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            return _value;
        }
    }
}
