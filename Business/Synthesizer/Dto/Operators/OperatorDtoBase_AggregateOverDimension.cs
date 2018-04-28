using System.Collections.Generic;
using System.Linq;
// ReSharper disable MemberCanBePrivate.Global

namespace JJ.Business.Synthesizer.Dto.Operators
{
	internal abstract class OperatorDtoBase_AggregateOverDimension
		: OperatorDtoBase_WithCollectionRecalculation, IOperatorDto_WithSignal_WithDimension
	{
		public InputDto Signal { get; set; }
		public InputDto From { get; set; }
		public InputDto Till { get; set; }
		public InputDto Step { get; set; }

		public override IReadOnlyList<InputDto> Inputs
		{
			get => new[] { Signal, From, Till, Step, Position };
			set
			{
				Signal = value.ElementAtOrDefault(0);
				From = value.ElementAtOrDefault(1);
				Till = value.ElementAtOrDefault(2);
				Step = value.ElementAtOrDefault(3);
				Position = value.ElementAtOrDefault(4);
			}
		}
	}
}