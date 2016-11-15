using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Exponent_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Exponent);

        public OperatorDtoBase LowOperatorDto { get; set; }
        public OperatorDtoBase HighOperatorDto { get; set; }
        public OperatorDtoBase RatioOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos => new OperatorDtoBase[] { LowOperatorDto, HighOperatorDto, RatioOperatorDto };
    }
}