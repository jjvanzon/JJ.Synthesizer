namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_BandPassFilter_ConstCenterFrequency_ConstBandWidth : OperatorDtoBase
    {
        public OperatorDtoBase SignalOperatorDto => InputOperatorDtos[0];
        public double CenterFrequency { get; set; }
        public double BandWidth { get; set; }

        public OperatorDtoBase_BandPassFilter_ConstCenterFrequency_ConstBandWidth(
            OperatorDtoBase signalOperatorDto,
            double centerFrequency,
            double bandWidth)
            : base(new OperatorDtoBase[] { signalOperatorDto })
        {
            CenterFrequency = centerFrequency;
            BandWidth = bandWidth;
        }
    }
}