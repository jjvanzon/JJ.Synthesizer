using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class AverageFollower_OperatorDto : OperatorDtoBase_AggregateFollower
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.AverageFollower);

        public AverageFollower_OperatorDto(
            OperatorDtoBase signalOperatorDto, 
            OperatorDtoBase sliceLengthOperatorDto, 
            OperatorDtoBase sampleCountOperatorDto)
            : base(signalOperatorDto, sliceLengthOperatorDto, sampleCountOperatorDto)
        { }
    }
}