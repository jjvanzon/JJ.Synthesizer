using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto.Operators
{
    internal class MinOverDimension_OperatorDto : OperatorDtoBase_AggregateOverDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.MinOverDimension;
    }

    internal class MinOverDimension_OperatorDto_CollectionRecalculationContinuous : MinOverDimension_OperatorDto
    { }

    internal class MinOverDimension_OperatorDto_CollectionRecalculationUponReset : MinOverDimension_OperatorDto
    { }

    internal class MinOverDimension_OperatorDto_ConstSignal : MinOverDimension_OperatorDto
    { }
}
