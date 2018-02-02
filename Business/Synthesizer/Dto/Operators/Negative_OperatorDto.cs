using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto.Operators
{
	internal class Negative_OperatorDto : OperatorDtoBase_WithNumber
	{
		public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Negative;
	}
}