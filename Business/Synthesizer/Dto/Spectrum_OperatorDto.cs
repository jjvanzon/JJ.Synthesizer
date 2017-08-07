using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Spectrum_OperatorDto : Spectrum_OperatorDto_AllVars
    { }

    internal class Spectrum_OperatorDto_ConstSound : OperatorDtoBase_ConstSound
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Spectrum;
    }

    internal class Spectrum_OperatorDto_AllVars : OperatorDtoBase_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Spectrum;

        public IOperatorDto SoundOperatorDto { get; set; }
        public IOperatorDto StartOperatorDto { get; set; }
        public IOperatorDto EndOperatorDto { get; set; }
        public IOperatorDto FrequencyCountOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { SoundOperatorDto, StartOperatorDto, EndOperatorDto, FrequencyCountOperatorDto };
            set
            {
                SoundOperatorDto = value[0];
                StartOperatorDto = value[1];
                EndOperatorDto = value[2];
                FrequencyCountOperatorDto = value[3];
            }
        }

        public override IEnumerable<InputDto> InputDtos => new[]
        {
            new InputDto(SoundOperatorDto),
            new InputDto(StartOperatorDto),
            new InputDto(EndOperatorDto),
            new InputDto(FrequencyCountOperatorDto)
        };
    }
}