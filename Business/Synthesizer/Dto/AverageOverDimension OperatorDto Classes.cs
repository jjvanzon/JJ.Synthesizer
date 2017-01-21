using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class AverageOverDimension_OperatorDto : OperatorDtoBase_AggregateOverDimension_AllVars
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.AverageOverDimension;
    }

    internal class AverageOverDimension_OperatorDto_ConstSignal : OperatorDtoBase_ConstSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.AverageOverDimension;
    }

    internal class AverageOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous : AverageOverDimension_OperatorDto
    { }

    internal class AverageOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset : AverageOverDimension_OperatorDto
    { }
}
