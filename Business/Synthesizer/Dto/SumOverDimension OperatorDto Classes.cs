using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class SumOverDimension_OperatorDto : OperatorDtoBase_AggregateOverDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SumOverDimension;
    }

    internal class SumOverDimension_OperatorDto_AllConsts : SumOverDimension_OperatorDto
    { }

    internal class SumOverDimension_OperatorDto_CollectionRecalculationContinuous : SumOverDimension_OperatorDto
    { }

    internal class SumOverDimension_OperatorDto_CollectionRecalculationUponReset : SumOverDimension_OperatorDto
    { }
}
