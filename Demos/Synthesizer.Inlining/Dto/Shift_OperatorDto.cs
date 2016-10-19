using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.Inlining.Dto
{
    internal class Shift_OperatorDto : OperatorDto
    {
        public InletDto SignalInletDto { get; set; }
        public InletDto DistanceInletDto { get; set; }

        public Shift_OperatorDto(InletDto signalInletDto, InletDto distanceInletDto)
            : base(new InletDto[] { signalInletDto, distanceInletDto })
        {
            SignalInletDto = signalInletDto;
            DistanceInletDto = distanceInletDto;
        }
    }
}