using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class BandPassFilterConstantTransitionGain_OperatorDto : OperatorDtoBase_BandPassFilter_VarCenterFrequency_VarBandWidth
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.BandPassFilterConstantTransitionGain);
    }

    internal class BandPassFilterConstantTransitionGain_OperatorDto_VarCenterFrequency_VarBandWidth : OperatorDtoBase_BandPassFilter_VarCenterFrequency_VarBandWidth
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.BandPassFilterConstantTransitionGain);
    }

    internal class BandPassFilterConstantTransitionGain_OperatorDto_ConstCenterFrequency_ConstBandWidth : OperatorDtoBase_BandPassFilter_ConstCenterFrequency_ConstBandWidth
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.BandPassFilterConstantTransitionGain);
    }
}
