using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
// ReSharper disable MemberCanBePrivate.Global

namespace JJ.Business.Synthesizer.Dto.Operators
{
	internal class RangeOverDimension_OperatorDto
		: OperatorDtoBase_PositionReader
	{
		public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.RangeOverDimension;

		public InputDto From { get; set; }
		public InputDto Till { get; set; }
		public InputDto Step { get; set; }

		public override IReadOnlyList<InputDto> Inputs
		{
			get => new[] { From, Till, Step, Position };
			set
			{
				From = value.ElementAtOrDefault(0);
				Till = value.ElementAtOrDefault(1);
				Step = value.ElementAtOrDefault(2);
				Position = value.ElementAtOrDefault(3);
			}
		}
	}

	internal class RangeOverDimension_OperatorDto_OnlyVars : RangeOverDimension_OperatorDto
	{ }

	internal class RangeOverDimension_OperatorDto_OnlyConsts : RangeOverDimension_OperatorDto
	{ }

	/// <summary> For Machine Optimization </summary>
	internal class RangeOverDimension_OperatorDto_WithConsts_AndStepOne : RangeOverDimension_OperatorDto
	{ }
}