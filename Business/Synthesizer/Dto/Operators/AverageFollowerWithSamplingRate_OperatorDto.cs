using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto.Operators
{
	internal class AverageFollowerWithSamplingRate_OperatorDto : OperatorDtoBase_AggregateFollowerWithSamplingRate
	{
		public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.AverageFollowerWithSamplingRate;
	}
}