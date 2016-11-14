namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_BandPassFilter_VarCenterFrequency_VarBandWidth : OperatorDtoBase
    {
        public OperatorDtoBase SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase CenterFrequencyOperatorDto => InputOperatorDtos[1];
        public OperatorDtoBase BandWidthOperatorDto => InputOperatorDtos[2];

        public OperatorDtoBase_BandPassFilter_VarCenterFrequency_VarBandWidth(
            OperatorDtoBase signalOperatorDto,
            OperatorDtoBase centerFrequencyOperatorDto,
            OperatorDtoBase bandWidthOperatorDto)
            : base(new OperatorDtoBase[] { signalOperatorDto, centerFrequencyOperatorDto, bandWidthOperatorDto })
        { }
    }
}