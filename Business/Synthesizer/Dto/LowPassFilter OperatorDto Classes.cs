using System.Collections.Generic;
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

        public IOperatorDto MaxFrequencyOperatorDto { get; set; }
        public IOperatorDto BlobVolumeOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { SoundOperatorDto, MaxFrequencyOperatorDto, BlobVolumeOperatorDto };
            set { SoundOperatorDto = value[0]; MaxFrequencyOperatorDto = value[1]; BlobVolumeOperatorDto = value[2]; }
        }

        public override IEnumerable<InputDto> InputDtos => new[]
        {
            new InputDto(SoundOperatorDto),
            new InputDto(MaxFrequencyOperatorDto),
            new InputDto(BlobVolumeOperatorDto),
        };
    }

    internal class LowPassFilter_OperatorDto_ManyConsts : OperatorDtoBase_Filter_ManyConsts_WithWidthOrBlobVolume
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.LowPassFilter;
        public override double Frequency => MaxFrequency;
        public override double WidthOrBlobVolume => BlobVolume;

        public double MaxFrequency { get; set; }
        public double BlobVolume { get; set; }

        public override IEnumerable<InputDto> InputDtos => new[]
        {
            new InputDto(SoundOperatorDto),
            new InputDto(MaxFrequency),
            new InputDto(BlobVolume),
        };
    }
}