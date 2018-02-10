using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
// ReSharper disable SuggestVarOrType_Elsewhere

namespace JJ.Business.Synthesizer.Dto.Operators
{
	internal class LowPassFilter_OperatorDto : LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar
	{ }

	internal class LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar : OperatorDtoBase_Filter_VarSound
	{
		public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.LowPassFilter;

		public InputDto MaxFrequency { get; set; }
		public InputDto BlobVolume { get; set; }

		public override IReadOnlyList<InputDto> Inputs
		{
			get => new[] { Sound, MaxFrequency, BlobVolume };
			set
			{
				var array = value.ToArray();
				Sound = array[0];
				MaxFrequency = array[1];
				BlobVolume = array[2];
			}
		}
	}

	internal class LowPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst : OperatorDtoBase_Filter_SoundVarOrConst_OtherInputsConst_WithWidthOrBlobVolume
	{
		public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.LowPassFilter;

		public override InputDto Frequency => MaxFrequency;
		public override InputDto WidthOrBlobVolume => BlobVolume;

		public InputDto MaxFrequency { get; set; }
		public InputDto BlobVolume { get; set; }

		public override IReadOnlyList<InputDto> Inputs
		{
			get => new[] { Sound, MaxFrequency, BlobVolume };
			set
			{
				var array = value.ToArray();
				Sound = array[0];
				MaxFrequency = array[1];
				BlobVolume = array[2];
			}
		}
	}
}