using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class ToggleTrigger_OperatorDto : OperatorDto_Trigger
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.ToggleTrigger);

        public ToggleTrigger_OperatorDto(
           OperatorDto passThroughOperatorDto,
           OperatorDto resetOperatorDto)
           : base(passThroughOperatorDto, resetOperatorDto)
        { }
    }
}