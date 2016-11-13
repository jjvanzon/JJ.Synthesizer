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
}