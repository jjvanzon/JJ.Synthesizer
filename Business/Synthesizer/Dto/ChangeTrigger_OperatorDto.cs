using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class ChangeTrigger_OperatorDto : OperatorDtoBase_Trigger
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.ChangeTrigger);
    }
}