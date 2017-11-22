using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
	internal class Absolute_OperatorDto : OperatorDtoBase_WithNumber
	{
		public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Absolute;
	}
}