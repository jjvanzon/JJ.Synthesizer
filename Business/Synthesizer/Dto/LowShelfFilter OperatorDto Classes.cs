using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class LowShelfFilter_OperatorDto : LowShelfFilter_OperatorDto_AllVars
    { }

    internal class LowShelfFilter_OperatorDto_ConstSignal : OperatorDtoBase_ConstSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.LowShelfFilter;
    }

    internal class LowShelfFilter_OperatorDto_AllVars : OperatorDtoBase_ShelfFilter_AllVars
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.LowShelfFilter;
    }

    internal class LowShelfFilter_OperatorDto_ManyConsts : OperatorDtoBase_ShelfFilter_ManyConsts
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.LowShelfFilter;
    }
}
