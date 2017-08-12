using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class SumOverDimension_OperatorDto : OperatorDtoBase_AggregateOverDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SumOverDimension;
    }

    internal class SumOverDimension_OperatorDto_AllConsts : SumOverDimension_OperatorDto
    { }

    internal class SumOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationContinuous : SumOverDimension_OperatorDto
    { }

    internal class SumOverDimension_OperatorDto_SignalVarOrConst_OtherInputsVar_CollectionRecalculationUponReset : SumOverDimension_OperatorDto
    { }
}
