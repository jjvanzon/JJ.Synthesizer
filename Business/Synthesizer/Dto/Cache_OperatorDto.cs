using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Cache_OperatorDto : OperatorDto
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Cache);

        public OperatorDto SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDto StartOperatorDto => InputOperatorDtos[1];
        public OperatorDto EndOperatorDto => InputOperatorDtos[2];
        public OperatorDto SamplingRateOperatorDto => InputOperatorDtos[3];

        public InterpolationTypeEnum InterpolationTypeEnum { get; set; }
        public SpeakerSetupEnum SpeakerSetupEnum { get; set; }

        public Cache_OperatorDto(
            OperatorDto signalOperatorDto,
            OperatorDto startOperatorDto,
            OperatorDto endOperatorDto,
            OperatorDto samplingRateOperatorDto)
            : base(new OperatorDto[] 
            {
                signalOperatorDto,
                startOperatorDto,
                endOperatorDto,
                samplingRateOperatorDto
            })
        { }
    }
}