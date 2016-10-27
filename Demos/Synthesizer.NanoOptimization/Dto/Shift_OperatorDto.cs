using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Shift_OperatorDto : OperatorDto
    {
        public OperatorDto SignalOperatorDto => ChildOperatorDtos[0];
        public OperatorDto DistanceOperatorDto => ChildOperatorDtos[1];
        public override string OperatorName => OperatorNames.Shift;

        public Shift_OperatorDto(OperatorDto signalOperatorDto, OperatorDto distanceOperatorDto)
            : base(new OperatorDto[] { signalOperatorDto, distanceOperatorDto })
        { }

    }
}