using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Shift_OperatorDto : OperatorDto
    {
        public OperatorDto SignalOperatorDto { get { return ChildOperatorDtos[0]; } }
        public OperatorDto DistanceOperatorDto { get { return ChildOperatorDtos[1]; } }

        public Shift_OperatorDto(OperatorDto signalOperatorDto, OperatorDto distanceOperatorDto)
            : base(new OperatorDto[] { signalOperatorDto, distanceOperatorDto })
        { }
    }
}