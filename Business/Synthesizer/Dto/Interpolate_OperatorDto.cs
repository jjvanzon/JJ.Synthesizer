using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Interpolate_OperatorDto : OperatorDto
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Interpolate);

        public OperatorDto SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDto SamplingRateOperatorDto => InputOperatorDtos[1];
        public ResampleInterpolationTypeEnum ResampleInterpolationTypeEnum { get; set; }

        public Interpolate_OperatorDto(OperatorDto signalOperatorDto, OperatorDto samplingRateOperatorDto)
            : base(new OperatorDto[] { signalOperatorDto, samplingRateOperatorDto })
        { }
    }
}