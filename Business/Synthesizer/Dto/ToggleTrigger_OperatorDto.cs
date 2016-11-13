using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class ToggleTrigger_OperatorDto : OperatorDtoBase_Trigger
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.ToggleTrigger);

        public ToggleTrigger_OperatorDto(
           OperatorDtoBase passThroughInputOperatorDto,
           OperatorDtoBase resetOperatorDto)
           : base(passThroughInputOperatorDto, resetOperatorDto)
        { }
    }
}