using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Or_OperatorDto : OperatorDto_VarA_VarB
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Or);

        public Or_OperatorDto(OperatorDto aOperatorDto, OperatorDto bOperatorDto)
            : base(aOperatorDto, bOperatorDto)
        { }
    }
}