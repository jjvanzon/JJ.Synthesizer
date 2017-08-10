using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class ToggleTrigger_OperatorDto : OperatorDtoBase_Trigger
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.ToggleTrigger;
    }

    internal class ToggleTrigger_OperatorDto_VarPassThrough_VarReset : ToggleTrigger_OperatorDto
    { }

    internal class ToggleTrigger_OperatorDto_VarPassThrough_ConstReset : ToggleTrigger_OperatorDto
    { }

    internal class ToggleTrigger_OperatorDto_ConstPassThrough_VarReset : ToggleTrigger_OperatorDto
    { }

    internal class ToggleTrigger_OperatorDto_ConstPassThrough_ConstReset : ToggleTrigger_OperatorDto
    { }
}