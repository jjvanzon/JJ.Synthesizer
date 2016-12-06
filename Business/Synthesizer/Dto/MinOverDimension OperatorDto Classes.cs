using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class MinOverDimension_OperatorDto_AllVars : OperatorDtoBase_AggregateOverDimension_AllVars
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MinOverDimension);
    }

    internal class MinOverDimension_OperatorDto_ConstSignal : OperatorDtoBase_ConstSignal
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MinOverDimension);
    }

    internal class MinOverDimension_OperatorDto_AllVars_CollectionRecalculationContinuous : MinOverDimension_OperatorDto_AllVars
    { }

    internal class MinOverDimension_OperatorDto_AllVars_CollectionRecalculationUponReset : MinOverDimension_OperatorDto_AllVars
    { }
}
