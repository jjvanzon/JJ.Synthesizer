using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
	internal class Divide_OperatorDto : OperatorDtoBase_WithAAndB
	{
		public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Divide;
	}
}