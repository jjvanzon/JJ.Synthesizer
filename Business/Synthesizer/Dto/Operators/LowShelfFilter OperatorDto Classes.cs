using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto.Operators
{
    internal class LowShelfFilter_OperatorDto : LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar
    { }

    internal class LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsVar : OperatorDtoBase_ShelfFilter_SoundVarOrConst_OtherInputsVar
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.LowShelfFilter;
    }

    internal class LowShelfFilter_OperatorDto_SoundVarOrConst_OtherInputsConst : OperatorDtoBase_ShelfFilter_SoundVarOrConst_OtherInputsConst
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.LowShelfFilter;
    }
}
