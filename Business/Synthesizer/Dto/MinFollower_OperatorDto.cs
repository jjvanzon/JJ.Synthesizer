using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class MinFollower_OperatorDto : OperatorDtoBase_AggregateFollower
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.MinFollower;
    }
}