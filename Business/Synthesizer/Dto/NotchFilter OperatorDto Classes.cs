using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class NotchFilter_OperatorDto : NotchFilter_OperatorDto_AllVars
    { }

    internal class NotchFilter_OperatorDto_ConstSound : OperatorDtoBase_ConstSound
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.NotchFilter;
    }

    internal class NotchFilter_OperatorDto_AllVars : OperatorDtoBase_Filter_VarSound
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.NotchFilter;

        public IOperatorDto CenterFrequencyOperatorDto { get; set; }
        public IOperatorDto WidthOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { SoundOperatorDto, CenterFrequencyOperatorDto, WidthOperatorDto };
            set { SoundOperatorDto = value[0]; CenterFrequencyOperatorDto = value[1]; WidthOperatorDto = value[2]; }
        }
    }

    internal class NotchFilter_OperatorDto_ManyConsts : OperatorDtoBase_Filter_ManyConsts_WithWidthOrBlobVolume
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.LowPassFilter;
        public override double Frequency => CenterFrequency;
        public override double WidthOrBlobVolume => Width;

        public double Width { get; set; }
        public double CenterFrequency { get; set; }
    }
}