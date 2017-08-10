using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class ChangeTrigger_OperatorDto : OperatorDtoBase_Trigger
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.ChangeTrigger;
    }

    internal class ChangeTrigger_OperatorDto_VarPassThrough_VarReset : ChangeTrigger_OperatorDto
    { }

    internal class ChangeTrigger_OperatorDto_VarPassThrough_ConstReset : ChangeTrigger_OperatorDto
    { }

    internal class ChangeTrigger_OperatorDto_ConstPassThrough_VarReset : ChangeTrigger_OperatorDto
    { }

    internal class ChangeTrigger_OperatorDto_ConstPassThrough_ConstReset : ChangeTrigger_OperatorDto
    { }
}