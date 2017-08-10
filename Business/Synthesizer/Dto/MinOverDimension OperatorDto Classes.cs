using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class MinOverDimension_OperatorDto : OperatorDtoBase_AggregateOverDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.MinOverDimension;
    }

    internal class MinOverDimension_OperatorDto_ConstSignal : OperatorDtoBase_WithSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.MinOverDimension;
    }

    internal class MinOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous : MinOverDimension_OperatorDto
    { }

    internal class MinOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset : MinOverDimension_OperatorDto
    { }
}
