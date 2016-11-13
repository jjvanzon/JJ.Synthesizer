using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class PulseTrigger_OperatorDto : OperatorDtoBase_Trigger
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.PulseTrigger);

        public PulseTrigger_OperatorDto(
           OperatorDtoBase passThroughInputOperatorDto,
           OperatorDtoBase resetOperatorDto)
           : base(passThroughInputOperatorDto, resetOperatorDto)
        { }
    }
}