using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class PulseTrigger_OperatorDto : OperatorDtoBase_Trigger
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.PulseTrigger;
    }

    internal class PulseTrigger_OperatorDto_VarPassThrough_VarReset : PulseTrigger_OperatorDto
    { }

    internal class PulseTrigger_OperatorDto_VarPassThrough_ConstReset : PulseTrigger_OperatorDto
    { }

    internal class PulseTrigger_OperatorDto_ConstPassThrough_VarReset : PulseTrigger_OperatorDto
    { }

    internal class PulseTrigger_OperatorDto_ConstPassThrough_ConstReset : PulseTrigger_OperatorDto
    { }
}