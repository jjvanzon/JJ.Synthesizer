using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class SortOverDimension_OperatorDto : OperatorDtoBase_AggregateOverDimension_AllVars
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SortOverDimension;
    }

    internal class SortOverDimension_OperatorDto_ConstSignal : OperatorDtoBase_ConstSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SortOverDimension;
    }

    internal class SortOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous : SortOverDimension_OperatorDto
    { }

    internal class SortOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset : SortOverDimension_OperatorDto
    { }
}
