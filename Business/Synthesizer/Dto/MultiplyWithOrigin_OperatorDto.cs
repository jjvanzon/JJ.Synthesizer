using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class MultiplyWithOrigin_OperatorDto : OperatorDto
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MultiplyWithOrigin);

        public OperatorDto AOperatorDto => ChildOperatorDtos[0];
        public OperatorDto BOperatorDto => ChildOperatorDtos[1];
        public OperatorDto OriginOperatorDto => ChildOperatorDtos[2];

        public MultiplyWithOrigin_OperatorDto(
            OperatorDto aOperatorDto,
            OperatorDto bOperatorDto,
            OperatorDto originOperatorDto)
            : base(new OperatorDto[] { aOperatorDto, bOperatorDto, originOperatorDto })
        { }
    }
}