using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Interpolate_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Interpolate);

        public OperatorDtoBase SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase SamplingRateOperatorDto => InputOperatorDtos[1];
        public ResampleInterpolationTypeEnum ResampleInterpolationTypeEnum { get; set; }

        public Interpolate_OperatorDto(OperatorDtoBase signalOperatorDto, OperatorDtoBase samplingRateOperatorDto)
            : base(new OperatorDtoBase[] { signalOperatorDto, samplingRateOperatorDto })
        { }
    }
}