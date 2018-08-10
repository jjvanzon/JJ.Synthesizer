using System.Collections.Generic;
using System.Linq;
// ReSharper disable MemberCanBePrivate.Global

namespace JJ.Business.Synthesizer.Dto.Operators
{
	internal abstract class OperatorDtoBase_AggregateFollowerWithSamplingRate 
		: OperatorDtoBase_PositionReader, IOperatorDto_WithSignal_WithDimension
	{
		public InputDto Signal { get; set; }
		public InputDto SliceLength { get; set; }
		public InputDto SamplingRate { get; set; }

		public override IReadOnlyList<InputDto> Inputs
		{
			get => new[] { Signal, SliceLength, SamplingRate, Position };
			set
			{
				Signal = value.ElementAtOrDefault(0);
				SliceLength = value.ElementAtOrDefault(1);
				SamplingRate = value.ElementAtOrDefault(2);
				Position = value.ElementAtOrDefault(3);
			}
		}
	}
}