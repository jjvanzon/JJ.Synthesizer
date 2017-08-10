using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class LowPassFilter_OperatorDto : LowPassFilter_OperatorDto_AllVars
    { }

    internal class LowPassFilter_OperatorDto_ConstSound : OperatorDtoBase_ConstSound
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.LowPassFilter;
    }

    internal class LowPassFilter_OperatorDto_AllVars : OperatorDtoBase_Filter_VarSound
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.LowPassFilter;

        public InputDto MaxFrequency { get; set; }
        public InputDto BlobVolume { get; set; }

        public override IEnumerable<InputDto> Inputs
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

    internal class LowPassFilter_OperatorDto_ManyConsts : OperatorDtoBase_Filter_ManyConsts_WithWidthOrBlobVolume
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.LowPassFilter;

        public override InputDto Frequency => MaxFrequency;
        public override InputDto WidthOrBlobVolume => BlobVolume;

        public InputDto MaxFrequency { get; set; }
        public InputDto BlobVolume { get; set; }

        public override IEnumerable<InputDto> Inputs
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