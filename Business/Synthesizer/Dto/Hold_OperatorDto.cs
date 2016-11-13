using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Hold_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Hold);

        public OperatorDtoBase SignalOperatorDto => InputOperatorDtos[0];

        public Hold_OperatorDto(OperatorDtoBase signalOperatorDto) 
            : base(new OperatorDtoBase[] { signalOperatorDto })
        { }
    }
}