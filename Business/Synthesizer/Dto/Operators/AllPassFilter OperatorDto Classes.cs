using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
// ReSharper disable SuggestVarOrType_Elsewhere

namespace JJ.Business.Synthesizer.Dto.Operators
{
	internal class AllPassFilter_OperatorDto : AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar
	{ }

	internal class AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar : OperatorDtoBase_Filter_VarSound
	{
		public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.AllPassFilter;

		public InputDto CenterFrequency { get; set; }
		public InputDto Width { get; set; }

		public override IReadOnlyList<InputDto> Inputs
		{
			get => new[] { Sound, CenterFrequency, Width };
			set
			{
				var array = value.ToArray();
				Sound = array[0];
				CenterFrequency = array[1];
				Width = array[2];
			}
		}
	}

	internal class AllPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst : OperatorDtoBase_Filter_SoundVarOrConst_OtherInputsConst_WithWidthOrBlobVolume
	{
		public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.AllPassFilter;
		public override InputDto Frequency => CenterFrequency;
		public override InputDto WidthOrBlobVolume => Width;

		public InputDto CenterFrequency { get; set; }
		public InputDto Width { get; set; }

		public override IReadOnlyList<InputDto> Inputs
		{
			get => new[] { Sound, CenterFrequency, Width };
			set
			{
				var array = value.ToArray();
				Sound = array[0];
				CenterFrequency = array[1];
				Width = array[2];
			}
		}
	}
}