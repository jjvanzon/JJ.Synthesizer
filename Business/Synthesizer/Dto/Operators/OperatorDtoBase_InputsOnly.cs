using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto.Operators
{
	internal abstract class OperatorDtoBase_InputsOnly : OperatorDtoBase
	{
		public override IReadOnlyList<InputDto> Inputs { get; set; }
	}
}
