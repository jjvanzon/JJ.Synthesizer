using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
	internal class Hold_OperatorDto : OperatorDtoBase_WithSignal
	{
		public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Hold;
	}
}