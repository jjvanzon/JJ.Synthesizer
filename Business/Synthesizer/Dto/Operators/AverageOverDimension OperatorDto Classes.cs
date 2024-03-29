﻿using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto.Operators
{
    internal class AverageOverDimension_OperatorDto : OperatorDtoBase_AggregateOverDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.AverageOverDimension;
    }

    internal class AverageOverDimension_OperatorDto_CollectionRecalculationContinuous : AverageOverDimension_OperatorDto
    { }

    internal class AverageOverDimension_OperatorDto_CollectionRecalculationUponReset : AverageOverDimension_OperatorDto
    { }
}
