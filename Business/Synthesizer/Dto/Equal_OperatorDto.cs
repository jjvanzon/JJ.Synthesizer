using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Equal_OperatorDto : OperatorDto_VarA_VarB
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Equal);

        public Equal_OperatorDto(OperatorDto aOperatorDto, OperatorDto bOperatorDto)
            : base(aOperatorDto, bOperatorDto)
        { }
    }
}