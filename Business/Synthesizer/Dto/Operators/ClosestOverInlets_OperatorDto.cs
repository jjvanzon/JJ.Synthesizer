using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Collections;

namespace JJ.Business.Synthesizer.Dto.Operators
{
	internal class ClosestOverInlets_OperatorDto : OperatorDtoBase, IOperatorDto_WithAggregateInfo
	{
		public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.ClosestOverInlets;

		public InputDto Input { get; set; }
		public IList<InputDto> Items { get; set; }

		/// <summary> In case of ClosestOverInlets(Exp), AggregateInfo is only about the Items, not about the Input. </summary>
		public AggregateInfo GetAggregateInfo() => AggregateInfoFactory.CreateAggregateInfo(Items);

		public override IReadOnlyList<InputDto> Inputs
		{
			get => Input.Union(Items).ToArray();
			set
			{
				Input = value.FirstOrDefault();
				Items = value.Skip(1).ToArray();
			}
		}
	}
}