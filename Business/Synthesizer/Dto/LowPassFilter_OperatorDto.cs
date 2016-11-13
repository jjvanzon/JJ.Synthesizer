using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class LowPassFilter_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.LowPassFilter);

        public OperatorDtoBase SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase MaxFrequencyOperatorDto => InputOperatorDtos[1];
        public OperatorDtoBase BandWidthOperatorDto => InputOperatorDtos[2];

        public LowPassFilter_OperatorDto(
            OperatorDtoBase signalOperatorDto,
            OperatorDtoBase maxFrequencyOperatorDto,
            OperatorDtoBase bandWidthOperatorDto)
            : base(new OperatorDtoBase[] { signalOperatorDto, maxFrequencyOperatorDto, bandWidthOperatorDto })
        { }
    }
}