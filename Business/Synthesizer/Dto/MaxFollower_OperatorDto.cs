using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class MaxFollower_OperatorDto : OperatorDto_AggregateFollower
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MaxFollower);

        public MaxFollower_OperatorDto(
            OperatorDto signalOperatorDto, 
            OperatorDto sliceLengthOperatorDto, 
            OperatorDto sampleCountOperatorDto)
            : base(signalOperatorDto, sliceLengthOperatorDto, sampleCountOperatorDto)
        { }
    }
}