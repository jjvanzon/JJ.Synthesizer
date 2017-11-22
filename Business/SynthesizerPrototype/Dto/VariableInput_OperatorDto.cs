using JJ.Business.SynthesizerPrototype.Helpers;

namespace JJ.Business.SynthesizerPrototype.Dto
{
	public class VariableInput_OperatorDto : OperatorDtoBase_WithoutInputOperatorDtos
	{
		public double DefaultValue { get; set; }
		public override string OperatorTypeName => nameof(OperatorTypeEnum.VariableInput);
	}
}
