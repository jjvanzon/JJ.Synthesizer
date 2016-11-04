using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Demos.Synthesizer.NanoOptimization.Calculation.WithInheritance
{
    internal class VariableInput_OperatorCalculator : OperatorCalculatorBase
    {
        public VariableInput_OperatorCalculator(double defaultValue)
        {
            _value = defaultValue;
        }

        /// <summary> Public field for performance. </summary>
        public double _value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            return _value;
        }
    }
}
