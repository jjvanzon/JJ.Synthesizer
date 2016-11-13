using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class OneOverX_OperatorDto : OperatorDtoBase_VarX
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.OneOverX);

        public OneOverX_OperatorDto(OperatorDtoBase xOperatorDto)
            : base(xOperatorDto)
        { }
    }
}