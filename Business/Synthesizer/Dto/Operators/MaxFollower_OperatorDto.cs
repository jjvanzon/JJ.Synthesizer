﻿using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto.Operators
{
    internal class MaxFollower_OperatorDto : OperatorDtoBase_AggregateFollower
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.MaxFollower;
    }
}