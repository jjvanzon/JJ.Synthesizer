using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class SumFollower_OperatorDto : OperatorDtoBase_AggregateFollower
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.SumFollower);
    }
}