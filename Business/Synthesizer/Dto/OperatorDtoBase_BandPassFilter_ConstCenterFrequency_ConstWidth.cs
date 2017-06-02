namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_BandPassFilter_ConstCenterFrequency_ConstWidth : OperatorDtoBase_Filter_ManyConsts_WithWidthOrBlobVolume
    {
        public override double Frequency => CenterFrequency;
        public override double WidthOrBlobVolume => Width;

        public double CenterFrequency { get; set; }
        public double Width { get; set; }
    }
}