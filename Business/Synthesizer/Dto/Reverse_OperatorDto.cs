using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Reverse_OperatorDto : OperatorDto
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Reverse);

        public OperatorDto SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDto SpeedOperatorDto => InputOperatorDtos[1];

        public Reverse_OperatorDto(
            OperatorDto signalOperatorDto, 
            OperatorDto speedOperatorDto)
            : base(new OperatorDto[] { signalOperatorDto, speedOperatorDto })
        { }
    }
}