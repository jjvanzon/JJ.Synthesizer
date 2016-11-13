using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Loop_OperatorDto : OperatorDto
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Loop);

        public OperatorDto SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDto SkipOperatorDto => InputOperatorDtos[1];
        public OperatorDto LoopStartMarkerOperatorDto => InputOperatorDtos[2];
        public OperatorDto LoopEndMarkerOperatorDto => InputOperatorDtos[3];
        public OperatorDto ReleaseOperatorDto => InputOperatorDtos[4];
        public OperatorDto NoteDurationOperatorDto => InputOperatorDtos[5];

        public Loop_OperatorDto(
            OperatorDto signalOperatorDto,
            OperatorDto skipOperatorDto,
            OperatorDto loopStartMarkerOperatorDto,
            OperatorDto loopEndMarkerOperatorDto,
            OperatorDto releaseEndMarkerOperatorDto,
            OperatorDto noteDurationOperatorDto)
            : base(new OperatorDto[]
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