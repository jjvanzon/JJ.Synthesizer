using System;
using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_Trigger : OperatorDtoBase
    {
        public OperatorDtoBase PassThroughInputOperatorDto { get; set; }
        public OperatorDtoBase ResetOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos => new OperatorDtoBase[] { PassThroughInputOperatorDto, ResetOperatorDto };
    }
}
