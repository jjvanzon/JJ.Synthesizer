using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Subtract_OperatorDto : OperatorDto_VarA_VarB
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Subtract);

        public Subtract_OperatorDto(OperatorDto aOperatorDto, OperatorDto bOperatorDto)
            : base(aOperatorDto, bOperatorDto)
        { }
    }
}