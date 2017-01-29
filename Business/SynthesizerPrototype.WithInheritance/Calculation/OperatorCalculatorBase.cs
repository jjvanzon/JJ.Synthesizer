using System.Diagnostics;
using JJ.Business.SynthesizerPrototype.WithInheritance.Helpers;

namespace JJ.Business.SynthesizerPrototype.WithInheritance.Calculation
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public abstract class OperatorCalculatorBase
    {
        public abstract double Calculate();

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
