using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Divide_OperatorDto : OperatorDto
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);

        public OperatorDto NumeratorOperatorDto => InputOperatorDtos[0];
        public OperatorDto DenominatorOperatorDto => InputOperatorDtos[1];

        public Divide_OperatorDto(OperatorDto numeratorOperatorDto, OperatorDto denominatorOperatorDto)
            : base(new OperatorDto[] { numeratorOperatorDto, denominatorOperatorDto })
        { }
    }
}
