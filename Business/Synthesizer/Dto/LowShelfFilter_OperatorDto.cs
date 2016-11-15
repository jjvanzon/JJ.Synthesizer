using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class LowShelfFilter_OperatorDto : LowShelfFilter_OperatorDto_AllVars
    { }

    internal class LowShelfFilter_OperatorDto_AllVars : OperatorDtoBase_ShelfFilter_AllVars
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.LowShelfFilter);
    }

    internal class LowShelfFilter_OperatorDto_ManyConsts : OperatorDtoBase_ShelfFilter_ManyConsts
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.LowShelfFilter);
    }
}
