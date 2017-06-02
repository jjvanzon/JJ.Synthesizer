using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class BandPassFilterConstantPeakGain_OperatorDto : OperatorDtoBase_BandPassFilter_VarCenterFrequency_VarWidth
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.BandPassFilterConstantPeakGain;
    }
    
    internal class BandPassFilterConstantPeakGain_OperatorDto_ConstSound : OperatorDtoBase_ConstSound
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.BandPassFilterConstantPeakGain;
    }

    internal class BandPassFilterConstantPeakGain_OperatorDto_VarCenterFrequency_VarWidth : OperatorDtoBase_BandPassFilter_VarCenterFrequency_VarWidth
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.BandPassFilterConstantPeakGain;
    }

    internal class BandPassFilterConstantPeakGain_OperatorDto_ConstCenterFrequency_ConstWidth : OperatorDtoBase_BandPassFilter_ConstCenterFrequency_ConstWidth
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.BandPassFilterConstantPeakGain;
    }
}
