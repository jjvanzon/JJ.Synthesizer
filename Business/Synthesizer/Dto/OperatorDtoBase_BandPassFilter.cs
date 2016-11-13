namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_BandPassFilter : OperatorDtoBase
    {
        public OperatorDtoBase SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase CenterFrequencyOperatorDto => InputOperatorDtos[1];
        public OperatorDtoBase BandWidthOperatorDto => InputOperatorDtos[2];

        public OperatorDtoBase_BandPassFilter(
            OperatorDtoBase signalOperatorDto,
            OperatorDtoBase centerFrequencyOperatorDto,
            OperatorDtoBase bandWidthOperatorDto)
            : base(new OperatorDtoBase[] { signalOperatorDto, centerFrequencyOperatorDto, bandWidthOperatorDto })
        { }
    }
}