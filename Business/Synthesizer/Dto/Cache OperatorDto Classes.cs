using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Cache_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Cache);

        public OperatorDtoBase SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase StartOperatorDto => InputOperatorDtos[1];
        public OperatorDtoBase EndOperatorDto => InputOperatorDtos[2];
        public OperatorDtoBase SamplingRateOperatorDto => InputOperatorDtos[3];

        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }
        public InterpolationTypeEnum InterpolationTypeEnum { get; set; }
        public SpeakerSetupEnum SpeakerSetupEnum { get; set; }

        public Cache_OperatorDto(
            OperatorDtoBase signalOperatorDto,
            OperatorDtoBase startOperatorDto,
            OperatorDtoBase endOperatorDto,
            OperatorDtoBase samplingRateOperatorDto)
            : base(new OperatorDtoBase[] 
            {
                signalOperatorDto,
                startOperatorDto,
                endOperatorDto,
                samplingRateOperatorDto
            })
        { }
    }

    internal class Cache_OperatorDto_SingleChannel : Cache_OperatorDto
    {
        public Cache_OperatorDto_SingleChannel(
            OperatorDtoBase signalOperatorDto,
            OperatorDtoBase startOperatorDto,
            OperatorDtoBase endOperatorDto,
            OperatorDtoBase samplingRateOperatorDto)
            : base(signalOperatorDto, startOperatorDto, endOperatorDto, samplingRateOperatorDto)
        { }
    }

    internal class Cache_OperatorDto_MultiChannel : Cache_OperatorDto
    {
        public Cache_OperatorDto_MultiChannel(
            OperatorDtoBase signalOperatorDto,
            OperatorDtoBase startOperatorDto,
            OperatorDtoBase endOperatorDto,
            OperatorDtoBase samplingRateOperatorDto)
            : base(signalOperatorDto, startOperatorDto, endOperatorDto, samplingRateOperatorDto)
        { }
    }
}