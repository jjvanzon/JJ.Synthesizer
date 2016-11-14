using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class AllPassFilter_OperatorDto : AllPassFilter_OperatorDto_AllVars
    {
        public AllPassFilter_OperatorDto(
            OperatorDtoBase signalOperatorDto,
            OperatorDtoBase centerFrequencyOperatorDto,
            OperatorDtoBase bandWidthOperatorDto)
            : base(signalOperatorDto, centerFrequencyOperatorDto, bandWidthOperatorDto)
        { }
    }

    internal class AllPassFilter_OperatorDto_AllVars : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.AllPassFilter);

        public OperatorDtoBase SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase CenterFrequencyOperatorDto => InputOperatorDtos[1];
        public OperatorDtoBase BandWidthOperatorDto => InputOperatorDtos[2];

        public AllPassFilter_OperatorDto_AllVars(
            OperatorDtoBase signalOperatorDto,
            OperatorDtoBase centerFrequencyOperatorDto,
            OperatorDtoBase bandWidthOperatorDto)
            : base(new OperatorDtoBase[] { signalOperatorDto, centerFrequencyOperatorDto, bandWidthOperatorDto })
        { }
    }

    internal class AllPassFilter_OperatorDto_ManyConsts : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.AllPassFilter);

        public OperatorDtoBase SignalOperatorDto => InputOperatorDtos[0];
        public double CenterFrequency { get; set; }
        public double BandWidth { get; set; }

        public AllPassFilter_OperatorDto_ManyConsts(
            OperatorDtoBase signalOperatorDto,
            double centerFrequency,
            double bandWidth)
            : base(new OperatorDtoBase[] { signalOperatorDto})
        {
            CenterFrequency = centerFrequency;
            BandWidth = bandWidth;
        }
    }
}