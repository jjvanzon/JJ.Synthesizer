using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Shift_OperatorDto : OperatorDtoBase
    {
        public OperatorDtoBase SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase DistanceOperatorDto => InputOperatorDtos[1];
        public override string OperatorTypeName => OperatorNames.Shift;

        public Shift_OperatorDto(OperatorDtoBase signalOperatorDto, OperatorDtoBase distanceOperatorDto)
            : base(new OperatorDtoBase[] { signalOperatorDto, distanceOperatorDto })
        { }

    }
}