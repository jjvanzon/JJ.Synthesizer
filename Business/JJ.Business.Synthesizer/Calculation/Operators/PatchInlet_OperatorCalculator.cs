using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    /// <summary> Not used yet (2015-12-18). </summary>
    internal class PatchInlet_OperatorCalculator : OperatorCalculatorBase
    {
        /// <summary> 
        /// Assign values from user input to this variable.
        /// For performance reasons it is not a property,
        /// even though the performance impact has not been tested.
        /// </summary>
        public double _value;

        public override double Calculate(double time, int channelIndex)
        {
            return _value;
        }
    }
}
