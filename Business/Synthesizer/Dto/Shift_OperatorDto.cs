using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Shift_OperatorDto : OperatorDto
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Shift);

        public OperatorDto SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDto DifferenceOperatorDto => InputOperatorDtos[1];

        public Shift_OperatorDto(OperatorDto signalOperatorDto, OperatorDto differenceOperatorDto)
            : base(new OperatorDto[] { signalOperatorDto, differenceOperatorDto })
        { }
    }
}
