using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Shift_OperatorDto_ConstSignal_ConstDistance : OperatorDtoBase
    {
        public double SignalValue { get; set; }
        public double Distance { get; set; }
        public override string OperatorTypeName => OperatorNames.Shift;

        public Shift_OperatorDto_ConstSignal_ConstDistance(double signalValue, double distance)
            : base(new OperatorDtoBase[0])
        {
            SignalValue = signalValue;
            Distance = distance;
        }
    }
}
