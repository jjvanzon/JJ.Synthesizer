using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto.Operators
{
	internal class Interpolate_OperatorDto
		: OperatorDtoBase_PositionReader, IOperatorDto_WithSignal_WithDimension, IOperatorDto_WithInterpolation
	{
		public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Interpolate;

		public InputDto Signal { get; set; }
		public InputDto SamplingRate { get; set; }
		public InterpolationTypeEnum InterpolationTypeEnum { get; set; }

		public override IReadOnlyList<InputDto> Inputs
		{
			get => new[] { Signal, SamplingRate, Position };
			set
			{
				Signal = value.ElementAtOrDefault(0);
				SamplingRate = value.ElementAtOrDefault(1);
				Position = value.ElementAtOrDefault(2);
			}
		}
	}

	internal class Interpolate_OperatorDto_ConstSignal : Interpolate_OperatorDto
	{ }

	internal class Interpolate_OperatorDto_Block : Interpolate_OperatorDto
	{ }

	internal class Interpolate_OperatorDto_Cubic_LagBehind : Interpolate_OperatorDto
	{ }

	internal class Interpolate_OperatorDto_Hermite_LagBehind : Interpolate_OperatorDto
	{ }

	internal class Interpolate_OperatorDto_Line_LagBehind : Interpolate_OperatorDto
	{ }

	internal class Interpolate_OperatorDto_Stripe_LagBehind : Interpolate_OperatorDto
	{ }
}