using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class SumFollower_OperatorDto : OperatorDtoBase_AggregateFollower
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.SumFollower);

        public SumFollower_OperatorDto(
            OperatorDtoBase signalOperatorDto, 
            OperatorDtoBase sliceLengthOperatorDto, 
            OperatorDtoBase sampleCountOperatorDto)
            : base(signalOperatorDto, sliceLengthOperatorDto, sampleCountOperatorDto)
        { }
    }
}