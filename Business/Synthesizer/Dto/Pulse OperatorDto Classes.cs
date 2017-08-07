using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Pulse_OperatorDto : OperatorDtoBase_VarFrequency
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Pulse;

        public IOperatorDto WidthOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { FrequencyOperatorDto, WidthOperatorDto };
            set { FrequencyOperatorDto = value[0]; WidthOperatorDto = value[1]; }
        }

        public override IEnumerable<InputDto> InputDtos => new[]
        {
            new InputDto(FrequencyOperatorDto),
            new InputDto(WidthOperatorDto)
        };
    }

    internal class Pulse_OperatorDto_ZeroFrequency : OperatorDtoBase_WithoutInputDtos
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Pulse;
    }

    internal class Pulse_OperatorDto_ConstFrequency_HalfWidth_WithOriginShifting : OperatorDtoBase_ConstFrequency
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Pulse;
    }

    internal class Pulse_OperatorDto_ConstFrequency_ConstWidth_WithOriginShifting : OperatorDtoBase_ConstFrequency
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Pulse;

        public double Width { get; set; }

        public override IEnumerable<InputDto> InputDtos => new[]
        {
            new InputDto(Frequency),
            new InputDto(Width)
        };
    }

    internal class Pulse_OperatorDto_ConstFrequency_VarWidth_WithOriginShifting : OperatorDtoBase_ConstFrequency
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Pulse;

        public IOperatorDto WidthOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { WidthOperatorDto };
            set => WidthOperatorDto = value[0];
        }

        public override IEnumerable<InputDto> InputDtos => new[]
        {
            new InputDto(Frequency),
            new InputDto(WidthOperatorDto)
        };
    }

    internal class Pulse_OperatorDto_VarFrequency_HalfWidth_WithPhaseTracking : OperatorDtoBase_VarFrequency
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Pulse;
    }

    internal class Pulse_OperatorDto_VarFrequency_ConstWidth_WithPhaseTracking : OperatorDtoBase_VarFrequency
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Pulse;

        public double Width { get; set; }

        public override IEnumerable<InputDto> InputDtos => new[]
        {
            new InputDto(FrequencyOperatorDto),
            new InputDto(Width)
        };
    }

    internal class Pulse_OperatorDto_VarFrequency_VarWidth_WithPhaseTracking : Pulse_OperatorDto
    { }

    internal class Pulse_OperatorDto_ConstFrequency_HalfWidth_NoOriginShifting : Pulse_OperatorDto_ConstFrequency_HalfWidth_WithOriginShifting
    { }

    internal class Pulse_OperatorDto_ConstFrequency_ConstWidth_NoOriginShifting : Pulse_OperatorDto_ConstFrequency_ConstWidth_WithOriginShifting
    { }

    internal class Pulse_OperatorDto_ConstFrequency_VarWidth_NoOriginShifting : Pulse_OperatorDto_ConstFrequency_VarWidth_WithOriginShifting
    { }

    internal class Pulse_OperatorDto_VarFrequency_HalfWidth_NoPhaseTracking : Pulse_OperatorDto_VarFrequency_HalfWidth_WithPhaseTracking
    { }

    internal class Pulse_OperatorDto_VarFrequency_ConstWidth_NoPhaseTracking : Pulse_OperatorDto_VarFrequency_ConstWidth_WithPhaseTracking
    { }

    internal class Pulse_OperatorDto_VarFrequency_VarWidth_NoPhaseTracking : Pulse_OperatorDto_VarFrequency_VarWidth_WithPhaseTracking
    { }
}