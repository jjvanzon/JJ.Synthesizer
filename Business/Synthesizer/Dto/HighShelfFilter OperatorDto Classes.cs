using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class HighShelfFilter_OperatorDto : HighShelfFilter_OperatorDto_AllVars
    { }

    internal class HighShelfFilter_OperatorDto_ConstSignal : OperatorDtoBase_ConstSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.HighShelfFilter;
    }

    internal class HighShelfFilter_OperatorDto_AllVars : OperatorDtoBase_ShelfFilter_AllVars
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.HighShelfFilter;
    }

    internal class HighShelfFilter_OperatorDto_ManyConsts : OperatorDtoBase_ShelfFilter_ManyConsts
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.HighShelfFilter;
    }
}
