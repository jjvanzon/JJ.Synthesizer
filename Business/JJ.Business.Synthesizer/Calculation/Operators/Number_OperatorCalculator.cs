using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal class Number_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly double _number;

        /// <summary>
        /// For derived classes that must be polymorphically related to the number operator,
        /// but do not actually use _number.
        /// </summary>
        protected Number_OperatorCalculator()
        { }

        public Number_OperatorCalculator(double number)
        {
            _number = number;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            return _number;
        }

        private string DebuggerDisplay => _number.ToString();
    }
}
