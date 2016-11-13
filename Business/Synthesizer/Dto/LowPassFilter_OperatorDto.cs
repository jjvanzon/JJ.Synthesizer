using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class LowPassFilter_OperatorDto : OperatorDto
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.LowPassFilter);

        public OperatorDto SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDto MaxFrequencyOperatorDto => InputOperatorDtos[1];
        public OperatorDto BandWidthOperatorDto => InputOperatorDtos[2];

        public LowPassFilter_OperatorDto(
            OperatorDto signalOperatorDto,
            OperatorDto maxFrequencyOperatorDto,
            OperatorDto bandWidthOperatorDto)
            : base(new OperatorDto[] { signalOperatorDto, maxFrequencyOperatorDto, bandWidthOperatorDto })
        { }
    }
}