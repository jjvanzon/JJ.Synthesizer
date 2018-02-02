using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto.Operators
{
	internal class VariableInput_OperatorDto : OperatorDtoBase_WithDimension
	{
		public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.VariableInput;

		public double DefaultValue { get; set; }
		public int Position { get; set; }

		public override IReadOnlyList<InputDto> Inputs
		{
			get => new InputDto[0];
			set { }
		}
	}
}
