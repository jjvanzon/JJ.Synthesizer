using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class VariableInput_OperatorCalculator : OperatorCalculatorBase
    {
        /// <summary> 
        /// Assign values from user input to this variable.
        /// For performance reasons it is not a property,
        /// even though the performance impact has not been tested.
        /// </summary>
        public double _value;

        public InletTypeEnum InletTypeEnum { get; private set; }
        public string Name { get; private set; }
        public int ListIndex { get; private set; }
        public double InitialValue { get; private set; }

        public VariableInput_OperatorCalculator(InletTypeEnum inletTypeEnum, string name, int listIndex, double defaultValue)
        {
            InletTypeEnum = inletTypeEnum;
            Name = name;
            ListIndex = listIndex;
            InitialValue = defaultValue;

            _value = InitialValue;
        }

        public override double Calculate(double time, int channelIndex)
        {
            return _value;
        }

        public override void ResetState()
        {
            _value = InitialValue;

            base.ResetState();
        }
    }
}
