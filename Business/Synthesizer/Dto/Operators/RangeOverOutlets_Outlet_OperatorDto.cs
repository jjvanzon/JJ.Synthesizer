using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
// ReSharper disable SuggestVarOrType_Elsewhere
// ReSharper disable MemberCanBePrivate.Global

namespace JJ.Business.Synthesizer.Dto.Operators
{
	internal class RangeOverOutlets_Outlet_OperatorDto : OperatorDtoBase
	{
		public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.RangeOverOutlets;

		public InputDto From { get; set; }
		public InputDto Step { get; set; }
		public int OutletPosition { get; set; }

		public override IReadOnlyList<InputDto> Inputs
		{
			get => new[] { From, Step };
			set
			{
				var array = value.ToArray();
				From = array[0];
				Step = array[1];
			}
		}
	}
}