using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class AverageOverDimension_OperatorDto : OperatorDtoBase_AggregateOverDimension
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.AverageOverDimension);
    }

    internal class AverageOverDimension_OperatorDto_CollectionRecalculationContinuous : OperatorDtoBase_AggregateOverDimension
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.AverageOverDimension);
    }

    internal class AverageOverDimension_OperatorDto_CollectionRecalculationUponReset : OperatorDtoBase_AggregateOverDimension
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.AverageOverDimension);
    }
}
