using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Unbundle_OperatorDto : OperatorDto
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Unbundle);
        
        // TODO: You need information to identify which outlet of the Unbundle Operator is represented here.

        public OperatorDto InputOperatorDto => InputOperatorDtos[0];

        public Unbundle_OperatorDto(OperatorDto inputOperatorDto)
            : base(new OperatorDto[] { inputOperatorDto })
        { }
    }
}
