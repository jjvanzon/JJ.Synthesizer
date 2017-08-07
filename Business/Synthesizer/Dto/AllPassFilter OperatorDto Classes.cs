using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class AllPassFilter_OperatorDto : AllPassFilter_OperatorDto_AllVars
    { }

    internal class AllPassFilter_OperatorDto_ConstSound : OperatorDtoBase_ConstSound
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.AllPassFilter;
    }

    internal class AllPassFilter_OperatorDto_AllVars : OperatorDtoBase_Filter_VarSound
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.AllPassFilter;

        public IOperatorDto CenterFrequencyOperatorDto { get; set; }
        public IOperatorDto WidthOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { SoundOperatorDto, CenterFrequencyOperatorDto, WidthOperatorDto };
            set { SoundOperatorDto = value[0]; CenterFrequencyOperatorDto = value[1]; WidthOperatorDto = value[2]; }
        }

        public override IEnumerable<InputDto> InputDtos => new[]
        {
            new InputDto(SoundOperatorDto),
            new InputDto(CenterFrequencyOperatorDto),
            new InputDto(WidthOperatorDto)
        };
    }

    internal class AllPassFilter_OperatorDto_ManyConsts : OperatorDtoBase_Filter_ManyConsts_WithWidthOrBlobVolume
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.AllPassFilter;
        public override double Frequency => CenterFrequency;
        public override double WidthOrBlobVolume => Width;

        public double CenterFrequency { get; set; }
        public double Width { get; set; }

        public override IEnumerable<InputDto> InputDtos => new[]
        {
            new InputDto(SoundOperatorDto),
            new InputDto(CenterFrequency),
            new InputDto(Width)
        };
    }
}