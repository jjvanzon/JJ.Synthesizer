using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.SynthesizerPrototype.Tests.Helpers;

namespace JJ.Business.SynthesizerPrototype.Tests.Calculation.WithStructs
{
    /// <summary>
    /// This is the only calculator that needs to be a reference type,
    /// because other object will write values to the same instance, also referenced from multiple.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal class VariableInput_OperatorCalculator : IOperatorCalculator
    {
        /// <summary> Public field for performance. </summary>
        public double _value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate()
        {
            return _value;
        }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
