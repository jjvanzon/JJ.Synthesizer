using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class MultiplyWithOrigin_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MultiplyWithOrigin);

        public OperatorDtoBase AOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase BOperatorDto => InputOperatorDtos[1];
        public OperatorDtoBase OriginOperatorDto => InputOperatorDtos[2];

        public MultiplyWithOrigin_OperatorDto(
            OperatorDtoBase aOperatorDto,
            OperatorDtoBase bOperatorDto,
            OperatorDtoBase originOperatorDto)
            : base(new OperatorDtoBase[] { aOperatorDto, bOperatorDto, originOperatorDto })
        { }
    }
}