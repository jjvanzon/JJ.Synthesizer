using System.Collections.Generic;
using System.Linq;

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
				var array = value.ToArray();
				A = array[0];
				B = array[1];
			}
		}
	}
}
