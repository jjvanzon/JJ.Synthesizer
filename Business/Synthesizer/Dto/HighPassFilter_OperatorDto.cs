using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class HighPassFilter_OperatorDto : OperatorDto
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.HighPassFilter);

        public OperatorDto SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDto MinFrequencyOperatorDto => InputOperatorDtos[1];
        public OperatorDto BandWidthOperatorDto => InputOperatorDtos[2];

        public HighPassFilter_OperatorDto(
            OperatorDto signalOperatorDto,
            OperatorDto minFrequencyOperatorDto,
            OperatorDto bandWidthOperatorDto)
            : base(new OperatorDto[] { signalOperatorDto, minFrequencyOperatorDto, bandWidthOperatorDto })
        { }
    }
}