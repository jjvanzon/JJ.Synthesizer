using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class NotchFilter_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.NotchFilter);

        public OperatorDtoBase SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase CenterFrequencyOperatorDto => InputOperatorDtos[1];
        public OperatorDtoBase BandWidthOperatorDto => InputOperatorDtos[2];

        public NotchFilter_OperatorDto(
            OperatorDtoBase signalOperatorDto,
            OperatorDtoBase centerFrequencyOperatorDto,
            OperatorDtoBase bandWidthOperatorDto)
            : base(new OperatorDtoBase[] { signalOperatorDto, centerFrequencyOperatorDto, bandWidthOperatorDto })
        { }
    }
}