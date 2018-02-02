using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Dto.Operators
{
	internal abstract class OperatorDtoBase_BandPassFilter_SoundVarOrConst_OtherInputsConst : OperatorDtoBase_Filter_SoundVarOrConst_OtherInputsConst_WithWidthOrBlobVolume
	{
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