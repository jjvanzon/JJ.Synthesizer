using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JJ.Business.SynthesizerPrototype.WithInheritance.Helpers;

namespace JJ.Business.SynthesizerPrototype.WithInheritance.Calculation
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public abstract class OperatorCalculatorBase
    {
        public abstract double Calculate();

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
