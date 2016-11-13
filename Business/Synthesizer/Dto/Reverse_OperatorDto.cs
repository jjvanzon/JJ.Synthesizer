using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Reverse_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Reverse);

        public OperatorDtoBase SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase SpeedOperatorDto => InputOperatorDtos[1];

        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }

        public Reverse_OperatorDto(
            OperatorDtoBase signalOperatorDto, 
            OperatorDtoBase speedOperatorDto)
            : base(new OperatorDtoBase[] { signalOperatorDto, speedOperatorDto })
        { }
    }
}