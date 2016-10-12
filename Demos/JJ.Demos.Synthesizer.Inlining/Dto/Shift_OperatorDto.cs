using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.Inlining.Dto
{
    internal class Shift_OperatorDto : OperatorDto
    {
        public Shift_OperatorDto(InletDto signalInletDto, InletDto distanceInletDto)
        {
            SignalInletDto = signalInletDto;
            DistanceInletDto = distanceInletDto;
        }

        public InletDto SignalInletDto { get; set; }
        public InletDto DistanceInletDto { get; set; }
    }
}