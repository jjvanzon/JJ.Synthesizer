using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
	internal class GetPosition_OperatorDto : OperatorDtoBase_PositionReader
	{
		public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.GetPosition;

		public int SamplingRate { get; set; }

		public override IReadOnlyList<InputDto> Inputs
		{
			get => new[] { Position };
			set => Position = value.FirstOrDefault();
		}
	}
}