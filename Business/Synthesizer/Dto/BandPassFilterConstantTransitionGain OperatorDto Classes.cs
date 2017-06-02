using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class BandPassFilterConstantTransitionGain_OperatorDto : OperatorDtoBase_BandPassFilter_VarCenterFrequency_VarWidth
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.BandPassFilterConstantTransitionGain;
    }

    internal class BandPassFilterConstantTransitionGain_OperatorDto_ConstSound : OperatorDtoBase_ConstSound
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.BandPassFilterConstantPeakGain;
    }

    internal class BandPassFilterConstantTransitionGain_OperatorDto_VarCenterFrequency_VarWidth : OperatorDtoBase_BandPassFilter_VarCenterFrequency_VarWidth
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.BandPassFilterConstantTransitionGain;
    }

    internal class BandPassFilterConstantTransitionGain_OperatorDto_ConstCenterFrequency_ConstWidth : OperatorDtoBase_BandPassFilter_ConstCenterFrequency_ConstWidth
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.BandPassFilterConstantTransitionGain;
    }
}
