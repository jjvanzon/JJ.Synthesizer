using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Negative_OperatorDto : OperatorDtoBase_VarX
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Negative);

        public Negative_OperatorDto(OperatorDtoBase xOperatorDto)
            : base(xOperatorDto)
        { }
    }
}