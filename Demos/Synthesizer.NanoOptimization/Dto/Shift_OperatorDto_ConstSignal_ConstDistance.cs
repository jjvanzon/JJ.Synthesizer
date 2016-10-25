using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Shift_OperatorDto_ConstSignal_ConstDistance : OperatorDto
    {
        public double SignalValue { get; set; }
        public double Distance { get; set; }

        public Shift_OperatorDto_ConstSignal_ConstDistance(double signalValue, double distance)
            : base(new OperatorDto[0])
        {
            SignalValue = signalValue;
            Distance = distance;
        }
    }
}
