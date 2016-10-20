using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Shift_OperatorDto_VarSignal_VarDistance : OperatorDto
    {
        public InletDto SignalInletDto { get; set; }
        public InletDto DistanceInletDto { get; set; }

        public Shift_OperatorDto_VarSignal_VarDistance(InletDto signalInletDto, InletDto distanceInletDto)
            : base(new InletDto[] { signalInletDto, distanceInletDto })
        {
            SignalInletDto = signalInletDto;
            DistanceInletDto = distanceInletDto;
        }
    }
}
