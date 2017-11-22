using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
	internal class BandPassFilterConstantPeakGain_OperatorDto : OperatorDtoBase_BandPassFilter_SoundVarOrConst_OtherInputsVar
	{
		public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.BandPassFilterConstantPeakGain;
	}

	internal class BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsVar : OperatorDtoBase_BandPassFilter_SoundVarOrConst_OtherInputsVar
	{
		public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.BandPassFilterConstantPeakGain;
	}

	internal class BandPassFilterConstantPeakGain_OperatorDto_SoundVarOrConst_OtherInputsConst : OperatorDtoBase_BandPassFilter_SoundVarOrConst_OtherInputsConst
	{
		public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.BandPassFilterConstantPeakGain;
	}
}
