using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto.Operators
{
    internal interface IOperatorDto_WithInterpolation_AndFollowingMode : IOperatorDto_WithInterpolation
    {
        FollowingModeEnum FollowingModeEnum { get; set; }
    }
}