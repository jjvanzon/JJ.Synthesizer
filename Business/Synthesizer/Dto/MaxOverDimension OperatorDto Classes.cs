using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class MaxOverDimension_OperatorDto : OperatorDtoBase_AggregateOverDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.MaxOverDimension;
    }

    internal class MaxOverDimension_OperatorDto_ConstSignal : OperatorDtoBase_WithSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.MaxOverDimension;
    }

    internal class MaxOverDimension_OperatorDto_SoundVarOrConst_OtherInputsVar_CollectionRecalculationContinuous : MaxOverDimension_OperatorDto
    { }

    internal class MaxOverDimension_OperatorDto_SoundVarOrConst_OtherInputsVar_CollectionRecalculationUponReset : MaxOverDimension_OperatorDto
    { }
}
