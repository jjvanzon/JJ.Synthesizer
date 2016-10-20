using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Shift_OperatorDto_VarSignal_ConstDistance : OperatorDto
    {
        public InletDto SignalInletDto { get; set; }
        public double Distance { get; set; }

        public Shift_OperatorDto_VarSignal_ConstDistance(InletDto signalInletDto, double distance)
            : base(new InletDto[] { signalInletDto })
        {
            Distance = distance;
            SignalInletDto = signalInletDto;
        }
    }
}
