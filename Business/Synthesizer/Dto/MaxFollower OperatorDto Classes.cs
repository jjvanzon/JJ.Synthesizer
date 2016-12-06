using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class MaxFollower_OperatorDto_ConstSignal : OperatorDtoBase_ConstSignal
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MaxFollower);
    }

    internal class MaxFollower_OperatorDto_AllVars : OperatorDtoBase_AggregateFollower_AllVars
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MaxFollower);
    }
}