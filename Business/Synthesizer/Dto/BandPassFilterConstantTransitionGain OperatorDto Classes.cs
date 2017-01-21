using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class BandPassFilterConstantTransitionGain_OperatorDto : OperatorDtoBase_BandPassFilter_VarCenterFrequency_VarBandWidth
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.BandPassFilterConstantTransitionGain;
    }

    internal class BandPassFilterConstantTransitionGain_OperatorDto_ConstSignal : OperatorDtoBase_ConstSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.BandPassFilterConstantPeakGain;
    }

    internal class BandPassFilterConstantTransitionGain_OperatorDto_VarCenterFrequency_VarBandWidth : OperatorDtoBase_BandPassFilter_VarCenterFrequency_VarBandWidth
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.BandPassFilterConstantTransitionGain;
    }

    internal class BandPassFilterConstantTransitionGain_OperatorDto_ConstCenterFrequency_ConstBandWidth : OperatorDtoBase_BandPassFilter_ConstCenterFrequency_ConstBandWidth
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.BandPassFilterConstantTransitionGain;
    }
}
