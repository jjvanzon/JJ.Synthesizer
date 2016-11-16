using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Hold_OperatorDto : OperatorDtoBase_VarSignal
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Hold);
    }
}