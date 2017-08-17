using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class AverageFollower_OperatorDto : OperatorDtoBase_AggregateFollower
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.AverageFollower;
    }
}