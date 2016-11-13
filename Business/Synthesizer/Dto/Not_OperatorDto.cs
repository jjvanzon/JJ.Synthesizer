using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Not_OperatorDto : OperatorDtoBase_VarX
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Not);

        public Not_OperatorDto(OperatorDtoBase xOperatorDto)
            : base(xOperatorDto)
        { }
    }
}