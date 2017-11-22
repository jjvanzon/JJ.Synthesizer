using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Dto
{
	internal class Add_OperatorDto : OperatorDtoBase_InputsOnly, IOperatorDto_WithAggregateInfo
	{
		public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Add;

		public AggregateInfo GetAggregateInfo() => AggregateInfoFactory.CreateAggregateInfo(Inputs);
	}
}
