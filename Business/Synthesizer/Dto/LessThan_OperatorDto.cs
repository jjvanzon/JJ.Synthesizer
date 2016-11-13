using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class LessThan_OperatorDto : OperatorDtoBase_VarA_VarB
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.LessThan);

        public LessThan_OperatorDto(OperatorDtoBase aOperatorDto, OperatorDtoBase bOperatorDto)
            : base(aOperatorDto, bOperatorDto)
        { }
    }
}