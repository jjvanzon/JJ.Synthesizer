using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class HighPassFilter_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.HighPassFilter);

        public OperatorDtoBase SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase MinFrequencyOperatorDto => InputOperatorDtos[1];
        public OperatorDtoBase BandWidthOperatorDto => InputOperatorDtos[2];

        public HighPassFilter_OperatorDto(
            OperatorDtoBase signalOperatorDto,
            OperatorDtoBase minFrequencyOperatorDto,
            OperatorDtoBase bandWidthOperatorDto)
            : base(new OperatorDtoBase[] { signalOperatorDto, minFrequencyOperatorDto, bandWidthOperatorDto })
        { }
    }
}