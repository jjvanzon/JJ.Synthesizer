using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class VariableInput_OperatorCalculator : OperatorCalculatorBase
    {
        public double _value;

        // TODO: This does not work either. MidiProcessor seems to keep reading out old values.

        //// Probably needed to make multiple threads access it,
        //// and not be stuck with old temporary values.
        //private object _valueLock = new object();
        //private double _value;

        ///// <summary> 
        ///// Assign values from user input to this variable.
        ///// For performance reasons it is not a property,
        ///// even though the performance impact has not been tested.
        ///// </summary>
        //public double Value
        //{
        //    get
        //    {
        //        lock (_valueLock)
        //        {
        //            return _value;
        //        }
        //    }
        //    set
        //    {
        //        lock (_valueLock)
        //        {
        //            _value = value;
        //        }
        //    }
        //}

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
            //return Value;
            return _value;
        }

        public override void ResetState()
        {
            //Value = InitialValue;
            _value = InitialValue;

            base.ResetState();
        }
    }
}
