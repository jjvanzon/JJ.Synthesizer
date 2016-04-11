using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class VariableInput_OperatorCalculator : OperatorCalculatorBase
    {
        /// <summary> Public field for performance. </summary>
        public double _value;

        public DimensionEnum DimensionEnum { get; private set; }
        public string Name { get; private set; }
        public int ListIndex { get; private set; }

        public VariableInput_OperatorCalculator(DimensionEnum dimensionEnum, string name, int listIndex, double defaultValue)
        {
            DimensionEnum = dimensionEnum;
            Name = name;
            ListIndex = listIndex;

            _value = defaultValue;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            return _value;
        }

        // NOTE: Do not override the Reset() method to reset it to the default value,
        // because Resetting part of the calculation does not mean resetting the variables.
        // It means resetting the calculation, but WITH the new variables.
    }
}
