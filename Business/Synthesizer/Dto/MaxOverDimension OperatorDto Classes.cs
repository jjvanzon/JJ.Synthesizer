using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class MaxOverDimension_OperatorDto : OperatorDtoBase_AggregateOverDimension
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MaxOverDimension);
    }

    internal class MaxOverDimension_OperatorDto_CollectionRecalculationContinuous : MaxOverDimension_OperatorDto
    { }

    internal class MaxOverDimension_OperatorDto_CollectionRecalculationUponReset : MaxOverDimension_OperatorDto
    { }
}
