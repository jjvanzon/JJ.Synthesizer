using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class SortOverDimension_OperatorDto : OperatorDtoBase_AggregateOverDimension
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.SortOverDimension);
    }

    internal class SortOverDimension_OperatorDto_CollectionRecalculationContinuous : SortOverDimension_OperatorDto
    { }

    internal class SortOverDimension_OperatorDto_CollectionRecalculationUponReset : SortOverDimension_OperatorDto
    { }
}
