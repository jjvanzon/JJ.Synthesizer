using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class GreaterThan_OperatorDto : OperatorDtoBase_VarA_VarB
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.GreaterThan);

        public GreaterThan_OperatorDto(OperatorDtoBase aOperatorDto, OperatorDtoBase bOperatorDto)
            : base(aOperatorDto, bOperatorDto)
        { }
    }
}