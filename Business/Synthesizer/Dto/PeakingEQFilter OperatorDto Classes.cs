using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class PeakingEQFilter_OperatorDto : PeakingEQFilter_OperatorDto_AllVars
    { }

    internal class PeakingEQFilter_OperatorDto_ConstSound : OperatorDtoBase_ConstSound
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.PeakingEQFilter;
    }

    internal class PeakingEQFilter_OperatorDto_AllVars : OperatorDtoBase_Filter_VarSound
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.PeakingEQFilter;

        public IOperatorDto CenterFrequencyOperatorDto { get; set; }
        public IOperatorDto WidthOperatorDto { get; set; }
        public IOperatorDto DBGainOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[]
            {
                SoundOperatorDto,
                CenterFrequencyOperatorDto,
                WidthOperatorDto,
                DBGainOperatorDto
            };
            set
            {
                SoundOperatorDto = value[0];
                CenterFrequencyOperatorDto = value[1];
                WidthOperatorDto = value[2];
                DBGainOperatorDto = value[3];
            }
        }

        public override IEnumerable<InputDto> InputDtos => new[]
        {
            new InputDto(SoundOperatorDto),
            new InputDto(CenterFrequencyOperatorDto),
            new InputDto(WidthOperatorDto),
            new InputDto(DBGainOperatorDto)
        };
    }

    internal class PeakingEQFilter_OperatorDto_ManyConsts : OperatorDtoBase_Filter_ManyConsts
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.PeakingEQFilter;
        public override double Frequency => CenterFrequency;

        public double CenterFrequency { get; set; }
        public double Width { get; set; }
        public double DBGain { get; set; }

        public override IEnumerable<InputDto> InputDtos => new[]
        {
            new InputDto(SoundOperatorDto),
            new InputDto(CenterFrequency),
            new InputDto(Width),
            new InputDto(DBGain)
        };
    }
}