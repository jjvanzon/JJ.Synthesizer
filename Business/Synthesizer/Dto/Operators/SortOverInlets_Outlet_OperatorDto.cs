using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto.Operators
{
	internal class SortOverInlets_Outlet_OperatorDto : OperatorDtoBase_PositionReader
	{
		public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SortOverInlets;

		public override IReadOnlyList<InputDto> Inputs
		{
			get => new[] { Position };
			set => Position = value.FirstOrDefault();
		}
	}
}