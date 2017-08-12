using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class AverageOverDimension_OperatorDto : OperatorDtoBase_AggregateOverDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.AverageOverDimension;
    }

    internal class AverageOverDimension_OperatorDto_ConstSignal : OperatorDtoBase_WithSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.AverageOverDimension;
    }

    internal class AverageOverDimension_OperatorDto_SoundVarOrConst_OtherInputsVar_CollectionRecalculationContinuous : AverageOverDimension_OperatorDto
    { }

    internal class AverageOverDimension_OperatorDto_SoundVarOrConst_OtherInputsVar_CollectionRecalculationUponReset : AverageOverDimension_OperatorDto
    { }
}
