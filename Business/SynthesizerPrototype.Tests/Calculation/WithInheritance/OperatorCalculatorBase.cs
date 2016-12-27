using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JJ.Business.SynthesizerPrototype.Tests.Helpers;

namespace JJ.Business.SynthesizerPrototype.Tests.Calculation.WithInheritance
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal abstract class OperatorCalculatorBase
    {
        public abstract double Calculate();

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
