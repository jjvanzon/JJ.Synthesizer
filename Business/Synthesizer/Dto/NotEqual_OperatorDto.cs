using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class NotEqual_OperatorDto : OperatorDtoBase_VarA_VarB
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.NotEqual);

        public NotEqual_OperatorDto(OperatorDtoBase aOperatorDto, OperatorDtoBase bOperatorDto)
            : base(aOperatorDto, bOperatorDto)
        { }
    }
}