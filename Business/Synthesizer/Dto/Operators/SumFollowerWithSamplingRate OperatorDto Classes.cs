using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto.Operators
{
	internal class SumFollowerWithSamplingRate_OperatorDto : OperatorDtoBase_AggregateFollowerWithSamplingRate
	{
		public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SumFollowerWithSamplingRate;
	}

	internal class SumFollowerWithSamplingRate_OperatorDto_AllVars : SumFollowerWithSamplingRate_OperatorDto
	{ }

	/// <summary> Slice length does not matter in this case. </summary>
	internal class SumFollowerWithSamplingRate_OperatorDto_ConstSignal_VarSamplingRate : SumFollowerWithSamplingRate_OperatorDto
	{ }

	/// <summary> Slice length does not matter in this case. </summary>
	internal class SumFollowerWithSamplingRate_OperatorDto_AllConsts : SumFollowerWithSamplingRate_OperatorDto
	{ }
}