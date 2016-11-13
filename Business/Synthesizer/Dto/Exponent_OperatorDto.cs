using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Exponent_OperatorDto : OperatorDto
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Exponent);

        public OperatorDto LowOperatorDto => InputOperatorDtos[0];
        public OperatorDto HighOperatorDto => InputOperatorDtos[1];
        public OperatorDto RatioOperatorDto => InputOperatorDtos[2];

        public Exponent_OperatorDto(
            OperatorDto lowOperatorDto,
            OperatorDto highOperatorDto,
            OperatorDto ratioOperatorDto)
            : base(new OperatorDto[] { lowOperatorDto, highOperatorDto, ratioOperatorDto })
        { }
    }
}