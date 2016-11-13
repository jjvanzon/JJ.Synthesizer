using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class ChangeTrigger_OperatorDto : OperatorDto_Trigger
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.ChangeTrigger);

        public ChangeTrigger_OperatorDto(
           OperatorDto passThroughOperatorDto,
           OperatorDto resetOperatorDto)
           : base(passThroughOperatorDto, resetOperatorDto)
        { }
    }
}