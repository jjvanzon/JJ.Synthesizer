using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto.Operators
{
	internal class Not_OperatorDto : OperatorDtoBase_WithNumber
	{
		public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Not;
	}
}