using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class AverageFollower_OperatorDto : OperatorDtoBase_AggregateFollower_AllVars
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.AverageFollower;
    }

    internal class AverageFollower_OperatorDto_AllVars : OperatorDtoBase_AggregateFollower_AllVars
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.AverageFollower;
    }

    internal class AverageFollower_OperatorDto_ConstSignal : OperatorDtoBase_ConstSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.AverageFollower;
    }
}