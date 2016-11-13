using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class MinFollower_OperatorDto : OperatorDtoBase_AggregateFollower
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MinFollower);

        public MinFollower_OperatorDto(
            OperatorDtoBase signalOperatorDto, 
            OperatorDtoBase sliceLengthOperatorDto, 
            OperatorDtoBase sampleCountOperatorDto)
            : base(signalOperatorDto, sliceLengthOperatorDto, sampleCountOperatorDto)
        { }
    }
}