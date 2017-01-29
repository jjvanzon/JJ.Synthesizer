using System.Diagnostics;
using System.Runtime.CompilerServices;
using JJ.Business.SynthesizerPrototype.WithStructs.Helpers;

namespace JJ.Business.SynthesizerPrototype.WithStructs.Calculation
{
    /// <summary>
    /// This is the only calculator that needs to be a reference type,
    /// because other object will write values to the same instance, also referenced from multiple.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class VariableInput_OperatorCalculator : IOperatorCalculator
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
