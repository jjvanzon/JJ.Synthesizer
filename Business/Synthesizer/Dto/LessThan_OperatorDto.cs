using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class LessThan_OperatorDto : OperatorDto_VarA_VarB
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.LessThan);

        public LessThan_OperatorDto(OperatorDto aOperatorDto, OperatorDto bOperatorDto)
            : base(aOperatorDto, bOperatorDto)
        { }
    }
}