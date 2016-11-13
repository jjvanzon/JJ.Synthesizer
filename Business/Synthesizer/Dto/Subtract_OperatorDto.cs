using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Subtract_OperatorDto : OperatorDtoBase_VarA_VarB
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Subtract);

        public Subtract_OperatorDto(OperatorDtoBase aOperatorDto, OperatorDtoBase bOperatorDto)
            : base(aOperatorDto, bOperatorDto)
        { }
    }
}