using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class BandPassFilterConstantPeakGain_OperatorDto : OperatorDtoBase_BandPassFilter_VarCenterFrequency_VarBandWidth
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.BandPassFilterConstantPeakGain);
    }
    
    internal class BandPassFilterConstantPeakGain_OperatorDto_ConstSignal : OperatorDtoBase_ConstSignal
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.BandPassFilterConstantPeakGain);
    }

    internal class BandPassFilterConstantPeakGain_OperatorDto_VarCenterFrequency_VarBandWidth : OperatorDtoBase_BandPassFilter_VarCenterFrequency_VarBandWidth
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.BandPassFilterConstantPeakGain);
    }

    internal class BandPassFilterConstantPeakGain_OperatorDto_ConstCenterFrequency_ConstBandWidth : OperatorDtoBase_BandPassFilter_ConstCenterFrequency_ConstBandWidth
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.BandPassFilterConstantPeakGain);
    }
}
