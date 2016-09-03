using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class VariableInput_OperatorCalculator : OperatorCalculatorBase
    {
        /// <summary> Public field for performance. </summary>
        public double _value;

        public DimensionEnum DimensionEnum { get; private set; }
        public string CanonicalName { get; private set; }
        public int ListIndex { get; private set; }

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
        public override double Calculate()
        {
            return _value;
        }

        // NOTE: Do not override the Reset() method to reset it to the default value,
        // because Resetting part of the calculation does not mean resetting the variables.
        // It means resetting the calculation, but WITH the new variables.
    }
}
