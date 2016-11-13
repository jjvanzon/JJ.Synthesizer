using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class PulseTrigger_OperatorDto : OperatorDto_Trigger
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.PulseTrigger);

        public PulseTrigger_OperatorDto(
           OperatorDto passThroughOperatorDto,
           OperatorDto resetOperatorDto)
           : base(passThroughOperatorDto, resetOperatorDto)
        { }
    }
}