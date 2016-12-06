using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class SortOverDimension_OperatorDto_AllVars : OperatorDtoBase_AggregateOverDimension_AllVars
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.SortOverDimension);
    }

    internal class SortOverDimension_OperatotDto_ConstSignal : OperatorDtoBase_ConstSignal
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.SortOverDimension);
    }

    internal class SortOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous : SortOverDimension_OperatorDto_AllVars
    { }

    internal class SortOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset : SortOverDimension_OperatorDto_AllVars
    { }
}
