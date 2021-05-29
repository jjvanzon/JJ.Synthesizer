using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto.Operators
{
    internal class MaxOverDimension_OperatorDto : OperatorDtoBase_AggregateOverDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.MaxOverDimension;
    }

    internal class MaxOverDimension_OperatorDto_CollectionRecalculationContinuous : MaxOverDimension_OperatorDto
    { }

    internal class MaxOverDimension_OperatorDto_CollectionRecalculationUponReset : MaxOverDimension_OperatorDto
    { }

    internal class MaxOverDimension_OperatorDto_ConstSignal : MaxOverDimension_OperatorDto
    { }
}
