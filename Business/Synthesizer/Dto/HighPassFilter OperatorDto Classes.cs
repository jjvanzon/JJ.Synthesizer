using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class HighPassFilter_OperatorDto : HighPassFilter_OperatorDto_AllVars
    { }

    internal class HighPassFilter_OperatorDto_ConstSound : OperatorDtoBase_ConstSound
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.HighPassFilter;
    }

    internal class HighPassFilter_OperatorDto_AllVars : OperatorDtoBase_Filter_VarSound
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.HighPassFilter;

        public InputDto MinFrequency { get; set; }
        public InputDto BlobVolume { get; set; }

        public override IEnumerable<InputDto> Inputs
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

    internal class HighPassFilter_OperatorDto_ManyConsts : OperatorDtoBase_Filter_ManyConsts_WithWidthOrBlobVolume
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.HighPassFilter;

        public override InputDto Frequency => MinFrequency;
        public override InputDto WidthOrBlobVolume => BlobVolume;

        public InputDto MinFrequency { get; set; }
        public InputDto BlobVolume { get; set; }

        public override IEnumerable<InputDto> Inputs
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