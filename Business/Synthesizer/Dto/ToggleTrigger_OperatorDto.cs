using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class ToggleTrigger_OperatorDto : OperatorDtoBase_Trigger_VarPassThrough_VarReset
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.ToggleTrigger);
    }

    internal class ToggleTrigger_OperatorDto_VarPassThrough_VarReset : OperatorDtoBase_Trigger_VarPassThrough_VarReset
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.ToggleTrigger);
    }

    internal class ToggleTrigger_OperatorDto_VarPassThrough_ConstReset : OperatorDtoBase_Trigger_VarPassThrough_ConstReset
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.ToggleTrigger);
    }

    internal class ToggleTrigger_OperatorDto_ConstPassThrough_VarReset : OperatorDtoBase_Trigger_ConstPassThrough_VarReset
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.ToggleTrigger);
    }

    internal class ToggleTrigger_OperatorDto_ConstPassThrough_ConstReset : OperatorDtoBase_Trigger_ConstPassThrough_ConstReset
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.ToggleTrigger);
    }
}