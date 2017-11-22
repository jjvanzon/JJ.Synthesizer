using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
	internal class SortOverInlets_Outlet_OperatorDto
		: OperatorDtoBase_PositionReader,
		  IOperatorDto_WithOutletPosition
	{
		public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SortOverInlets;

		public int OutletPosition { get; set; }

		public override IReadOnlyList<InputDto> Inputs
		{
			get => new[] { Position };
			set => Position = value.FirstOrDefault();
		}
	}
}