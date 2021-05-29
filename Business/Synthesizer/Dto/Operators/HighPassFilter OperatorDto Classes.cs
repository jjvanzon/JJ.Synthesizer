using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
// ReSharper disable SuggestVarOrType_Elsewhere
// ReSharper disable MemberCanBePrivate.Global

namespace JJ.Business.Synthesizer.Dto.Operators
{
    internal class HighPassFilter_OperatorDto : HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar
    { }

    internal class HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsVar : OperatorDtoBase_Filter_VarSound
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.HighPassFilter;

        public InputDto MinFrequency { get; set; }
        public InputDto BlobVolume { get; set; }

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new[] { Sound, MinFrequency, BlobVolume };
            set
            {
                var array = value.ToArray();
                Sound = array[0];
                MinFrequency = array[1];
                BlobVolume = array[2];
            }
        }
    }

    internal class HighPassFilter_OperatorDto_SoundVarOrConst_OtherInputsConst : OperatorDtoBase_Filter_SoundVarOrConst_OtherInputsConst_WithWidthOrBlobVolume
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.HighPassFilter;

        public override InputDto Frequency => MinFrequency;
        public override InputDto WidthOrBlobVolume => BlobVolume;

        public InputDto MinFrequency { get; set; }
        public InputDto BlobVolume { get; set; }

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new[] { Sound, MinFrequency, BlobVolume };
            set
            {
                var array = value.ToArray();
                Sound = array[0];
                MinFrequency = array[1];
                BlobVolume = array[2];
            }
        }
    }
}