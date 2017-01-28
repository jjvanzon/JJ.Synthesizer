using System;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_BandPassFilter_ConstCenterFrequency_ConstBandWidth : OperatorDtoBase_Filter_ManyConsts_WithBandWidth
    {
        public override double Frequency => CenterFrequency;

        public double CenterFrequency { get; set; }
        public double BandWidth { get; set; }
    }
}