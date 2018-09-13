using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Dto.Operators
{
	internal abstract class OperatorDtoBase_WithNumber : OperatorDtoBase, IOperatorDto_WithNumber
    {
		public InputDto Number { get; set; }

		public override IReadOnlyList<InputDto> Inputs
		{
			get => new[] { Number };
			set => Number = value.ElementAtOrDefault(0);
		}
	}
}
