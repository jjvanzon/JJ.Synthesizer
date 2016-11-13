using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Absolute_OperatorDto : OperatorDto_VarX
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Absolute);

        public Absolute_OperatorDto(OperatorDto xOperatorDto)
            : base(xOperatorDto)
        { }
    }
}