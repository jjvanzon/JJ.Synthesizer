using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
	internal class And_OperatorDto : OperatorDtoBase_WithAAndB
	{
		public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.And;
	}
}