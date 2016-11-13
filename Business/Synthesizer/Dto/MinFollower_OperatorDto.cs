using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class MinFollower_OperatorDto : OperatorDto_AggregateFollower
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MinFollower);

        public MinFollower_OperatorDto(
            OperatorDto signalOperatorDto, 
            OperatorDto sliceLengthOperatorDto, 
            OperatorDto sampleCountOperatorDto)
            : base(signalOperatorDto, sliceLengthOperatorDto, sampleCountOperatorDto)
        { }
    }
}