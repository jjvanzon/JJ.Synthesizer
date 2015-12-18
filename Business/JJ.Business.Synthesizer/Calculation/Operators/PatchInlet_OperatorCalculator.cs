using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class PatchInlet_OperatorCalculator : OperatorCalculatorBase
    {
        /// <summary> 
        /// Assign values from user input to this variable.
        /// For performance reasons it is not a property,
        /// even though the performance impact has not been tested.
        /// </summary>
        public double _value;

        public InletTypeEnum InletTypeEnum { get; set; }
        public string Name { get; set; }
        public int ListIndex { get; set; }

        public override double Calculate(double time, int channelIndex)
        {
            return _value;
        }
    }
}
