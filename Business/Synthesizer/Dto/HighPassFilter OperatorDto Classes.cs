using System.Collections.Generic;
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

        public IOperatorDto MinFrequencyOperatorDto { get; set; }
        public IOperatorDto BlobVolumeOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { SoundOperatorDto, MinFrequencyOperatorDto, BlobVolumeOperatorDto };
            set { SoundOperatorDto = value[0]; MinFrequencyOperatorDto = value[1]; BlobVolumeOperatorDto = value[2]; }
        }

        public override IEnumerable<InputDto> InputDtos => new[]
        {
            new InputDto(SoundOperatorDto),
            new InputDto(MinFrequencyOperatorDto),
            new InputDto(BlobVolumeOperatorDto)
        };
    }

    internal class HighPassFilter_OperatorDto_ManyConsts : OperatorDtoBase_Filter_ManyConsts_WithWidthOrBlobVolume
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.HighPassFilter;
        public override double Frequency => MinFrequency;
        public override double WidthOrBlobVolume => BlobVolume;

        public double MinFrequency { get; set; }
        public double BlobVolume { get; set; }

        public override IEnumerable<InputDto> InputDtos => new[]
        {
            new InputDto(SoundOperatorDto),
            new InputDto(MinFrequency),
            new InputDto(BlobVolume)
        };
    }
}