using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class LowShelfFilter_OperatorDto : OperatorDtoBase_ShelfFilter
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.LowShelfFilter);
    }
}
