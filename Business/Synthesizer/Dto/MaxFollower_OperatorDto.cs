using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class MaxFollower_OperatorDto : OperatorDtoBase_AggregateFollower
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MaxFollower);

        public MaxFollower_OperatorDto(
            OperatorDtoBase signalOperatorDto, 
            OperatorDtoBase sliceLengthOperatorDto, 
            OperatorDtoBase sampleCountOperatorDto)
            : base(signalOperatorDto, sliceLengthOperatorDto, sampleCountOperatorDto)
        { }
    }
}