using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class ChangeTrigger_OperatorDto : OperatorDtoBase_Trigger
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.ChangeTrigger);

        public ChangeTrigger_OperatorDto(
           OperatorDtoBase passThroughInputOperatorDto,
           OperatorDtoBase resetOperatorDto)
           : base(passThroughInputOperatorDto, resetOperatorDto)
        { }
    }
}