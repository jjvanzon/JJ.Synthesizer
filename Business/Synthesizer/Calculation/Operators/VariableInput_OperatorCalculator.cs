using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class VariableInput_OperatorCalculator : OperatorCalculatorBase
    {
        /// <summary> Public field for performance. </summary>
        public double _value;

        public DimensionEnum DimensionEnum { get; }
        public string CanonicalName { get; }
        public int ListIndex { get; }

        public VariableInput_OperatorCalculator(
            DimensionEnum dimensionEnum, 
            string canonicalName,
            int listIndex, 
            double defaultValue)
        {
            DimensionEnum = dimensionEnum;
            CanonicalName = canonicalName;
            ListIndex = listIndex;

            _value = defaultValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate() => _value;

        // NOTE: Do not override the Reset() method to reset it to the default value,
        // because Resetting part of the calculation does not mean resetting the variables.
        // It means resetting the calculation, but WITH the new variables.
    }
}
