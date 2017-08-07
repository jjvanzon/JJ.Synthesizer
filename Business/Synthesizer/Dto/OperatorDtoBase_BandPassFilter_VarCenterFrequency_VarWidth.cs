using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_BandPassFilter_VarCenterFrequency_VarWidth : OperatorDtoBase_Filter_VarSound
    {
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
}