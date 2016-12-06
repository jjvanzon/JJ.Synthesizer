using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class MinFollower_OperatorDto : OperatorDtoBase_AggregateFollower_AllVars
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MinFollower);
    }

    internal class MinFollower_OperatorDto_AllVars : OperatorDtoBase_AggregateFollower_AllVars
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MinFollower);
    }

    internal class MinFollower_OperatorDto_ConstSignal : OperatorDtoBase_ConstSignal
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MinFollower);
    }
}