using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Exponent_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Exponent);

        public OperatorDtoBase LowOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase HighOperatorDto => InputOperatorDtos[1];
        public OperatorDtoBase RatioOperatorDto => InputOperatorDtos[2];

        public Exponent_OperatorDto(
            OperatorDtoBase lowOperatorDto,
            OperatorDtoBase highOperatorDto,
            OperatorDtoBase ratioOperatorDto)
            : base(new OperatorDtoBase[] { lowOperatorDto, highOperatorDto, ratioOperatorDto })
        { }
    }
}