using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Loop_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Loop);

        public OperatorDtoBase SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase SkipOperatorDto => InputOperatorDtos[1];
        public OperatorDtoBase LoopStartMarkerOperatorDto => InputOperatorDtos[2];
        public OperatorDtoBase LoopEndMarkerOperatorDto => InputOperatorDtos[3];
        public OperatorDtoBase ReleaseOperatorDto => InputOperatorDtos[4];
        public OperatorDtoBase NoteDurationOperatorDto => InputOperatorDtos[5];

        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }

        public Loop_OperatorDto(
            OperatorDtoBase signalOperatorDto,
            OperatorDtoBase skipOperatorDto,
            OperatorDtoBase loopStartMarkerOperatorDto,
            OperatorDtoBase loopEndMarkerOperatorDto,
            OperatorDtoBase releaseEndMarkerOperatorDto,
            OperatorDtoBase noteDurationOperatorDto)
            : base(new OperatorDtoBase[]
            {
                signalOperatorDto,
                skipOperatorDto,
                loopStartMarkerOperatorDto,
                loopEndMarkerOperatorDto,
                releaseEndMarkerOperatorDto,
                noteDurationOperatorDto
            })
        { }
    }
}