using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto.Operators
{
    internal class SumFollower_OperatorDto : OperatorDtoBase_AggregateFollower
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SumFollower;
    }

    internal class SumFollower_OperatorDto_AllVars : SumFollower_OperatorDto
    { }

    /// <summary> Slice length does not matter in this case. </summary>
    internal class SumFollower_OperatorDto_ConstSignal_VarSampleCount : SumFollower_OperatorDto
    { }

    /// <summary> Slice length does not matter in this case. </summary>
    internal class SumFollower_OperatorDto_ConstSignal_ConstSampleCount : SumFollower_OperatorDto
    { }
}