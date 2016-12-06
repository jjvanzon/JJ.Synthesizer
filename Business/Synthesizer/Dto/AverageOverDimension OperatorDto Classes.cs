using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class AverageOverDimension_OperatorDto_AllVars : OperatorDtoBase_AggregateOverDimension_AllVars
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.AverageOverDimension);
    }

    internal class AverageOverDimension_OperatorDto_ConstSignal : OperatorDtoBase_ConstSignal
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.AverageOverDimension);
    }

    internal class AverageOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous : AverageOverDimension_OperatorDto_AllVars
    { }

    internal class AverageOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset : AverageOverDimension_OperatorDto_AllVars
    { }
}
