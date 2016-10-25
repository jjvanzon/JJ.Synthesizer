using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Shift_OperatorDto_VarSignal_ConstDistance : OperatorDto
    {
        public OperatorDto SignalOperatorDto { get { return ChildOperatorDtos[0]; } }
        public double Distance { get; set; }

        public Shift_OperatorDto_VarSignal_ConstDistance(OperatorDto signalOperatorDto, double distance)
            : base(new OperatorDto[] { signalOperatorDto })
        {
            Distance = distance;
        }
    }
}
