using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers;

namespace JJ.Demos.Synthesizer.NanoOptimization.Calculation.Operators.WithInheritance
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal abstract class OperatorCalculatorBase
    {
        public abstract double Calculate();

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
