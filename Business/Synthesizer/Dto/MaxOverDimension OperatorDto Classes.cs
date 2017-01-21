using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class MaxOverDimension_OperatorDto : OperatorDtoBase_AggregateOverDimension_AllVars
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.MaxOverDimension;
    }

    internal class MaxOverDimension_OperatorDto_ConstSignal : OperatorDtoBase_ConstSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.MaxOverDimension;
    }

    internal class MaxOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous : MaxOverDimension_OperatorDto
    { }

    internal class MaxOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset : MaxOverDimension_OperatorDto
    { }
}
