using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class AverageFollower_OperatorDto : OperatorDtoBase_AggregateFollower_SignalVarOrConst_OtherInputsVar
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.AverageFollower;
    }

    internal class AverageFollower_OperatorDto_SignalVarOrConst_OtherInputsVar : OperatorDtoBase_AggregateFollower_SignalVarOrConst_OtherInputsVar
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.AverageFollower;
    }

    internal class AverageFollower_OperatorDto_ConstSignal : OperatorDtoBase_WithSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.AverageFollower;
    }
}