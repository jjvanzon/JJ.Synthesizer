using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class AverageFollower_OperatorDto : OperatorDto_AggregateFollower
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.AverageFollower);

        public AverageFollower_OperatorDto(
            OperatorDto signalOperatorDto, 
            OperatorDto sliceLengthOperatorDto, 
            OperatorDto sampleCountOperatorDto)
            : base(signalOperatorDto, sliceLengthOperatorDto, sampleCountOperatorDto)
        { }
    }
}