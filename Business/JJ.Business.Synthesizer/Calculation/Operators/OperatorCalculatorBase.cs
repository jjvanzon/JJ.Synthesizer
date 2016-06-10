using System.Diagnostics;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    // Dispatch through a base class is faster than using an interface.

    /// <summary>
    /// If you have child OperatorCalculators use OperatorCalculatorBase_WithChildCalculators instead.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal abstract class OperatorCalculatorBase
    {
        public abstract double Calculate();

        /// <summary> Does nothing </summary>
        public virtual void Reset() { }

        private string DebuggerDisplay
        {
            get { return DebugHelper.GetDebuggerDisplay(this); }
        }

    }
}
