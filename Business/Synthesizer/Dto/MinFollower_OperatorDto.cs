using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class MinFollower_OperatorDto : OperatorDtoBase_AggregateFollower_AllVars
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.MinFollower;
    }

    internal class MinFollower_OperatorDto_AllVars : OperatorDtoBase_AggregateFollower_AllVars
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.MinFollower;
    }

    internal class MinFollower_OperatorDto_ConstSignal : OperatorDtoBase_WithSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.MinFollower;
    }
}