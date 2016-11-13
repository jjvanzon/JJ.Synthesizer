using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Not_OperatorDto : OperatorDto_VarX
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Not);

        public Not_OperatorDto(OperatorDto xOperatorDto)
            : base(xOperatorDto)
        { }
    }
}