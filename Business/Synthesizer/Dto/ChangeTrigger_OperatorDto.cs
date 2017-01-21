using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class ChangeTrigger_OperatorDto : OperatorDtoBase_Trigger_VarPassThrough_VarReset
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.ChangeTrigger;
    }

    internal class ChangeTrigger_OperatorDto_VarPassThrough_VarReset : OperatorDtoBase_Trigger_VarPassThrough_VarReset
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.ChangeTrigger;
    }

    internal class ChangeTrigger_OperatorDto_VarPassThrough_ConstReset : OperatorDtoBase_Trigger_VarPassThrough_ConstReset
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.ChangeTrigger;
    }

    internal class ChangeTrigger_OperatorDto_ConstPassThrough_VarReset : OperatorDtoBase_Trigger_ConstPassThrough_VarReset
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.ChangeTrigger;
    }

    internal class ChangeTrigger_OperatorDto_ConstPassThrough_ConstReset : OperatorDtoBase_Trigger_ConstPassThrough_ConstReset
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.ChangeTrigger;
    }
}