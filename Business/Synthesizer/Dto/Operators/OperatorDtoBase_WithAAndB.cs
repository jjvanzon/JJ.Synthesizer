using System.Collections.Generic;
using System.Linq;
// ReSharper disable SuggestVarOrType_Elsewhere

namespace JJ.Business.Synthesizer.Dto.Operators
{
	internal abstract class OperatorDtoBase_WithAAndB : OperatorDtoBase
	{
		public InputDto A { get; set; }
		public InputDto B { get; set; }

		public override IReadOnlyList<InputDto> Inputs
		{
			get => new[] { A, B };
			set
			{
                A = value.ElementAtOrDefault(0);
                B = value.ElementAtOrDefault(1);
			}
		}
	}
}
