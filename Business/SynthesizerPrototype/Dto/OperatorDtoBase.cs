using System.Collections.Generic;
using System.Diagnostics;
using JJ.Business.SynthesizerPrototype.Helpers;

namespace JJ.Business.SynthesizerPrototype.Dto
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
	public abstract class OperatorDtoBase : IOperatorDto
	{
		public int DimensionStackLevel { get; set; }
		public abstract string OperatorTypeName { get; }
		public abstract IList<IOperatorDto> InputOperatorDtos { get; set; } 

		private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
	}
}
