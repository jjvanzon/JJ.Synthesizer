using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class HighShelfFilter_OperatorDto : HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar
    { }

    internal class HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar : OperatorDtoBase_ShelfFilter_SoundVarOrConst_OtherInputsVar
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.HighShelfFilter;
    }

    internal class HighShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst : OperatorDtoBase_ShelfFilter_SoundVarOrConst_OtherInputsConst
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.HighShelfFilter;
    }
}
