using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class MinOverDimension_OperatorDto : OperatorDtoBase_AggregateOverDimension
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MinOverDimension);
    }

    internal class MinOverDimension_OperatorDto_CollectionRecalculationContinuous : MinOverDimension_OperatorDto
    { }

    internal class MinOverDimension_OperatorDto_CollectionRecalculationUponReset : MinOverDimension_OperatorDto
    { }
}
