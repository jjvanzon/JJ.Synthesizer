using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JJ.Business.Synthesizer.Tests.NanoOptimization.Helpers;

namespace JJ.Business.Synthesizer.Tests.NanoOptimization.Calculation.WithInheritance
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal abstract class OperatorCalculatorBase
    {
        public abstract double Calculate();

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
