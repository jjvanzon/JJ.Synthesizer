using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
// ReSharper disable SuggestVarOrType_Elsewhere

namespace JJ.Business.Synthesizer.Dto.Operators
{
	internal class Power_OperatorDto : OperatorDtoBase
	{
		public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Power;

		public InputDto Base { get; set; }
		public InputDto Exponent { get; set; }

		public override IReadOnlyList<InputDto> Inputs
		{
			get => new[] { Base, Exponent };
			set
			{
				var array = value.ToArray();
				Base = array[0];
				Exponent = array[1];
			}
		}
	}
}
